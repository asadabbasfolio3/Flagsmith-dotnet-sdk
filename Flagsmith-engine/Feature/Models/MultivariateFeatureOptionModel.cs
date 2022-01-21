using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Flagsmith_engine.Feature.Models
{
    public class MultivariateFeatureOptionModel
    {
        public int Id { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
