using Application.Common.Dependencies.DataAccess.Repositories.Common;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetHeaviestProducts(int numberOfProducts);
        Task<List<Product>> GetMostStockedProducts(int numberOfProducts);
    }
}
