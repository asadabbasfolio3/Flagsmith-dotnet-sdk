using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models
{
    public class Segment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SegmentRuleModel> Rules { get; set; }
        public List<FeatureStateModel> FeatureStates { get; set; }
    }
}
