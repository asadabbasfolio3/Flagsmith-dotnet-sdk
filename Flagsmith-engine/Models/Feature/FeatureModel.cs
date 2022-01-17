﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models.Feature
{
   public class FeatureModel
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
