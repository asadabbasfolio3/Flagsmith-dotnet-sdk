using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models
{
    public class EnvironmentModel
    {
        public int ID { get; set; }
        public string ApiKey { get; set; }
        public ProjectModel Project { get; set; }
        public List<FeatureStateModel> FeatureStates { get; set; }
        public IntegrationModel amplitudeConfig { get; set; }
        public IntegrationModel SegmentConfig { get; set; }
        public IntegrationModel MixpanelConfig { get; set; }
        public IntegrationModel HeapConfig { get; set; }

    }
}
