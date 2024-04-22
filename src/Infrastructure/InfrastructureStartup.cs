using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureStartup
    {
        public static void AdMyInfrastructureConfiguration(this IConfigurationBuilder configurationBuilder, HostBuilderContext context)
        {
            AzureKeyVault.Startup.ConfigureAppConfiguration(context, configurationBuilder);
        }

        public static void AddMyInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            //Identity.Startup.ConfigueService(services, configuration);
            //Authentication.Startup.ConfigureServices(services, configuration);
            Persistence.Startup.ConfigurationServices(services, configuration, env);
            ApplicationDependencies.Startup.ConfigureServices(services, configuration);
        }

        public static void UseMyInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
        {
            //Authentication.Startup.Configure(app);
            Persistence.Startup.Configure(app, configuration);
        }
    }
}
