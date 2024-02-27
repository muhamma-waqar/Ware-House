using Application.Common.Dependencies.DataAccess;
using Domain.Partners;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.CreatePartner
{
    public record CreatePartnerCommand : IRequest<int>
    {
        public string Name { get; init; } = null!;

        public AddressDto Address { get; init; } = null!;
        public record AddressDto
        {
            public string Country { get; init; } = null!;
            public string ZipCode { get; init; } = null!;
            public string Street { get; init; } = null!;
            public string City { get; init; } = null!;
        }
    }

    public class CreatePartnerCommandHadler : IRequestHandler<CreatePartnerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePartnerCommandHadler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = new Partner(
                name: request.Name.Trim(),
                address: new Address(
                    country: request.Address.Country.Trim(),
                    zipCode: request.Address.ZipCode.Trim(),
                    street: request.Address.Street.Trim(),
                    city: request.Address.City.Trim()
                    ));
            _unitOfWork.Partners.Add(partner);
            await _unitOfWork.SaveChanges();
            return partner.Id;
        }
    }
}
