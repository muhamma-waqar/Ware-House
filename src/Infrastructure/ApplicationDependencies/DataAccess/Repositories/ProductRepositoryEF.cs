using Application.Common.Dependencies.DataAccess.Repositories;
using Application.Common.Dependencies.DataAccess.Repositories.Common;
using AutoMapper;
using Domain.Products;
using Infrastructure.ApplicationDependencies.DataAccess.Repositories.Common;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies.DataAccess.Repositories
{
    internal class ProductRepositoryEF : RepositoryBaseEF<Product>, IProductRepository
    {
        protected override IQueryable<Product> BaseQuery
            => _context.Products.Include(x => x.Mass);

        public ProductRepositoryEF(ApplicationDbContext context, IMapper mapper): base(context, mapper) { }

        public Task<List<Product>> GetHeaviestProducts(int numberOfProducts)
            => BaseQuery
            .OrderByDescending(x => x.Mass)
            .Take(numberOfProducts)
            .ToListAsync();

        public Task<List<Product>> GetMostStockedProducts(int numberOfProducts)
            => BaseQuery.OrderByDescending(x => x.NumberInStock)
            .Take(numberOfProducts)
            .ToListAsync();

        public override void Remove(Product entity)
        {
            _context.Remove(entity);
        }

        public override void RemoveRange(IEnumerable<Product> entities)
        {
            foreach(var e in entities)
                this.Remove(e);
        }
    }
}
