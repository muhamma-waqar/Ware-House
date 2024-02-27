using Application.Common.Dependencies.DataAccess;
using Application.Common.Dependencies.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ApplicationDependencies.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly Application

        public IPartnerRepository Partners { get; }

        public IProductRepository Products { get; }

        public ITransactionRepository Transactions { get; }

        public bool HasActiveTransaction => _currentTransaction is not null;

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task RollbackTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
