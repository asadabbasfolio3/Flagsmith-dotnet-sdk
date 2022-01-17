using Flagsmith_engine.Trait.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Identity.Models
{
    public class IdentityModel
    {
        public string IdentityUUID { get; set; }
        public string Identifier { get; set; }
        public string EnvironmentApiKey { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<TraitModel> IdentityTraits { get; set; }
        public string CompositeKey => GenerateCompositeKey(EnvironmentApiKey, Identifier);

        private string GenerateCompositeKey(string envKey, string identifier) => $"{envKey}_{identifier}";


    }
}
