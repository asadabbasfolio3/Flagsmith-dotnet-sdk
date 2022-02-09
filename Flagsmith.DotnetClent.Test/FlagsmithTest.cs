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

}

