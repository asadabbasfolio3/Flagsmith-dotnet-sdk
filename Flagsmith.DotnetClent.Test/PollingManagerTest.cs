using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace Flagsmith.DotnetClient.Test
{
    public class PollingManagerTest
    {
        [Fact]
        public void TestPollingManagerCallsUpdateEnvironmentOnStart()
        {
            FlagsmithClient.instance = null;
            var x = new Mock<FlagsmithClient>(Fixtures.FlagsmithConfiguration());
            x.Setup(x => x.GetAndUpdateEnvironmentFromApi()).Returns(Task.CompletedTask);
            var temp = x.Object;
            x.Verify(x => x.GetAndUpdateEnvironmentFromApi(), Times.Once);
        }
        [Fact]
        public async Task TestPollingManagerCallsUpdateEnvironmentOnEachRefresh()
        {
            FlagsmithClient.instance = null;
            var config = Fixtures.FlagsmithConfiguration();
            config.EnvironmentRefreshIntervalSeconds = 1;
            var x = new Mock<FlagsmithClient>(config);
            x.Setup(x => x.GetAndUpdateEnvironmentFromApi()).Returns(Task.CompletedTask);
            var temp = x.Object;
            await Task.Delay(2500);
            x.Verify(x => x.GetAndUpdateEnvironmentFromApi(), Times.Exactly(3));
        }
    }
}
