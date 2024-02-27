using Infrastructure.Common.Extensions;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seed
{
    static class DbInitializer
    {
        public static void SeedDatabase(IApplicationBuilder appBuilder, IConfiguration configuration)
        {
            using (var scope = appBuilder.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider;
                var setting = configuration.GetMyOptions<ApplicationDbSetting>();
            }
        }
    }
}
