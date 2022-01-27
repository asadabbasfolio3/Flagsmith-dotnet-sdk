using System;
using System.Collections.Generic;
using System.Text;
using Flagsmith_engine.Feature.Models;
using System.Linq;
using Flagsmith_engine.Exceptions;
namespace Flagsmith_engine.Identity.Models
{
    public class IdentityFeaturesList : List<FeatureStateModel>
    {
        public new void Add(FeatureStateModel model)
        {
            if (this.Any(m => m.Feature.Id == model.Feature.Id))
                throw new DuplicateFeatureState();
            base.Add(model);
        }
    }
}
