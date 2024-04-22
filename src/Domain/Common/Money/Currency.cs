using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Money
{
    public record Currency
    {
        public string Code { get; set; }
        public string Symbol { get; set; }
        public bool SymbolWellKnown { get; init; } = false;

        private Currency() { }

        private static readonly IReadOnlyDictionary<string, Record> _lookup = new Dictionary<string, Record>()
        {
            ["USD"] = new Record("USD", "$", symbolWellKnown: true),
            ["EUR"] = new Record("EUR", "€", symbolWellKnown: true),
            ["JPY"] = new Record("JPY", "¥", symbolWellKnown: true),
            ["GBP"] = new Record("GBP", "£", symbolWellKnown: true),
            ["CAD"] = new Record("CAD", "C$"),
            ["AUD"] = new Record("AUD", "A$"),
            ["CHF"] = new Record("CHF", "Fr."),
            ["NZD"] = new Record("NZD", "NZD$"),
            ["RUB"] = new Record("RUB", "₽"),
            ["HUF"] = new Record("HUF", "Ft"),
        };

        private Currency(Record record) => (Code, Symbol, SymbolWellKnown) = (record.Code, record.Symbol, record.SymbolWellKnown);

        public static Currency FromCode(string code)
        {
            if (!_lookup.TryGetValue(code.ToUpper(), out var record))
                throw new ArgumentException($"Code '{code}' is out a currency code we recognize.", nameof(code));
            return new Currency(record);
        }

        public static bool IsValidCurrencyCode(string code) => _lookup.ContainsKey(code);

        public static List<Currency> GetAllCurrencies => _lookup.Select(x => new Currency(x.Value)).ToList();

        public override string ToString() => Code;


        /// <summary>
        /// Returns an instance of the currency that is deemed default for the domain.
        /// </summary>
        public static Currency Default => USD;

        public static Currency USD = new Currency(_lookup["USD"]);
        public static Currency EUR = new Currency(_lookup["EUR"]);
        public static Currency JPY = new Currency(_lookup["JPY"]);
        public static Currency GBP = new Currency(_lookup["GBP"]);
        public static Currency CAD = new Currency(_lookup["CAD"]);
        public static Currency AUD = new Currency(_lookup["AUD"]);
        public static Currency CHF = new Currency(_lookup["CHF"]);
        public static Currency NZD = new Currency(_lookup["NZD"]);
        public static Currency RUB = new Currency(_lookup["RUB"]);
        public static Currency HUF = new Currency(_lookup["HUF"]);

        private readonly struct Record
        {
            public readonly string Code;
            public readonly string Symbol;
            public readonly bool SymbolWellKnown;

            public Record(string code, string symbol, bool symbolWellKnown = false) 
                =>(Code, Symbol, SymbolWellKnown) = (code, symbol, symbolWellKnown);
        }
    }
}
