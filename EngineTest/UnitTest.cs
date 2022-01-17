using Flagsmith_engine.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace EngineTest
{
    public class UnitTests
    {
        [Theory]
        [MemberData(nameof(ExtractTestCases),parameters: @"\TestEngineData\Data\environment_n9fbf9h3v4fFgH3U3ngWhb.json")]
        public void Test_Engine(EnvironmentModel environment_model,IdentityModel IdentityModel,Response response)
        {
            //TODO
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
                var response = item["response"].ToObject<IdentityModel>();
                testCases.Add(new object[] { environment_model, identity_model, response });
            }
            return testCases;
        }
    }
}
