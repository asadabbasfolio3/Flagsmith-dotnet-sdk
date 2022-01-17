using System;
using System.Collections.Generic;
using System.Text;
using Flagsmith_engine.Models.Feature;
namespace Flagsmith_engine.Models.Segment
{
    public class SegmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SegmentRuleModel> Rules { get; set; }

        public List<FeatureStateModel> FeatureStates { get; set; }
    }
}
