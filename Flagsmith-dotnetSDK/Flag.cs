using Newtonsoft.Json;
using FlagsmithEngine.Feature.Models;
using FlagsmithEngine.Identity.Models;
using System.Collections.Generic;
using System.Linq;

namespace Flagsmith
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Flag
    {
        [JsonProperty]
        protected int featureId;
        [JsonProperty]
        protected Feature feature = null;

        [JsonProperty]
        protected bool enabled = false;

        [JsonProperty("feature_state_value")]
        protected string value = null;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public Feature GetFeature()
        {
            return feature;
        }

        public bool IsEnabled()
        {
            return enabled;
        }

        public virtual string GetValue()
        {
            return value;
        }



    }
}
