using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using FlagsmithEngine.Utils;
using System.Linq;
using System.Runtime.Serialization;
using FlagsmithEngine.Exceptions;
using FlagsmithEngine.Interfaces;
namespace FlagsmithEngine.Feature.Models
{
    public class FeatureStateModel
    {
        public Hashing hashing = new Hashing();
        [JsonProperty(PropertyName = "feature")]
        public FeatureModel Feature { get; set; }
        [JsonProperty(PropertyName = "enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("feature_state_value")]
        public object Value { get; set; }
        [JsonProperty(PropertyName = "multivariate_feature_state_values")]
        public List<MultivariateFeatureStateValueModel> MultivariateFeatureStateValues { get; set; }
        [JsonProperty(PropertyName = "django_id")]
        public int DjangoId { get; set; }
        [JsonProperty("featurestate_uuid")]
        public string FeatureStateUUID { get; set; } = Guid.NewGuid().ToString();
        public object GetValue(string identityId = null) =>
            identityId != null && MultivariateFeatureStateValues?.Count > 0 ? GetMultivariateValue(identityId.ToString()) : Value;
        public object GetMultivariateValue(string identityId)
        {
            var percentageValue = hashing.GetHashedPercentageForObjectIds(new List<string>
            {
              DjangoId != 0 ? DjangoId.ToString() : FeatureStateUUID,
              identityId.ToString()
            });
            var startPercentage = 0.0;
            foreach (var myValue in MultivariateFeatureStateValues.OrderBy(m => m.Id))
            {
                var limit = myValue.PercentageAllocation + startPercentage;
                if (startPercentage <= percentageValue && percentageValue < limit)
                    return myValue.MultivariateFeatureOption.Value;
                startPercentage = limit;
            }
            return Value;
        }
        [OnSerialized()]
        private void ValidatePercentageAllocations(StreamingContext _)
        {
            var totalAllocation = MultivariateFeatureStateValues?.Sum(m => m.PercentageAllocation);
            if (totalAllocation > 100)
                throw new InvalidPercentageAllocation("Total percentage allocation should not be more than 100");
        }
    }
}
