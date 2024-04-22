using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Mass
{
    public record MassUnit : IEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float ConversionRateToGram { get; set; }

        public int Id {  get; set; }

        private MassUnit(string name, string symbol, float conversionRateToGram)
        {
            (Name,Symbol,ConversionRateToGram) = (name,symbol,conversionRateToGram);
        }

        public static readonly MassUnit Tonne = new("tonne", "t", 1000000f);
        public static readonly MassUnit Kilogram = new("kilogram", "kg", 1000f);
        public static readonly MassUnit Gram = new("gram", "g", 1f);
        public static readonly MassUnit Pound = new("pound", "lb", 453.59237f);

        public static MassUnit FromSymbol(string unitSymbol)
            => unitSymbol.ToLower() switch
            {
                "t" => Tonne,
                "kg" => Kilogram,
                "g" => Gram,
                "lb" => Pound,
                _ => throw new ArgumentException($"Uknown {nameof(MassUnit)} value '{unitSymbol}'.", nameof(unitSymbol))
            };
    }
}
