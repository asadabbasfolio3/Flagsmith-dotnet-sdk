using FlagsmithEngine.Environment.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
namespace Flagsmith.DotnetClient.Test
{
    public class FlagsmithTest
    {
        [Fact]
        public void TestFlagsmithStartsPollingManagerOnInitIfEnabled()
        {

        }
        [Fact]
        public async Task TestUpdateEnvironmentSetsEnvironment()
        {
            FlagsmithClientTest.instance = null;
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            Assert.True(flagsmithClientTest.IsEnvironmentEmpty());
            await flagsmithClientTest.TriggerEnvironmentUpdate();
            Assert.False(flagsmithClientTest.IsEnvironmentEmpty());
            Assert.True(flagsmithClientTest.IsEnvironmentEqual(Fixtures.Environment));
        }
        [Fact]
        public async Task TestGetEnvironmentFlagsCallsApiWhenNoLocalEnvironment()
        {
            FlagsmithClientTest.instance = null;
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            var flags = await flagsmithClientTest.GetFeatureFlags();
            Assert.Equal(1, flagsmithClientTest["GetFeatureFlagsFromApi"]);
            Assert.True(flags[0].IsEnabled());
            Assert.Equal("some-value", flags[0].GetValue());
            Assert.Equal("some_feature", flags[0].GetFeature().GetName());
        }
        [Fact]
        public async Task TestGetEnvironmentFlagsUsesLocalEnvironmentWhenAvailable()
        {
            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(Fixtures.FlagsmithConfiguration());
            var flags = await flagsmithClientTest.GetFeatureFlags();
            Assert.Equal(0, flagsmithClientTest["GetFeatureFlagsFromApi"]);
            var fs = Fixtures.Environment.FeatureStates[0];
            Assert.Equal(fs.Enabled, flags[0].IsEnabled());
            Assert.Equal(fs.GetValue(), flags[0].GetValue());
            Assert.Equal(fs.Feature.Name, flags[0].GetFeature().GetName());
        }
        [Fact]
        public async Task TestGetIdentityFlagsCallsApiWhenNoLocalEnvironmentNoTraits()
        {

            FlagsmithClientTest.instance = null;
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            var flags = await flagsmithClientTest.GetFeatureFlags("identifier");
            Assert.Equal(1, flagsmithClientTest["GetIdentityFlagsFromApi"]);
            Assert.True(flags[0].IsEnabled());
            Assert.Equal("some-value", flags[0].GetValue());
            Assert.Equal("some_feature", flags[0].GetFeature().GetName());
        }
        [Fact]
        public void TestGetIdentityFlagsCallsApiWhenNoLocalEnvironmentWithTraits()
        {
            //currenlty get api call is implemnted for identity flags so no need for traits post api test
        }
        [Fact]
        public async Task TestGetIdentityFlagsUsesLocalEnvironmentWhenAvailable()
        {

            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(Fixtures.FlagsmithConfiguration());
            _ = await flagsmithClientTest.GetFeatureFlags("identifier", null);
            Assert.Equal(1, flagsmithClientTest["GetIdentityFlagsFromDocuments"]);
        }
        [Fact]
        public async Task TestRequestConnectionErrorRaisesFlagsmithApiError()
        {
            FlagsmithClientTest.instance = null;
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            var flagsmithClientTest = new FlagsmithClient(config);
            await Assert.ThrowsAsync<FlagsmithAPIError>(async () => await flagsmithClientTest.GetFeatureFlags());
        }
        [Fact]
        public void TestNon200ResponseRaisesFlagsmithApiError()
        {

        }
        [Fact]
        public async Task TestDefaultFlagIsUsedWhenNoEnvironmentFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            config.DefaultFlagHandler = (string name) => defaultFlag;
            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            var flag = await flagsmithClientTest.GetFeatureFlag("some_feature1");
            Assert.True(flag.IsEnabled());
            Assert.Equal("some-default-value", flag.GetValue());
        }
        [Fact]
        public async Task TestDefaultFlagIsNotUsedWhenEnvironmentFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            config.DefaultFlagHandler = (string name) => defaultFlag;
            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            var flag = await flagsmithClientTest.GetFeatureFlag("some_feature");
            Assert.True(flag.IsEnabled());
            Assert.NotEqual("some-default-value", flag.GetValue());
        }
        [Fact]
        public async Task TestDefaultFlagIsUsedWhenNoIdentityFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            config.DefaultFlagHandler = (string name) => defaultFlag;
            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            var flag = await flagsmithClientTest.GetFeatureFlag("some_feature1", "identifier");
            Assert.True(flag.IsEnabled());
            Assert.Equal("some-default-value", flag.GetValue());
        }
        [Fact]
        public async Task TestDefaultFlagIsNotUsedWhenIdentityFlagsReturned()
        {
            var defaultFlag = new Flag(null, true, "some-default-value");
            var config = Fixtures.FlagsmithConfiguration();
            config.EnableClientSideEvaluation = false;
            config.DefaultFlagHandler = (string name) => defaultFlag;
            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(config);
            var flag = await flagsmithClientTest.GetFeatureFlag("some_feature", "identifier");
            Assert.True(flag.IsEnabled());
            Assert.NotEqual("some-default-value", flag.GetValue());
        }
        [Fact]
        public void TestDefaultFlagsAreUsedIfApiErrorAndDefaultFlagHandlerGiven()
        {

        }
    }

}

