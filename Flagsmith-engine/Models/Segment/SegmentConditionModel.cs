using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models.Segment
{
    public class SegmentConditionModel
    {
        public string Operator { get; set; }
        public string Value { get; set; }
        public string Property { get; set; }
    }
}
