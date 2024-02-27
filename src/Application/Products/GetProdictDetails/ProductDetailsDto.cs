using Application.Common.Mapping;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetProdictDetails
{
    public record ProductDetailsDto : IMapForm<Product>
    {
        public int Id { get; set; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public DateTime CreateAt { get; init; }
        public string CreatedBy { get; init; } = null!;
        public DateTime? LastModifiedAt { get; init; }
        public string? LastModifiedBy { get; init; }
        public decimal PriceAmount { get; init; }
        public string PriceCurrencyCode { get; init; } = null!;
        public float MassValue { get; init; }
        public string MassUnitSymbol { get; init; } = null!;
        public int NumberInStock { get; init; }
    }
}
