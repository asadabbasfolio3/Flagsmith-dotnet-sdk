using Flagsmith_engine.Exceptions;
using Flagsmith_engine.Interfaces;
using Flagsmith_engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flagsmith_engine
{
    public class Engine:IEngine
    {
        /// <summary>
        /// Get a list of feature states for a given environment
        /// </summary>
        /// <param name="environmentModel">environment model object</param>
        /// <param name="featureName">name of a feature to get</param>
        /// <returns>feature-state model</returns>
        public FeatureStateModel GetEnvironmentFeatureState(EnvironmentModel environmentModel, string featureName)
        {
            try
            {
                return environmentModel.FeatureStates.FirstOrDefault(x => x.FeatureStateValue == featureName);
            }
            catch (Exception)
            {
                throw new FeatureStateNotFound();
            }
        }

        /// <summary>
        /// Get a specific feature state for a given feature_name in a given environment
        /// </summary>
        /// <param name="environmentModel">the environment model object</param>
        /// <returns>list of feature-state model</returns>
        public List<FeatureStateModel> GetEnvironmentFeatureStates(EnvironmentModel environmentModel)
        {
            if (environmentModel.Project.HideDisabledFlags)
            {
                return environmentModel.FeatureStates.FindAll(x => x.Enabled == true);
            }
            return environmentModel.FeatureStates;
        }
    }
}
