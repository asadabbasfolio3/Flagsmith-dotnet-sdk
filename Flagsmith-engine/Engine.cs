using Flagsmith_engine.Models;
using System;
using System.Collections.Generic;

namespace Flagsmith_engine
{
    public class Engine
    {
        public List<FeatureStateModel> GetEnvironmentFeatureStates(EnvironmentModel environmentModel,string featureName)
        {
            //TODO
            return environmentModel.FeatureStates;
        }
    }
}
