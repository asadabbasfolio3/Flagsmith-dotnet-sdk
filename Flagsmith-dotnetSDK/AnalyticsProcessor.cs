﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;

namespace Flagsmith
{
    public class AnalyticsProcessor
    {
        const int AnalyticsTimer = 10;
        readonly string _AnalyticsEndPoint;
        readonly string _EnvironmentKey;
        readonly int _TimeOut;
        DateTime _LastFlushed;
        Dictionary<int, int> _AnalyticsData;
        HttpClient _HttpClient;

        public AnalyticsProcessor(HttpClient httpClient, string environmentKey, string baseApiUrl, int timeOut = 3)
        {
            _EnvironmentKey = environmentKey;
            _AnalyticsEndPoint = baseApiUrl;
            _TimeOut = timeOut;
            _LastFlushed = DateTime.Now;
            _AnalyticsData = new Dictionary<int, int>();
            _HttpClient = httpClient;
        }
        private async Task Flush()
        {
            if (_AnalyticsData?.Any() == false)
                return;
            var request = new HttpRequestMessage(HttpMethod.Post, _AnalyticsEndPoint)
            {
                Headers =
                {
                     { "X-Environment-Key", _EnvironmentKey }
                },
                Content = new StringContent(JsonConvert.SerializeObject(_AnalyticsData))
            };
            var tokenSource = new CancellationTokenSource();
            tokenSource.CancelAfter(TimeSpan.FromSeconds(_TimeOut));
            await _HttpClient.SendAsync(request, new CancellationTokenSource().Token);
            _AnalyticsData.Clear();
            _LastFlushed = DateTime.Now;
        }
        public async Task TrackFeature(int featureId)
        {
            _AnalyticsData[featureId] += 1;
            if ((DateTime.Now - _LastFlushed).Seconds > AnalyticsTimer)
                await Flush();
        }
    }
}
