using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.ProductStockMass
{
    public record StockMassDto
    {
        public float Value { get; init; }
        public string Unit { get; init; } = null!;
    }
}
