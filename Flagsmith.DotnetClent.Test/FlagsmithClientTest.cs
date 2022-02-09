using FlagsmithEngine.Environment.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace Flagsmith.DotnetClient.Test
{
    internal class FlagsmithClientTest : FlagsmithClient
    {
        Dictionary<string, int> totalFucntionCalls;
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
            await Task.Delay(0);
            var flags = JsonConvert.DeserializeObject<List<Flag>>(Fixtures.ApiFlagResponse);
            totalFucntionCalls[nameof(GetFeatureFlagsFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetFeatureFlagsFromApi), out int i) ? i + 1 : 1;
            return flags;
        }
        public async Task TriggerEnvironmentUpdate() => await this.GetAndUpdateEnvironmentFromApi();

        private void _initDict()
        {
            if (totalFucntionCalls == null)
                totalFucntionCalls = new Dictionary<string, int>();
        }


    }
}
