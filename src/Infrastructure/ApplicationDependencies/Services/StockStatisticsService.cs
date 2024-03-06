using Application.Common.Dependencies.Services;
using Domain.Common.Mass;
using Domain.Common.Money;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies.Services
{
    class StockStatisticsService : IStockStatisticsService
    {
        private readonly ApplicationDbContext _dbcontext;
        public StockStatisticsService(ApplicationDbContext dbContext)
            => _dbcontext = dbContext;

        public async Task<(int ProductCount, int TotalStock)> GetProductStockCounts()
        {
            var res = await _dbcontext.Products
                .GroupBy(x => 1)
                .Select(g => new
                {
                    productCount = g.Count(),
                    totalStock = g.Sum(p => p.NumberInStock)
                }).SingleAsync();

            return (res.productCount, res.totalStock);
        }

        public async Task<Mass> GetProductStockTotalMass(MassUnit unit)
        {
            var totalMassPerUnit = await _dbcontext.Products
                .GroupBy(x => x.Mass.Unit, p => new
                {
                    p.Mass,
                    p.NumberInStock
                })
                .Select(g => new
                {
                    MassUnit = g.Key,
                    TotalMass = g.Sum(x => x.Mass.Value * x.NumberInStock)
                }).ToListAsync();

            var totalMass = totalMassPerUnit
                .Select(g => new Mass(g.TotalMass, g.MassUnit))
                .Sum(mass => mass.ConvertTo(unit).Value);

            return new Mass(totalMass, unit);
        }


        public async Task<Money> GetProductStockTotalValue()
        {
            var stockValuesPerCurrency = await _dbcontext.Products
                .GroupBy(x => x.Price.Currency, p => new
                {
                    UnitPrice = p.Price.Amount,
                    Currency = p.Price.Currency,
                    NumberInStock = p.NumberInStock
                })
                .Select(g => new
                {
                    Currency = g.Key,
                    TotalValue = g.Sum(x => x.UnitPrice * x.NumberInStock)
                }).ToListAsync();

            if (stockValuesPerCurrency.Count > 1)
                throw new InvalidOperationException(
                    $"Operation connot be completed, because not all product prices use the same currency. Distinct currencies deteched:" +
                    $"{string.Join(", ", stockValuesPerCurrency.Select(x => x.Currency.Code))}.");
            
            var stockValue  = stockValuesPerCurrency.First();
            return new Money(stockValue.TotalValue, Currency.FromCode(stockValue.Currency.Code));


        }

    }
}
