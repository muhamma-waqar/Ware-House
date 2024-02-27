using Application.Common.Dependencies.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.ProductStockCount
{
    public record ProductStockCountQuey : IRequest<ProductStockCountDto>
    {
    }

    public class ProductStockCountQueryHandler : IRequestHandler<ProductStockCountQuey, ProductStockCountDto>
    {
        private readonly IStockStatisticsService _statisticsService;

        public ProductStockCountQueryHandler(IStockStatisticsService statisticsService) => _statisticsService = statisticsService;
        public async Task<ProductStockCountDto> Handle(ProductStockCountQuey request, CancellationToken cancellationToken)
        {
            var res = await _statisticsService.GetProductStockCounts();

            return new ProductStockCountDto
            {
                ProductCount = res.ProductCount,
                TotalStock = res.TotalStock
            };
        }
    }
}
