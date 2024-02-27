using Domain.Partners;
using Domain.Products;
using Domain.Transactions;
using SampleData.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleData
{
    public static class DataGenerator
    {
        public static (List<Product>, List<Partner>) GenerateBaseEntities()
        {
            var products = SampleProducts.GenerateSampleProducts(76);
            var partners = SamplePartners.GetSamplePartners();

            return (products, partners);
        }

        public static Transaction GenerateTransaction(IReadOnlyList<Partner> partners, IReadOnlyList<Product> products) => SampleTransactions.GenerateTransaction(partners, products);
    }
}
