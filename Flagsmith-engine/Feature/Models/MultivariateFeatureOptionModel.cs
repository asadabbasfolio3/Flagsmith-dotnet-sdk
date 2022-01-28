using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace FlagsmithEngine.Feature.Models
{
    public class MultivariateFeatureOptionModel
    {
        public int Id { get; set; }
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
