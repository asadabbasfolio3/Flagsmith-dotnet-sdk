using System.Linq;
using System.Collections.Generic;
using Flagsmith_engine.Models.Feature;
using Flagsmith_engine.Models.Environment;
using Flagsmith_engine.Exceptions;
using Flagsmith_engine.Models.Identity;
using Flagsmith_engine.Models.Identity.Trait;
using Flagsmith_engine.Models.Segment;
using System;

namespace Flagsmith_engine
{
    public class Engine
    {
        public List<FeatureStateModel> GetEnvironmentFeatureStates(EnvironmentModel environmentModel) =>
            environmentModel.Project.HideDisabledFlags ? environmentModel.FeatureStates.Where(fs => fs.Enabled).ToList() : environmentModel.FeatureStates;

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
