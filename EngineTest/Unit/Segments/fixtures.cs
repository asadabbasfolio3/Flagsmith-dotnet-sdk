using Flagsmith_engine.Segment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineTest.Unit.Segments
{
    public class fixtures
    {
        public static string TraitKey1 = "email";
        public static string TraitValue1 = "user@example.com";

        public static string TraitKey2 = "num_purchase";
        public static string TraitValue2 = "12";

        public static string TraitKey3 = "date_joined";
        public static string TraitValue3 = "2021-01-01";
        public static SegmentModel emptySegment = new SegmentModel() { Id = 1, Name = "empty_segment" };
    }
}
