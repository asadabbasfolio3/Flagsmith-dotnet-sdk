using System.Linq;
using System.Collections.Generic;
using Flagsmith_engine.Models.Feature;
using Flagsmith_engine.Models.Environment;
using Flagsmith_engine.Exceptions;
using Flagsmith_engine.Models.Identity;
using Flagsmith_engine.Models.Identity.Trait;
using System;
using Flagsmith_engine.Interfaces;
using Flagsmith_engine.Utils;

namespace Flagsmith_engine
{
    public class Engine : IEngine
    {
        /// <summary>
        /// Get a list of feature states for a given environment
        /// </summary>
        /// <param name="environmentModel">the environment model object</param>
        /// <returns>list of feature-state model</returns>
        public List<FeatureStateModel> GetEnvironmentFeatureStates(EnvironmentModel environmentModel) =>
            environmentModel.Project.HideDisabledFlags ? environmentModel.FeatureStates.Where(fs => fs.Enabled).ToList() : environmentModel.FeatureStates;
        /// <summary>
        /// Get a specific feature state for a given feature_name in a given environment
        /// </summary>
        /// <param name="environmentModel">environment model object</param>
        /// <param name="featureName">name of a feature to get</param>
        /// <returns>feature-state model</returns>
        public FeatureStateModel GetEnvironmentFeatureState(EnvironmentModel environmentModel, string featureName)
        {
            var featureState = environmentModel.FeatureStates.FirstOrDefault(fs => fs.Feature.Name == featureName);
            if (featureState == null)
                throw new FeatureStateNotFoundException();
            return featureState;
        }
        public List<FeatureStateModel> GetIdentityFeatureStates(EnvironmentModel environmentModel, IdentityModel identity, string featureName, List<TraitModel> overrideTraits)
        {
            throw new NotImplementedException();
        }
        public List<FeatureStateModel> GetIdentityFeatureState(EnvironmentModel environmentModel, IdentityModel identity, string featureName, List<TraitModel> overrideTraits)
        {
            var identitySegments = Evaluator.GetIdentitySegments(environmentModel, identity, overrideTraits);
            throw new NotImplementedException();
        }
    }
}
