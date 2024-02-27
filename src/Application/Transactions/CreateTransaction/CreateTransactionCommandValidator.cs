using Application.Common.Dependencies.DataAccess.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.CreateTransaction
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        private readonly IProductRepository _productRepository;
        public CreateTransactionCommandValidator(IProductRepository productRepository) 
        {
            this._productRepository = productRepository;
            RuleFor(x => x.TransactionLines)
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleForEach(x => x.TransactionLines)
                    .Must(l => l.ProductQuantity > 0)
                    .WithMessage("Product quantity at line {CollectionIndex} must be larger than 0");

                    RuleFor(x => x.TransactionLines)
                    .Must(x => x.GroupBy(x => x.ProductId).Any(g => g.Count() == 1))
                    .WithMessage("Can't have than one transactioin lines for the same product.");

                    RuleFor(x => x.TransactionLines)
                    .MustAsync(HaveSufficientStock)
                    .WithMessage("Cannot record a sales transaction of quantity {RequestedQty} for product '{ProductName}', because current stock is {ProductStock}.");
                });
        }

        private async Task<bool> HaveSufficientStock(CreateTransactionCommand c, CreateTransactionCommand.TransactionLine[] lines, ValidationContext<CreateTransactionCommand> ctx, CancellationToken _)
        {
            if(c.TransactionType == Domain.Transactions.TransactionType.Procurement)
            {
                return true;
            }

            var requestProducts = await _productRepository.GetFiltered(x => lines.Select(l => l.ProductId).Contains(x.Id));

            foreach(var line in lines)
            {
                var product = requestProducts.Where(p => p.Id == line.ProductId).Single();

                if(product.NumberInStock < line.ProductQuantity)
                {
                    ctx.MessageFormatter.AppendArgument("ProductName", product.Name);
                    ctx.MessageFormatter.AppendArgument("ProductStock", product.NumberInStock);
                    ctx.MessageFormatter.AppendArgument("RequestedQty", line.ProductQuantity);
                    return false;
                }
            }

            return true;
        }
    }
}
