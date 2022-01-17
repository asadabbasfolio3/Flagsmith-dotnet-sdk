﻿using Flagsmith_engine.Models;
using Flagsmith_engine.Models.Environment;
using Flagsmith_engine.Models.Feature;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Interfaces
{
    public interface IEngine
    {
        List<FeatureStateModel> GetEnvironmentFeatureStates(EnvironmentModel environmentModel);
        FeatureStateModel GetEnvironmentFeatureState(EnvironmentModel environmentModel, string featureName);
    }
}