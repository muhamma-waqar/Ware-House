using Azure.Identity;
using Infrastructure.AzureKeyVault.Settings;
using Infrastructure.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AzureKeyVault
{
    internal static class Startup
    {
        public static void ConfigureAppConfiguration(HostBuilderContext _, IConfigurationBuilder configurationBuilder)
        {
            var settings = configurationBuilder.Build().GetMyOptions<AzureKeyVaultSettings>();

            if (settings is not null && settings.AddToConfiguration)
            {
                configurationBuilder.AddAzureKeyVault(new Uri(settings.ServiceUrl), new DefaultAzureCredential());
            }
        }
    }
}
