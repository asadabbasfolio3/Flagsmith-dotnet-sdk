using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Flagsmith_engine.Segment;
using Flagsmith_engine.Segment.Models;
using Flagsmith_engine.Trait.Models;
using Flagsmith_engine.Identity.Models;

namespace EngineTest.Unit.Segments
{
    public class SegmentEvaluatorTest
    {
        [Theory]
        [MemberData(nameof(TestCasesSegmentCondition))]
        public void TestSegmentConditionMatchesTraitValue(string _operator, object traitValue, string conditionValue, bool expectedResult)
        {
            SegmentConditionModel conditionModel = new SegmentConditionModel { Operator = _operator, Value = conditionValue, Property = "foo" };
            Assert.Equal(expectedResult, Evaluator.MatchesTraitValue(traitValue, conditionModel));
        }
        public static IEnumerable<object[]> TestCasesSegmentCondition() =>
            new List<object[]>
            {
                new object[]{ Constants.Equal, "bar", "bar", true }
            };
        [Theory]
        [MemberData(nameof(TestCasesIdentityInSegment))]
        public void TestIdentityInSegment(SegmentModel segment, List<TraitModel> identityTraits, bool expectedResult)
        {
            var identity = new IdentityModel()
            {
                Identifier = "foo",
                IdentityTraits = identityTraits,
                EnvironmentApiKey = "api-key",
            };
            Assert.Equal(expectedResult, Evaluator.EvaluateIdentityInSegment(identity, segment, null));
        }
        public static IEnumerable<object[]> TestCasesIdentityInSegment()
        {
            return new List<object[]> { new object[] { fixtures.emptySegment, new List<TraitModel>(), false } };
        }
    }
}
