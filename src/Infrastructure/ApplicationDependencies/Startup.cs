using Application.Common.Dependencies.DataAccess;
using Application.Common.Dependencies.DataAccess.Repositories;
using Application.Common.Dependencies.Services;
using Infrastructure.ApplicationDependencies.DataAccess.Repositories;
using Infrastructure.ApplicationDependencies.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies
{
    internal static class Startup
    {
        public static void ConfigureServices(this IServiceCollection service, IConfiguration _)
        {
            service.AddScoped<IProductRepository, ProductRepositoryEF>();
            service.AddScoped<IPartnerRepository, PartnerRepositoryEF>();
            service.AddScoped<ITransactionRepository, TransactionRepositoryEF>();
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            service.AddTransient<IDateTime, DateTimeService>();
            service.AddTransient<IStockStatisticsService, StockStatisticsService>();
        }
    }
}
