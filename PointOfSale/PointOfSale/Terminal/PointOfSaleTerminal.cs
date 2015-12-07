using PointOfSale.Terminal.DiscountCalculations;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal
{
    public class PointOfSaleTerminal
    {
        private readonly Dictionary<string, ProductItemsCounter> data;

        public PointOfSaleTerminal(IEnumerable<PointOfSaleProduct> products)
        {
            Contract.Requires(products != null);
            Contract.Requires(products.Select(p => p.ProductCode).Distinct().Count() == products.Count(), 
                              "Single product entry for each product code");

            data = products.ToDictionary(p => p.ProductCode, p => new ProductItemsCounter(p.PriceCalculator));
        }

        public void Scan(string productCode)
        {
            if (!data.ContainsKey(productCode))
            {
                throw new ArgumentException("Unknown product code");
            }

            data[productCode].AddItem();
        }

        public decimal CalculateTotal()
        {
            return DoCalculateTotal();
        }

        public decimal CalculateTotal(DiscountCard discountCard)
        {
            Contract.Requires(discountCard != null);

            decimal discountRate = discountCard.DiscountPercents / 100.0M;
            discountCard.AddTotal(DoCalculateTotal());
            return DoCalculateTotal(discountRate);
        }

        private decimal DoCalculateTotal(decimal discountRate = 0.0M)
        {
            return data.Aggregate(0.0M, (a, pr) => a + pr.Value.GetTotalPrice(discountRate));
        }
    }
}
