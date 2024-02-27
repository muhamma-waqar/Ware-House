using Application.Common.Dependencies.DataAccess.Repositories.Common;
using Domain.Transactions;

namespace Application.Common.Dependencies.DataAccess.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction> GetEntireTransaction(int id);
    }
}
