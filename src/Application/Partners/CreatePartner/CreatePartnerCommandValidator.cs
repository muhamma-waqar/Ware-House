using Domain.Partners;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.CreatePartner
{
    public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
    {
        public CreatePartnerCommandValidator() {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(PartnerInvariants.NameMaxLength);

            RuleFor(x => x.Address).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Address.Country).NotNull().MaximumLength(100);
                RuleFor(x => x.Address.ZipCode).NotNull().MaximumLength(100);
                RuleFor(x => x.Address.Street).NotNull().MaximumLength(100);
                RuleFor(x => x.Address.City).NotNull().MaximumLength(100);
            });
        }
    }
}
