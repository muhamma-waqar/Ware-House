using Application.Common.Mapping;
using AutoMapper;
using Domain.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Partners.GetPartnersList
{
    public record PartnerDto : IMapForm<Partner>
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string Address { get; init; } = null!;
        public string Country { get; init; } = null!;
        public string ZipCode { get; init; } = null!;
        public string City { get; init; } = null!;
        public string Street { get; init; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Partner, PartnerDto>()
                .ForMember(dest => dest.Country, x => x.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.ZipCode, x => x.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.City, x => x.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, x => x.MapFrom(src => src.Address.Street));
        }
    }
}
