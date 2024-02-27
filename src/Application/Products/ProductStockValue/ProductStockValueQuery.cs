using Application.Common.Dependencies.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.ProductStockValue
{
    public record ProductStockValueQuery : IRequest<StockValueDto>
    {
    }

    public class ProductStockValueQueryHandler : IRequestHandler<ProductStockValueQuery, StockValueDto>
    {
        private readonly IStockStatisticsService _statisticsService;
        public ProductStockValueQueryHandler(IStockStatisticsService statisticsService) => _statisticsService = statisticsService;

        public async Task<StockValueDto> Handle(ProductStockValueQuery request, CancellationToken cancellationToken)
        {
            var totalStockValue = await _statisticsService.GetProductStockTotalValue();

            return new StockValueDto()
            {
                Amount = totalStockValue.Amount,
                CurrencyCode = totalStockValue.Currency.Code
            };
        }
    }
}
