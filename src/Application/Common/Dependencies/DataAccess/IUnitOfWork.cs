using Application.Common.Dependencies.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.DataAccess
{
    public interface IUnitOfWork
    {
        public IPartnerRepository Partners { get; }
        public IProductRepository Products { get; }
        public ITransactionRepository Transactions { get; } 
        bool HasActiveTransaction { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        public Task SaveChanges();
    }
}
