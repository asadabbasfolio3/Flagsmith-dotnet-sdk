using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Flagsmith_engine.Segment.Models
{
    public class SegmentConditionModel
    {

        public string Operator { get; set; }
        public string Value { get; set; }
        public string Property { get; set; }

        public bool EvaluateNotContains(string traitValue) => !traitValue.Contains(Value);
        public bool EvaluateRegex(string traitValue) => Regex.Match(traitValue, Value).Success;

    }
}
