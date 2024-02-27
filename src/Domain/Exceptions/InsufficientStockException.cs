using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Products;

namespace Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public string ProductName { get; }
        public int RequestedQuantity { get; }
        public int ActualQuantity { get; }

        public InsufficientStockException(Product product, int requestedQuantity, int actualQuantity) :
            base($"Quantity requested for sale ({requestedQuantity}) from product '{product.Name}' exceeds number i stock ({actualQuantity}).")
        {
            (ProductName, RequestedQuantity, ActualQuantity) = (product.Name, requestedQuantity, actualQuantity);
        }
    }
}
