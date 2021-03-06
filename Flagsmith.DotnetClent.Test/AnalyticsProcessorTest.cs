using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;

namespace Flagsmith.DotnetClient.Test
{
    /// <summary>
    /// This class provides some extra functionality to help particularly in unit testing.
    /// </summary>
    internal class AnalyticsProcessorTest : AnalyticsProcessor
    {
        public AnalyticsProcessorTest(HttpClient httpClient, string environmentKey, string baseApiUrl, int timeOut = 3)
            : base(httpClient, environmentKey, baseApiUrl, timeOut: timeOut)
        {
        }
        /// <summary>
        /// Returns tracked feature counts that are not posted on the server yet.
        /// </summary>
        /// <param name="featureId"></param>
        /// <returns></returns>
        public int this[int featureId] => AnalyticsData[featureId];
        /// <summary>
        /// Check if there are any items in analytics cache.
        /// </summary>
        /// <returns></returns>
        public bool HasTrackingItemsInCache() => AnalyticsData.Any();
        public override string ToString() => JsonConvert.SerializeObject(AnalyticsData);
    }
}
