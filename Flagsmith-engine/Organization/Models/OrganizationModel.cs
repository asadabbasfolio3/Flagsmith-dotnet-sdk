using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Organization.Models
{
   public class OrganizationModel
    {
        public bool PersistTraitData { get; set; }
        public string Name { get; set; }
        public bool FeatureAnalytics { get; set; }
        public bool StopServingFlags { get; set; }
        public int Id { get; set; }
    }
}
