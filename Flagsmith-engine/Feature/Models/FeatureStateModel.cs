using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Feature.Models
{
    public class FeatureStateModel
    {
        [JsonProperty(PropertyName = "feature")]
        public FeatureModel Feature { get; set; }
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }
        public string Value { get; set; }
        [JsonProperty(PropertyName = "multivariate_feature_state_values")]
        public List<MultivariateFeatureStateValueModel> MultivariateFeatureStateValues { get; set; }
        [JsonProperty(PropertyName = "django_id")]
        public int DjangoId { get; set; }
        public string FeatureStateUUID { get; set; }
    }
}
