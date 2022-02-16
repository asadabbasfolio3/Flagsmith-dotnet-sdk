using FlagsmithEngine.Environment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using FlagsmithEngine.Trait.Models;
using System.Linq;

namespace Flagsmith.DotnetClient.Test
{
    internal class FlagsmithClientTest : FlagsmithClient
    {
        Dictionary<string, int> totalFucntionCalls;
        public Func<Task<string>> GetJsonOutput { get; set; }
        public FlagsmithClientTest(FlagsmithConfiguration flagsmithConfiguration) : base(flagsmithConfiguration)
        {
            _initDict();
        }
        public int this[string functionName] => totalFucntionCalls.GetValueOrDefault(functionName);

        public bool IsEnvironmentEmpty() => Environment == null;

        public bool IsEnvironmentEqual(EnvironmentModel environment) => Environment == environment;
        protected async override Task GetAndUpdateEnvironmentFromApi()
        {
            await Task.Delay(0);
            Environment = Fixtures.Environment;
            _initDict();
            totalFucntionCalls[nameof(GetAndUpdateEnvironmentFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetAndUpdateEnvironmentFromApi), out int i) ? i + 1 : 1;
        }
        protected override async Task<List<Flag>> GetFeatureFlagsFromApi()
        {
            GetJsonOutput ??= () => Task.FromResult(Fixtures.ApiFlagResponse);
            var flags = JsonConvert.DeserializeObject<List<Flag>>(await GetJSON(null, null));
            totalFucntionCalls[nameof(GetFeatureFlagsFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetFeatureFlagsFromApi), out int i) ? i + 1 : 1;
            return flags;
        }
        public async Task TriggerEnvironmentUpdate() => await this.GetAndUpdateEnvironmentFromApi();
        protected override async Task<List<Flag>> GetIdentityFlagsFromApi(string identity)
        {
            GetJsonOutput ??= () => Task.FromResult(Fixtures.ApiIdentityResponse);
            var identityResponse = JsonConvert.DeserializeObject<Identity>(await GetJsonOutput());
            totalFucntionCalls[nameof(GetIdentityFlagsFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetIdentityFlagsFromApi), out int i) ? i + 1 : 1;
            return identityResponse.flags;

        }
        protected override List<Flag> GetIdentityFlagsFromDocuments(string identifier, List<TraitModel> traits)
        {
            var flags = new List<Flag> { new Flag(new Feature(1, "some_feature"), true, "some_value") };
            totalFucntionCalls[nameof(GetIdentityFlagsFromDocuments)] = totalFucntionCalls.TryGetValue(nameof(GetIdentityFlagsFromDocuments), out int i) ? i + 1 : 1;
            return flags;
        }
        protected override List<Flag> GetFeatureFlagsFromDocuments()
        {
            totalFucntionCalls[nameof(GetFeatureFlagsFromDocuments)] = totalFucntionCalls.TryGetValue(nameof(GetFeatureFlagsFromDocuments), out int i) ? i + 1 : 1;
            return Fixtures.Environment.FeatureStates.Select(x => new Flag(new Feature(x.Feature.Id, x.Feature.Name), x.Enabled, x.GetValue()?.ToString())).ToList();
        }
        protected override async Task<string> GetJSON(HttpMethod method, string url, string body = null)
        {
            string output = string.Empty;
            try
            {
                output = await GetJsonOutput?.Invoke() ?? string.Empty;
            }
            finally
            {
                GetJsonOutput = null;
            }
            return output;
        }

        private void _initDict()
        {
            if (totalFucntionCalls == null)
                totalFucntionCalls = new Dictionary<string, int>();
        }


    }
}
