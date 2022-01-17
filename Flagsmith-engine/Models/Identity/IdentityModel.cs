﻿using Flagsmith_engine.Models.Identity.Trait;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flagsmith_engine.Models.Identity
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
