using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models.Feature
{
    public class FeatureStateModel
    {
        public FeatureModel Feature { get; set; }
        public bool Enabled { get; set; }
        public string FeatureStateUUID { get; set; }
        public string  Value { get; set; }
        public List<MultivariateFeatureStateValueModel> MultivariateFeatureStateValues { get; set; }
    }
}
