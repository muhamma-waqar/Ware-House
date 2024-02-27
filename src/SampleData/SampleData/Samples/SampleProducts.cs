using Domain.Common.Mass;
using Domain.Common.Money;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleData.Samples
{
    internal static class SampleProducts
    {
        private static readonly string[] _namePrefixes = { "Bio-electric", "Neural", "Isograted", "Isolinear", "Microdyne", "Phylum", "Matterstream", "Transwarp", "Plasma", "Holographic", "Temporal", "Antimatter", "Dark matter", "Quantum", "Hydrogen", "Biogel", "RNA", "Void" };
        private static readonly string[] _nameSuffixes = { "Cable", "Diode", "Transponder", "Inducer", "Coupler", "Relay", "Coil", "Scanner", "Vacillator", "Inhibitor", "Oscillator", "Generator", "Inducer", "Reductor", "Splicer", "Transmuter", "Orchestrator", "Analyzer", "Doodad" };
        private static int MaximumNumber => _namePrefixes.Length * _nameSuffixes.Length;

        private static readonly string[] _descriptionPrefixes = { "Manages the", "Controls the", "Enhances the", "Distributes the", "Transforms the", "Acts as a governor in the", "Experimental version. Ensures the", "Plays a stabilizing role pertaining to the", "Quantifiably transposes the" };
        private static readonly string[] _descriptionJoiners = { "interaction of", "flow of", "connections between", "transfusions of", "intricate interconnections within", "seaming reagents of", "surrogate gyroconnections over" };
        private static readonly string[] _descriptionSuffixes = { "advanced micro circuits", "superconductive neural agents", "parallel quantum particles", "manifold dermal quantifiers", "charged stellar remains", "vorachodric micro-fitted interval dischargers", "exometric and telokinetic nano-engines", "tubular and oxogenic micoplasmosis" };

        private static readonly Random _rnd = new();

        internal static List<Product> GenerateSampleProducts(int number)
        {
            if(number > MaximumNumber)
            {
                throw new ArgumentException($"Maximum {MaximumNumber} unique products can be generated.", nameof(number));
            }

            var uniqueNames = new HashSet<String>(number);
            while (uniqueNames.Count < number)
            {
                uniqueNames.Add(GetName());
            }

            return uniqueNames.Select(name => new Product(name:name,description: GetDescription(), price: new Money(_rnd.Next(10,999) + 0.99M, Currency.Default),mass: new Mass(_rnd.Next(10,200) * 0.1f, MassUnit.Kilogram))).ToList();

            string GetName() => $"{GetRandom(_descriptionPrefixes)} {GetRandom(_nameSuffixes)}";

            string GetDescription() => $"{GetRandom(_descriptionPrefixes)} {GetRandom(_descriptionPrefixes)} {GetRandom(_descriptionSuffixes)}";

            string GetRandom(string[] arr)  => arr[_rnd.Next(arr.Length)];

        }

    }
}
