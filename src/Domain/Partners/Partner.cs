using Domain.Common;
using Domain.Products;
using Domain.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Partners
{
    public class Partner : MyEntity
    {
        [Required]
        [StringLength(PartnerInvariants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public Address Address { get; private set; }

        public virtual IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();
        private List<Transaction> _transactions = new();

        private Partner()
        {
            Name = default!;
            Address = default!;
        }

        public Partner(string name, Address address)
        {
           this.UpdateName(name);
            this.UpdateAddress(address);

        }

        public Transaction SellTo(IEnumerable<(Product product, int quantity)> items)
            => CreateTransaction(items, TransactionType.Sales);

        public Transaction ProcureFrom(IEnumerable<(Product product, int quantity)> items)
            => CreateTransaction(items, TransactionType.Procurement);

        [MemberNotNull()]
        public void UpdateName(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");

            if (value.Length > PartnerInvariants.NameMaxLength)
                throw new ArgumentException($"Length of value ({value.Length}) exceeds maximum name length ({ProductInvariants.NameMaxLength}).");

            Name = value;
        }

        [MemberNotNull(nameof(Address))]
        public void UpdateAddress(Address address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        private Transaction CreateTransaction(IEnumerable<(Product product, int quantity)> items, TransactionType transactionType)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));
            if (!items.Any() || items.Any(x => x.product == null || x.quantity < 1))
                throw new ArgumentException("List of items must be a non-empty list of non-null products and quantities of at least 1.", nameof(items));

            var transation  = new Transaction(
                type: transactionType,
                partner: this
                );

            foreach( var (product , quantity) in items)
            {
                transation.AddTransactionLine(product, quantity);
            }
            _transactions.Add(transation);
            return transation;
        }
    }
}
