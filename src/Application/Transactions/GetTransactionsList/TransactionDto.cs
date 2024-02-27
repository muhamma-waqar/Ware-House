using Application.Common.Mapping;
using Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.GetTransactionsList
{
    public record TransactionDto : IMapForm<Transaction>
    {
        public int Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public int TransactionType { get; init; }
        public string PartnerName { get; init; } = null!;

        public decimal TotalAmount { get; init; }
        public string TotalCurrencyCode { get; init; } = null!;
    }
}
