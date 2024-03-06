
using Infrastructure.Common.Extensions;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence
{
    internal static class Startup
    {
        public static void ConfigurationServices(this IServiceCollection service, IConfiguration configuration, IHostEnvironment env)
        {
            service.AddSingleton(configuration.GetMyOptions<ApplicationDbSettings>());
            service.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetMyOptions<ConnectionStrings>().DefaultConnection,
                    opts => opts.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                if (env.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
            });

        }

        public static void Configure(IApplicationBuilder app, IConfiguration configuration)
        {
            Seed.DbInitializer.SeedDatabase(app, configuration);
        }
    }
}
