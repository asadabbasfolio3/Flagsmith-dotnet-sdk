using System;
using System.Collections.Generic;
using System.Text;
using Flagsmith_engine.Segment.Models;
using System.Linq;
namespace Flagsmith_engine.Segment.Models
{
    public class SegmentRuleModel
    {
        public string Type { get; set; }
        public List<SegmentRuleModel> Rules { get; set; }
        public List<SegmentConditionModel> Conditions { get; set; }

        public bool MatchingFunction(List<bool> list)
        {
            switch (Type)
            {
                case Constants.AllRule: return list.All(x => x);
                case Constants.AnyRule: return list.Any(x => x);
                case Constants.NoneRule: return !list.Any(x => x);
            }
            throw new Exception("Rule Not Found");
        }
    }
}
