using Flagsmith_engine.Models;
using Flagsmith_engine.Environment.Models;
using Flagsmith_engine.Identity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Flagsmith_engine.Interfaces;
using System.Linq;

namespace EngineTest
{
    public class EngineTest
    {
        private IEngine _iengine;
        EngineTest(IEngine iEngine)
        {
            _iengine = iEngine;
        }
        [Theory]
        [MemberData(nameof(ExtractTestCases),parameters: @"\TestEngineData\Data\environment_n9fbf9h3v4fFgH3U3ngWhb.json")]
        public void Test_Engine(EnvironmentModel environmentModel,IdentityModel IdentityModel,Response response)
        {
            var engineResponse = _iengine.GetIdentityFeatureStates(environmentModel, IdentityModel);

            var sortedEngineflags = engineResponse.OrderBy(x => x.Feature.Name);
            var sortedApiFlags = response.flags.OrderBy(x => x.feature.Name);

            Assert.Equal(sortedEngineflags.Count(),sortedApiFlags.Count());

            foreach (var featureState in sortedApiFlags)
            {
               //TODO
            }
        }
        public static JObject LoadData(string path)
        {
            path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + path;
            using (StreamReader r = new StreamReader(path))
            {
                return JObject.Parse(r.ReadToEnd());
            }
        }
        public static IEnumerable<object[]> ExtractTestCases(string path)
        {
            var testCases = new List<object[]>();
            var test_data = LoadData(path);
            var environment_model = test_data["environment"].ToObject<EnvironmentModel>();
            foreach (var item in test_data["identities_and_responses"])
            {
                var identity_model = item["identity"].ToObject<IdentityModel>();
                var response = item["response"].ToObject<Response>();
                testCases.Add(new object[] { environment_model, identity_model, response });
            }
            return testCases;
        }
    }
}
