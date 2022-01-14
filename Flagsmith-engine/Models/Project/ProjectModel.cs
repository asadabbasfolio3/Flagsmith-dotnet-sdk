using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models
{
   public class ProjectModel
    {
        [JsonProperty(PropertyName = "hide_disabled_flags")]
        public bool hide_disabled_flags { get; set; }
        //[JsonProperty(PropertyName = "segments")]
        //public List<Segment> segments { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "organisation")]
        public Organization organization { get; set; }
    }
}
