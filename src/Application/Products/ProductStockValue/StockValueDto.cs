using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.ProductStockValue
{
    public record StockValueDto
    {
        public decimal Amount { get; init; }
        public string CurrencyCode { get; init; } = null!;
    }
}
