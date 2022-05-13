//===============================================================================
// Microsoft FastTrack for Azure
// Azure App Configuration Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFunction
{
    // AzFunc class
    public class AppConfigFunction
    {
        private readonly IConfiguration _configuration;
        // enable push mode by IConfigurationRefresher
        private readonly IConfigurationRefresher _configurationRefresher;
        
        public AppConfigFunction(IConfiguration configuration, IConfigurationRefresherProvider refresherProvider)
        {
            _configuration = configuration;
            _configurationRefresher = refresherProvider.Refreshers.First();
        }

        // AzFunc implementation with a TimerTrigger at minute 0 of every hour.
        [FunctionName("AppConfigFunction")]
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"Function [{nameof(AppConfigFunction)}] triggered at {DateTime.Now}");

            await _configurationRefresher.TryRefreshAsync();
            // Fetch config value at each trigger or we can scheduledly pull from app configuration
            log.LogInformation($"Version {_configuration.GetValue<string>("AzureFunction:Settings:Version")}");
            // read another config key
            log.LogInformation($"Blob Connection String {_configuration.GetValue<string>("AzureFunction:Settings:BlobConnectionString")}");
        }
    }
}
