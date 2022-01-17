using Flagsmith_engine.Models.Environment;
using Flagsmith_engine.Models.Identity;
using Flagsmith_engine.Models.Identity.Trait;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flagsmith_engine.Models.Segment
{
    public static class Evaluator
    {

        public static List<SegmentModel> GetIdentitySegments(EnvironmentModel environmentModel, IdentityModel identity, List<TraitModel> overrideTraits)
            => environmentModel.Project.Segments.Where(s => EvaluateIdentityInSegment(identity, s, overrideTraits)).ToList();

        public static bool EvaluateIdentityInSegment(IdentityModel identity, SegmentModel segment, List<TraitModel> overrideTraits)
        {
            var traits = overrideTraits.Any() ? overrideTraits : identity.IdentityTraits;
            return segment.Rules.Any() && segment.Rules.All(rule => TraitsMatchSegmentRule(traits, rule, segment.Id.ToString(), identity.CompositeKey));
        }
        public static bool TraitsMatchSegmentRule(List<TraitModel> identityTraits, SegmentRuleModel rule, string segemntId, string identityId)
        {
            throw new NotImplementedException();
        }
        public static bool TraitsMatchSegmentCondition(List<TraitModel> identityTraits, SegmentConditionModel condition, string segemntId, string identityId)
        {
            throw new NotImplementedException();
        }
    }
}
