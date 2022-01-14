using Flagsmith_engine.Models;
using System;
using System.Collections.Generic;

namespace Flagsmith_engine
{
    public class Engine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="environmentModel"></param>
        /// <param name="featureName"></param>
        /// <returns></returns>
        public List<FeatureStateModel> GetEnvironmentFeatureStates(EnvironmentModel environmentModel,string featureName)
        {
            //TODO
            return environmentModel.FeatureStates;
        }
    }
}
