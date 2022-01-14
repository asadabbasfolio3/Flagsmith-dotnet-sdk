using Newtonsoft.Json;
using System.Collections.Generic;

namespace Flagsmith_engine.Models
{
    public class EnvironmentModel
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "api_key")]
        public string ApiKey { get; set; }
        [JsonProperty(PropertyName = "project")]
        public ProjectModel Project { get; set; }
        [JsonProperty(PropertyName = "feature_states")]
        public List<FeatureStateModel> FeatureStates { get; set; }
        public IntegrationModel amplitudeConfig { get; set; }
        public IntegrationModel SegmentConfig { get; set; }
        public IntegrationModel MixpanelConfig { get; set; }
        public IntegrationModel HeapConfig { get; set; }

    }
}
