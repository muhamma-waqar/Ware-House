using Domain.Common;
using Domain.Common.Mass;
using Domain.Common.Money;
using Domain.Exceptions;
using Domain.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products
{
    public class Product : MyEntity
    {

        [Required]
        [StringLength(ProductInvariants.NameMaxLength)]
        public string Name { get; private set; }

        [Required]
        [StringLength(ProductInvariants.DescriptionMaxLength)]
        public string Description { get; private set; }

        [Required]
        public Money Price { get; private set; }

        [Required]
        public Mass Mass { get; private set; }

        public int NumberInStock { get; private set; }

        private Product()
        {
            (Name, Description, Mass, Price) = (null!, null!, null!, null!);
        }

        public Product(string name, string description,  Money price, Mass mass)
        {
            UpdateName(name);
            UpdateDescription(description);
            CheckMass(mass?.Value ?? throw new ArgumentNullException(nameof(mass)));
            CheckPrice(price?.Amount ?? throw new ArgumentNullException(nameof(price)));

            Mass = mass;
            Price = price;
            NumberInStock = 0;
        }

        public void UpdateMass(float value)
        {
            CheckMass(value);
            Mass = new Mass(value, Mass?.Unit ?? ProductInvariants.DefaultMassUnit);
        }

        public void UpdatePrice(decimal amount)
        {
            CheckPrice(amount);
            Price = new Money(amount, Price?.Currency ?? ProductInvariants.DefaultPriceCurrency);
        }

        public void UpdateName(string value) 
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name connot be empty");
            if (value.Length > ProductInvariants.NameMaxLength) throw new ArgumentException($"Length of value ({value.Length}) exceeds maximum name lenght ({ProductInvariants.NameMaxLength}).");
            Name = value;
        }

        [MemberNotNull(nameof(Description))]
        public void UpdateDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty.");
            if (value.Length > ProductInvariants.DescriptionMaxLength)
                throw new ArgumentException($"Length of value ({value.Length}) exceeds maximum description length ({ProductInvariants.NameMaxLength})");
            Description = value;
        }

        internal void RecordTransaction(TransactionLine transactionLine)
        {
            if (transactionLine.Quantity < 1)
                throw new ArgumentException("Product quantity in transactioin must be 1 or greater.");
            switch (transactionLine.Transaction.TransactionType)
            {
                case TransactionType.Sales:
                    if (transactionLine.Quantity > NumberInStock)
                        throw new  InsufficientStockException(this, transactionLine.Quantity, NumberInStock);
                    NumberInStock -= transactionLine.Quantity;
                    break;
                case TransactionType.Procurement:
                    NumberInStock += transactionLine.Quantity;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Unexpected {nameof(TransactionType)}: '{transactionLine.Transaction.TransactionType}'.");
            }
        }

        private static void CheckMass(float value)
        {
            if (value < ProductInvariants.MassMinimum)
                throw new ArgumentException($"Value '{value}' is smaller than the minimum required mass of {ProductInvariants.MassMinimum}.");
        }

        private static void CheckPrice(decimal amount)
        {
            if (amount < ProductInvariants.PriceMinimum)
                throw new ArgumentException($"Amount '{amount}' is smaller than the minimum required price of {ProductInvariants.MassMinimum}.");
        }
    }
}
