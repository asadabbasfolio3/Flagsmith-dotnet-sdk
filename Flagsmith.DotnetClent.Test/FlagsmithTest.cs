using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
namespace Flagsmith.DotnetClient.Test
{
    public class FlagsmithTest
    {
        [Fact]
        public void TestFlagsmithStartsPollingManagerOnInitIfEnabled()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.JsonObject.ToString())
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object, enableClientSideEvaluation: true);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);
        }
        [Fact]
        public async void TestUpdateEnvironmentSetsEnvironment()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.JsonObject.ToString())
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, enableClientSideEvaluation: true, httpClient: mockHttpClient.Object);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);
            await flagsmithClientTest.GetEnvironmentFlags();
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/flags/", Times.Never);
        }
        [Fact]
        public async Task TestGetEnvironmentFlagsCallsApiWhenNoLocalEnvironment()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.ApiFlagResponse)
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object);
            var flags = (await flagsmithClientTest.GetEnvironmentFlags()).AllFlags();
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/flags/", Times.Once);
            Assert.True(flags[0].Enabled);
            Assert.Equal("some-value", flags[0].Value);
            Assert.Equal("some_feature", flags[0].Feature.Name);
        }
        [Fact]
        public async Task TestGetEnvironmentFlagsUsesLocalEnvironmentWhenAvailable()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.JsonObject.ToString())
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, enableClientSideEvaluation: true, httpClient: mockHttpClient.Object);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);
            var flags = (await flagsmithClientTest.GetEnvironmentFlags()).AllFlags();
            var fs = Fixtures.Environment.FeatureStates[0];
            Assert.Equal(fs.Enabled, flags[0].Enabled);
            Assert.Equal(fs.GetValue(), flags[0].Value);
            Assert.Equal(fs.Feature.Name, flags[0].Feature.Name);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);
        }
        [Fact]
        public async Task TestGetIdentityFlagsCallsApiWhenNoLocalEnvironmentNoTraits()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.ApiIdentityResponse)
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object);
            var flags = (await flagsmithClientTest.GetIdentityFlags("identifier")).AllFlags();
            Assert.True(flags[0].Enabled);
            Assert.Equal("some-value", flags[0].Value);
            Assert.Equal("some_feature", flags[0].Feature.Name);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/identities/identifier/", Times.Once);

        }
        [Fact]
        public async Task TestGetIdentityFlagsUsesLocalEnvironmentWhenAvailable()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.JsonObject.ToString())
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, enableClientSideEvaluation: true, httpClient: mockHttpClient.Object);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);
            _ = await flagsmithClientTest.GetIdentityFlags("identifier", null);
            mockHttpClient.verifyHttpRequest(HttpMethod.Get, "/api/v1/environment-document/", Times.Once);

        }
        [Fact]
        public async Task TestRequestConnectionErrorRaisesFlagsmithApiError()
        {
            var mockHttpClient = HttpMocker.MockHttpThrowConnectionError();
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object);
            await Assert.ThrowsAsync<FlagsmithAPIError>(async () => await flagsmithClientTest.GetEnvironmentFlags());
        }
        [Fact]
        public async Task TestNon200ResponseRaisesFlagsmithApiError()
        {
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object);
            await Assert.ThrowsAsync<FlagsmithAPIError>(async () => await flagsmithClientTest.GetEnvironmentFlags());
        }
        [Fact]
        public async Task TestDefaultFlagIsUsedWhenNoEnvironmentFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("[]")
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object, defaultFlagHandler: (string name) => defaultFlag);
            var flags = await flagsmithClientTest.GetEnvironmentFlags();
            var flag = await flags.GetFlag("some_feature");
            Assert.True(flag.Enabled);
            Assert.Equal("some-default-value", flag.Value);
        }
        [Fact]
        public async Task TestDefaultFlagIsNotUsedWhenEnvironmentFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.ApiFlagResponse)
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object, defaultFlagHandler: (string name) => defaultFlag);
            var flags = await flagsmithClientTest.GetEnvironmentFlags();
            var flag = await flags.GetFlag("some_feature");
            Assert.True(flag.Enabled);
            Assert.NotEqual("some-default-value", flag.Value);
        }
        [Fact]
        public async Task TestDefaultFlagIsUsedWhenNoIdentityFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("{}")
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object, defaultFlagHandler: (string name) => defaultFlag);
            var flags = await flagsmithClientTest.GetIdentityFlags("identifier");
            var flag = await flags.GetFlag("some_feature");
            Assert.True(flag.Enabled);
            Assert.Equal("some-default-value", flag.Value);
        }
        [Fact]
        public async Task TestDefaultFlagIsNotUsedWhenIdentityFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(Fixtures.ApiIdentityResponse)
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object, defaultFlagHandler: (string name) => defaultFlag);
            var flags = await flagsmithClientTest.GetIdentityFlags("identifier");
            var flag = await flags.GetFlag("some_feature");
            Assert.True(flag.Enabled);
            Assert.NotEqual("some-default-value", flag.Value);
        }
        [Fact]
        public async Task TestDefaultFlagsAreUsedIfApiErrorAndDefaultFlagHandlerGiven()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var mockHttpClient = HttpMocker.MockHttpResponse(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
            });
            var flagsmithClientTest = new FlagsmithClient(Fixtures.ApiKey, httpClient: mockHttpClient.Object, defaultFlagHandler: (string name) => defaultFlag);
            var flags = await flagsmithClientTest.GetEnvironmentFlags();
            var flag = await flags.GetFlag("some_feature");
            Assert.True(flag.Enabled);
            Assert.Equal("some-default-value", flag.Value);
        }


    }

}

