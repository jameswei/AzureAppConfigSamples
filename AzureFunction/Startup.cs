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
using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

[assembly: FunctionsStartup(typeof(AzureFunction.Startup))]

namespace AzureFunction
{
    class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder.AddAzureAppConfiguration(options =>
            {
                options.Connect(Environment.GetEnvironmentVariable("ConnectionString"))
                       // Load all keys that start with `AzureFunction:`
                       // support key filter expression
                       .Select("AzureFunction:*")
                       .ConfigureRefresh(refreshOptions =>
                          {
                              // Configure to reload configuration if the registered 'Version' key is modified
                              refreshOptions.Register("AzureFunction:Settings:Version", LabelFilter.Null, true);
                              // set interval as 5 minutes
                              refreshOptions.SetCacheExpiration(TimeSpan.FromMinutes(5));
                          });
            });
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddAzureAppConfiguration();
        }
    }
}