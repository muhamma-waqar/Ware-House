using Domain.Common.Money;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Transactions
{
    public class TransactionLine
    {
        public int Id { get; private set; }

        [Required]
        public int ProductId { get; init; }
        public virtual Product Product { get; init; } = null!;

        [Required]
        public Transaction Transaction { get; init; } = null!;

        [Range(1, int.MaxValue)]
        public int Quantity { get; init; }

        [Required]
        public Money UnitPrice { get; init; } = null!;

        internal TransactionLine() { }
    }
}
