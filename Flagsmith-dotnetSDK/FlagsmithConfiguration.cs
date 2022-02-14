using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
namespace Flagsmith
{
    public class FlagsmithConfiguration
    {
        public FlagsmithConfiguration()
        {
            ApiUrl = "https://api.flagsmith.com/api/v1/";
            EnvironmentKey = string.Empty;
            UseLegacyIdentities = true;
        }

        public string ApiUrl { get; set; }
        public string EnvironmentKey { get; set; }
        public bool UseLegacyIdentities { get; set; }
        public bool EnableClientSideEvaluation { get; set; }
        public int EnvironmentRefreshIntervalSeconds { get; set; } = 60;
        public Func<string, Flag> DefaultFlagHandler;
        public ILogger Logger { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ApiUrl) && !string.IsNullOrEmpty(EnvironmentKey);
        }
    }
}
