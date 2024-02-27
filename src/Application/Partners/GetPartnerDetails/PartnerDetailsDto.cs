using Application.Common.Mapping;
using AutoMapper;
using Domain.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.GetPartnerDetails
{
    public record PartnerDetailsDto : IMapForm<Partner>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;   
        public DateTime CreateAt { get; init; }
        public DateTime? LastModifiedAt { get; init; }

        public AddressDto Address { get; init; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Partner, PartnerDetailsDto>();
            profile.CreateMap<Address, AddressDto>();
        }

        public record AddressDto
        {
            public string Country { get; init; } = null!;
            public string ZipCode { get; init; } = null!;
            public string City { get; init; } = null!;
            public string Street { get; init; } = null!;    
        }
    }
}
