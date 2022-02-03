using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Flagsmith
{
    public class AnalyticsProcessor
    {
        readonly string _AnalyticsEndPoint;
        string _EnvironmentKey, _LastFlushed;
        int _TimeOut;
        Dictionary<int, int> _AnalyticsData;
        public AnalyticsProcessor(string environmentKey, string baseApiUrl, int timeOut = 3)
        {
            _EnvironmentKey = environmentKey;
            _AnalyticsEndPoint = baseApiUrl;
            _TimeOut = timeOut;
            _AnalyticsData = new Dictionary<int, int>();
        }
        private Task Flush()
        {
            if (_AnalyticsData?.Any() == false)
                return Task.CompletedTask;
            throw new NotImplementedException();
        }
        public void TrackFeature(int featureId)
        {
            throw new NotImplementedException();
        }
    }
}
