using Domain.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(ProductInvariants.NameMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(ProductInvariants.DescriptionMaxLength);

            RuleFor(x => x.MassValue)
                .GreaterThanOrEqualTo(ProductInvariants.MassMinimum);

            RuleFor(x => x.PriceAmount)
                .GreaterThanOrEqualTo(ProductInvariants.PriceMinimum);
        }
    }
}
