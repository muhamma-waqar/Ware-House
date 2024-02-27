using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.ProductStockCount
{
    public record ProductStockCountDto
    {
        public int ProductCount { get; set; }
        public int TotalStock {  get; set; }
    }
}
