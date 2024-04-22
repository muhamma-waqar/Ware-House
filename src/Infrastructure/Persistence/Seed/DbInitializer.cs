using Google.Apis.Logging;
using Infrastructure.Common.Extensions;
using Infrastructure.Identity.Model;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleData;
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
                var setting = configuration.GetMyOptions<ApplicationDbSettings>();
                try
                {
                    var context = service.GetRequiredService<ApplicationDbContext>();
                    if(setting.AutoMigrate == true && context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }

                    if(setting.AutoSeed == true)
                    {
                        SeedDefaultUser(service, configuration.GetMyOptions<UserSeedSettings>());
                        SeedSampleData(context);
                    }
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
                    logger.LogError(ex, "An error occured while migration or seeding the database.");

                    throw;
                }
            }
        }

        private static void SeedDefaultUser(IServiceProvider service, UserSeedSettings settings)
        {
            if (!settings.SeedDefaultUser)
                return;

            using (var userManager = service.GetRequiredService<UserManager<ApplicationUser>>())
            {
                if (!userManager.Users.Any(u => u.UserName == settings.DefaultUsername))
                {
                    var defaultUser = new ApplicationUser { UserName = settings.DefaultUsername, Email = settings.DefaultEmail };
                    var result = userManager.CreateAsync(defaultUser, settings.DefaultPassword).GetAwaiter().GetResult();

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Default user creation failed with the following errors:"
                            + result.Errors.Aggregate("", (sum, err) => sum += $"{Environment.NewLine} - {err.Description}"));
                    }
                }
            }
        }

        private static void SeedSampleData(ApplicationDbContext context)
        {
            if(!context.Partners.Any())
            {
                var (products, partner) = DataGenerator.GenerateBaseEntities();

                context.Partners.AddRange(partner);
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            if (!context.Transactions.Any())
            {
                var products = context.Products.ToList();
                var partners = context.Partners.ToList();

                var transactionToGenerate = 103;
                for(int i = 0; i < transactionToGenerate; i++)
                {
                    context.Transactions.Add(
                        DataGenerator.GenerateTransaction(partners, products));
                    context.SaveChanges();
                }
            }
        }

    }
}
