using FlagsmithEngine.Feature.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Flagsmith
{
    public class AnalyticFlag : Flag
    {
        [JsonIgnore]
        private readonly AnalyticsProcessor _AnalyticsProcessor;
        public AnalyticFlag(AnalyticsProcessor analyticsProcessor)
        {
            _AnalyticsProcessor = analyticsProcessor ?? throw new ArgumentNullException(nameof(analyticsProcessor));
        }
        public static AnalyticFlag FromFeatureStateModel(AnalyticsProcessor analyticsProcessor, FeatureStateModel featureStateModel, string identityId = null) =>
           new AnalyticFlag(analyticsProcessor)
           {
               feature = new Feature(featureStateModel.Feature.Id, featureStateModel.Feature.Name),
               enabled = featureStateModel.Enabled,
               value = featureStateModel.GetValue(identityId).ToString(),
           };

        public static List<AnalyticFlag> FromFeatureStateModel(AnalyticsProcessor analyticsProcessor, List<FeatureStateModel> featureStateModels, string identityId = null)
        {
            return featureStateModels.Select(f => FromFeatureStateModel(analyticsProcessor, f, identityId)).ToList();
        }
        public static List<AnalyticFlag> FromApiFlag(AnalyticsProcessor analyticsProcessor, List<Flag> flags)
        {
            return flags.Select(flag => new AnalyticFlag(analyticsProcessor)
            {
                enabled = flag.IsEnabled(),
                value = flag.GetValue(),
                feature = flag.GetFeature()
            }).ToList();
        }
        public override string GetValue()
        {
            _ = _AnalyticsProcessor.TrackFeature(featureId);
            return value;
        }
    }
}
