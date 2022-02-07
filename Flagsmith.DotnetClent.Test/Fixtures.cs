using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Flagsmith.DotnetClient.Test
{
    internal class Fixtures
    {
        public static AnalyticsProcessor AnalyticsProcessor { get; } = new AnalyticsProcessor(new HttpClient(), "text_key", "http://test_url");
        public static AnalyticsProcessor GetAnalyticalProcessor(HttpClient httpClient) => new AnalyticsProcessor(httpClient, "text_key", "http://test_url");
    }
}
