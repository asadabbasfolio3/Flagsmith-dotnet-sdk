using System;
using System.Collections.Generic;
using System.Text;
using Flagsmith_engine.Models.Segment;
namespace Flagsmith_engine.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Oraganization { get; set; }
        public bool HideDisabledFlags { get; set; }
        public List<SegmentModel> Segments { get; set; }
    }
}
