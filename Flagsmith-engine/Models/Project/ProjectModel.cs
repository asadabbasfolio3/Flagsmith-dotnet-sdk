﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Flagsmith_engine.Models.Segment;
namespace Flagsmith_engine.Models
{
    public class ProjectModel
    {
        [JsonProperty(PropertyName = "hide_disabled_flags")]
        public bool HideDisabledFlags { get; set; }
        [JsonProperty(PropertyName = "segments")]
        public List<SegmentModel> Segments { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "organisation")]
        public Organization Organization { get; set; }
    }
}
