﻿using System;
using System.Collections.Generic;
using System.Text;
using Flagsmith_engine.Identity.Models;
using Xunit;
using Flagsmith_engine.Feature.Models;
using Flagsmith_engine.Trait.Models;
using Flagsmith_engine.Exceptions;
namespace EngineTest.Unit.Identities
{
    public class IdentityTest
    {
        [Fact]
        public void TestCompsiteKey()
        {
            var environmentApiKey = "abc123";
            var identifier = "identity";
            var identity = new IdentityModel { EnvironmentApiKey = environmentApiKey, Identifier = identifier };
            Assert.Equal($"{environmentApiKey}_{identifier}", identity.CompositeKey);
        }
        [Fact]
        public void TestIdentiyModelCreatesDefaultIdentityUUID()
        {
            IdentityModel identity = new IdentityModel { Identifier = "test", EnvironmentApiKey = "some_key" };
            Assert.NotEmpty(identity.IdentityUUID);
        }
        [Fact]
        public void TestGenerateCompositeKey()
        {
            var environmentApiKey = "abc123";
            var identifier = "identity";
            var identity = new IdentityModel();
            Assert.Equal($"{environmentApiKey}_{identifier}", identity.GenerateCompositeKey(environmentApiKey, identifier));
        }
        public void TestUpdateTraitsRemoveTraitsWithNoneValue(IdentityModel identityInSegment)
        {
            var traitKey = identityInSegment.IdentityTraits[0].TraitKey;
            var traitToRemove = new TraitModel { TraitKey = traitKey, TraitValue = null };
            identityInSegment.UpdateTraits(new List<TraitModel> { traitToRemove });
            Assert.Empty(identityInSegment.IdentityTraits);
        }
        public void TestUpdateIdentityTraitsUpdatesTraitValue(IdentityModel identityInSegment)
        {
            var traitKey = identityInSegment.IdentityTraits[0].TraitKey;
            var traitValue = "updated_trait_value";
            var traitToUpdate = new TraitModel { TraitKey = traitKey, TraitValue = traitValue };
            identityInSegment.UpdateTraits(new List<TraitModel> { traitToUpdate });
            Assert.Single(identityInSegment.IdentityTraits);
            Assert.Equal(traitToUpdate, identityInSegment.IdentityTraits[0]);
        }
        public void TestUpdateTraitsAddsNewTraits(IdentityModel identityInSegment)
        {

            var newTrait = new TraitModel { TraitKey = "new_key", TraitValue = "foobar" };
            identityInSegment.UpdateTraits(new List<TraitModel> { newTrait });
            Assert.Equal(2, identityInSegment.IdentityTraits.Count);
            Assert.Contains(newTrait, identityInSegment.IdentityTraits);
        }
        public void TestAppendingFeatureStatesRaisesDuplicateFeatureStateIfFsForTheFeatureAlreadyExists(IdentityModel identity, FeatureModel feature1)
        {
            var fs1 = new FeatureStateModel { Feature = feature1, Enabled = false };
            var fs2 = new FeatureStateModel { Feature = feature1, Enabled = true };
            try
            {
                identity.IdentityFeatures.Add(fs1);
            }
            catch (DuplicateFeatureState)
            {
                identity.IdentityFeatures.Add(fs2);
            }
        }
        public void TestAppendFeatureState(IdentityModel identity, FeatureModel feature1)
        {
            var fs1 = new FeatureStateModel { Feature = feature1, Enabled = false };
            identity.IdentityFeatures.Add(fs1);
            identity.IdentityFeatures.Contains(fs1);
        }
    }
}
