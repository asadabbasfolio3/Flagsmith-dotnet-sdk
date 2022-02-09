using System;
using Flagsmith;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Flagsmith.DotnetClient.Test
{
    public class AnalyticsTests
    {
        public readonly AnalyticsProcessor _AnalyticsProcessor;
        public AnalyticsTests()
        {
            _AnalyticsProcessor = Fixtures.AnalyticsProcessor;
        }
        [Fact]
        public void TestAnalyticsProcessorTrackFeatureUpdatesAnalyticsData()
        {
            _ = _AnalyticsProcessor.TrackFeature(1);
            Assert.Equal(1, _AnalyticsProcessor[1]);
            _ = _AnalyticsProcessor.TrackFeature(1);
            Assert.Equal(2, _AnalyticsProcessor[1]);
        }
        [Fact]
        public async Task TestAnalyticsProcessorFlushClearsAnalyticsData()
        {
            _ = _AnalyticsProcessor.TrackFeature(1);
            await _AnalyticsProcessor.Flush();
            Assert.False(_AnalyticsProcessor.HasTrackingItemsInCache());
        }
        [Fact]
        public async void TestAnalyticsProcessorFlushPostRequestDataMatchAnanlyticsData()
        {
            var httpClient = new Mock<HttpClient>();
            httpClient.CallBase = true;
            var analyticsProcessor = Fixtures.GetAnalyticalProcessor(httpClient.Object);
            await analyticsProcessor.TrackFeature(1);
            await analyticsProcessor.TrackFeature(2);
            var jObject = JObject.Parse(analyticsProcessor.ToString());
            await analyticsProcessor.Flush();
            httpClient.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(1, jObject["1"].Value<int>());
            Assert.Equal(1, jObject["2"].Value<int>());
        }
        [Fact]
        public async Task TestAnalyticsProcessorFlushEarlyExitIfAnalyticsDataIsEmpty()
        {

            var httpClient = new Mock<HttpClient>();
            httpClient.CallBase = true;
            var analyticsProcessor = Fixtures.GetAnalyticalProcessor(httpClient.Object);
            await analyticsProcessor.Flush();
            httpClient.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()), Times.Never());
        }
        [Fact]
        public async Task TestAnalyticsProcessorCallingTrackFeatureCallsFlushWhenTimerRunsOut()
        {
            var httpClient = new Mock<HttpClient>();
            httpClient.CallBase = true;
            var analyticsProcessor = Fixtures.GetAnalyticalProcessor(httpClient.Object);
            await Task.Delay(12 * 1000);
            await analyticsProcessor.TrackFeature(1);
            httpClient.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
