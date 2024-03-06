using Application.Common.Dependencies.DataAccess.Repositories;
using AutoMapper;
using Domain.Transactions;
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
    internal class TransactionRepositoryEF : RepositoryBaseEF<Transaction>, ITransactionRepository
    {
        protected override IQueryable<Transaction> BaseQuery
            => _context.Transactions.Include(e => e.TransactionLines)
            .IgnoreQueryFilters();

        public TransactionRepositoryEF(ApplicationDbContext context, IMapper mapper):base(context, mapper) { }

        public override void Remove(Transaction entity)
            => _context.Remove(entity);

        public override void RemoveRange(IEnumerable<Transaction> entities)
            => _context.RemoveRange(entities);

        public async Task<Transaction?> GetEntireTransaction(int id)
            => await BaseQuery
            .Include(x => x.Partner)
            .Include(x => x.TransactionLines)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
