using System;
using System.Collections.Generic;
using System.Text;
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
        public void TestGetEnvironmentFlagsCallsApiWhenNoLocalEnvironment()
        {
            FlagsmithClientTest.instance = null;
            var flagsmithClientTest = new FlagsmithClientTest(Fixtures.FlagsmithConfiguration());
            Assert.True(flagsmithClientTest.IsEnvironmentEmpty());

        }
        [Fact]
        public void TestGetEnvironmentFlagsUsesLocalEnvironmentWhenAvailable()
        {

        }
        [Fact]
        public void TestGetIdentityFlagsCallsApiWhenNoLocalEnvironmentNoTraits()
        {

        }
        [Fact]
        public void TestGetIdentityFlagsCallsApiWhenNoLocalEnvironmentWithTraits()
        {

        }
        [Fact]
        public void TestGetIdentityFlagsUsesLocalEnvironmentWhenAvailable()
        {

        }
        [Fact]
        public void TestRequestConnectionErrorRaisesFlagsmithApiError()
        {

        }
        [Fact]
        public void TestNon200ResponseRaisesFlagsmithApiError()
        {

        }
        [Fact]
        public void TestDefaultFlagIsUsedWhenNoEnvironmentFlagsReturned()
        {

        }
        [Fact]
        public void TestDefaultFlagIsNotUsedWhenEnvironmentFlagsReturned()
        {

        }
        [Fact]
        public void TestDefaultFlagIsUsedWhenNoIdentityFlagsReturned()
        {

        }
        [Fact]
        public void TestDefaultFlagIsNotUsedWhenIdentityFlagsReturned()
        {

        }
    }
    class FlagsmithClientTest : FlagsmithClient
    {
        Dictionary<string, int> totalFucntionCalls;
        public FlagsmithClientTest(FlagsmithConfiguration flagsmithConfiguration) : base(flagsmithConfiguration)
        {
            _initDict();
        }
        public bool IsEnvironmentEmpty()
        {
            return Environment == null;
        }
        protected async override Task GetAndUpdateEnvironmentFromApi()
        {
            await Task.Delay(0);
            Environment = Fixtures.Environment;
            _initDict();
            totalFucntionCalls[nameof(GetAndUpdateEnvironmentFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetAndUpdateEnvironmentFromApi), out int i) ? i + 1 : 1;
        }
        public int this[string functionName] => totalFucntionCalls[functionName];
        private void _initDict()
        {
            if (totalFucntionCalls == null)
                totalFucntionCalls = new Dictionary<string, int>();
        }

    }
}

