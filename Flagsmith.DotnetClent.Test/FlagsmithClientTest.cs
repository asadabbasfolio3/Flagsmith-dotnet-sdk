﻿using FlagsmithEngine.Environment.Models;
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
using Moq;

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
        protected override async Task<Flags> GetFeatureFlagsFromApi()
        {
            var flags = await base.GetFeatureFlagsFromApi();
            totalFucntionCalls[nameof(GetFeatureFlagsFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetFeatureFlagsFromApi), out int i) ? i + 1 : 1;
            return flags;
        }
        public async Task TriggerEnvironmentUpdate() => await this.GetAndUpdateEnvironmentFromApi();
        protected override async Task<Flags> GetIdentityFlagsFromApi(string identity)
        {
            var identityResponse = JsonConvert.DeserializeObject<Identity>(await GetJSON(HttpMethod.Get, Fixtures.ApiUrl));
            totalFucntionCalls[nameof(GetIdentityFlagsFromApi)] = totalFucntionCalls.TryGetValue(nameof(GetIdentityFlagsFromApi), out int i) ? i + 1 : 1;
            return Flags.FromApiFlag(null, configuration.DefaultFlagHandler, identityResponse.flags);

        }
        protected override Flags GetIdentityFlagsFromDocuments(string identifier, List<Trait> traits)
        {
            //var flags = new List<Flag> { new Flag(new Feature(1, "some_feature"), true, "some_value") };
            var flags = base.GetIdentityFlagsFromDocuments(identifier, traits);
            totalFucntionCalls[nameof(GetIdentityFlagsFromDocuments)] = totalFucntionCalls.TryGetValue(nameof(GetIdentityFlagsFromDocuments), out int i) ? i + 1 : 1;
            return flags;
        }
        protected override Flags GetFeatureFlagsFromDocuments()
        {
            var flags = base.GetFeatureFlagsFromDocuments();
            totalFucntionCalls[nameof(GetFeatureFlagsFromDocuments)] = totalFucntionCalls.TryGetValue(nameof(GetFeatureFlagsFromDocuments), out int i) ? i + 1 : 1;
            return flags;
        }
        public void MockHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = httpResponseMessage.StatusCode,
                    Content = httpResponseMessage.Content
                }));
            httpClient = httpClientMock.Object;
        }
        public void MockHttpThrowConnectionError()
        {
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpRequestException());
            httpClient = httpClientMock.Object;
        }

        private void _initDict()
        {
            if (totalFucntionCalls == null)
                totalFucntionCalls = new Dictionary<string, int>();
        }


    }
}
