using Flagsmith_engine.Models.Feature;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models
{
    public class FeatureStateModel
    {
        [JsonProperty(PropertyName = "feature_state_value")]
        public string FeatureStateValue { get; set; }
        [JsonProperty(PropertyName = "multivariate_feature_state_values")]
        public List<object> MultivariateFeatureStateValues { get; set; }
        [JsonProperty(PropertyName = "django_id")]
        public int DjangoId { get; set; }
        [JsonProperty(PropertyName = "feature")]
        public FeatureModel Feature { get; set; }
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }
    }
}
