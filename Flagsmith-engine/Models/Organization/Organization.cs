using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models
{
   public class Organization
    {
        public bool persist_trait_data { get; set; }
        public string name { get; set; }
        public bool feature_analytics { get; set; }
        public bool stop_serving_flags { get; set; }
        public int id { get; set; }
    }
}
