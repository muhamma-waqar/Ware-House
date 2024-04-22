using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Partners
{
    public record Address : IEntity
    {

        [Required, MinLength(1), MaxLength(100)]
        public string Street { get; init; }

        [Required, MinLength(1), MaxLength(100)]
        public string City { get; init; }

        [Required, MinLength(1), MaxLength(100)]
        public string Country { get; init; }

        [Required, MinLength(1), MaxLength(100)]
        public string ZipCode { get; init; }

        public int Id {  get; init; }

        private Address() 
        {
            Street = City = Country = ZipCode = default!;
        }

        // TODO: Add validation and constraints
        public Address(string street, string city, string country, string zipCode)
            => (Street, City, Country, ZipCode) = (street, city, country, zipCode);

        public override string ToString() => $"{Street}, {ZipCode}, {City}, {Country}";
    }
}
