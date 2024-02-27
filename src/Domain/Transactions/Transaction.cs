using Domain.Common;
using Domain.Common.Money;
using Domain.Partners;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Transactions
{
    public class Transaction : MyEntity
    {
        public TransactionType TransactionType { get; private set; }

        [Required]
        public Money Total { get; private set; } = new Money(0, Currency.USD);
        public int PartnerId { get; private set; }  
        public virtual Partner Partner { get; private set; }

        public virtual IReadOnlyCollection<TransactionLine> TransactionLines => _transactionsLines.AsReadOnly();

        private readonly List<TransactionLine> _transactionsLines = new();

        private Transaction()
        {
            Partner = null!;
        }

        internal Transaction(TransactionType type, Partner partner)
        {
            TransactionType = type;
            Partner = partner;
        }

        internal void AddTransactionLine(Product product, int quantity) 
        {
            if(product == null) throw new ArgumentNullException(nameof(product));
            if(quantity <1)
                throw new ArgumentException("Value must be equal to or greather than 1.", nameof(quantity));

            var transactionLine = new TransactionLine()
            {
                Transaction = this,
                Product = product,
                Quantity = quantity,
                UnitPrice = product.Price.Copy()
            };

            product.RecordTransaction(transactionLine);

            _transactionsLines.Add(transactionLine);

            var currency = _transactionsLines.First().UnitPrice.Currency;
            Total = TransactionLines.Aggregate(new Money(0, currency),
                (total, line) => total + (line.UnitPrice * line.Quantity));
        }

        
    }
}
