using Application.Common.Dependencies.Services;
using Domain.Common.Mass;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.ProductStockMass
{
    public record ProductStockMassQuery : IRequest<StockMassDto>
    {
    }

    public class ProductStockMassQueryHandler : IRequestHandler<ProductStockMassQuery, StockMassDto>
    {
        private readonly IStockStatisticsService _statisticsService;

        public ProductStockMassQueryHandler(IStockStatisticsService statisticsService) => _statisticsService = statisticsService;

        public async Task<StockMassDto> Handle(ProductStockMassQuery request, CancellationToken cancellationToken)
        {
            var mass = await _statisticsService.GetProductStockTotalMass(MassUnit.Tonne);

            return new StockMassDto()
            {
                Value = mass.Value,
                Unit = mass.Unit.Symbol
            };
        }
    }
}
