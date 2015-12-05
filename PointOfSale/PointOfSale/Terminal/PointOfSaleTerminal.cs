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

        public double CalculateTotal()
        {
            return DoCalculateTotal(0.0);
        }

        public double CalculateTotal(DiscountCard discountCard)
        {
            Contract.Requires(discountCard != null);

            double discountRate = discountCard.DiscountPercents / 100.0;
            discountCard.AddTotal(DoCalculateTotal());
            return DoCalculateTotal(discountRate);
        }

        private double DoCalculateTotal(double discountRate = 0.0)
        {
            return data.Aggregate(0.0, (a, pr) => a + pr.Value.GetTotalPrice(discountRate));
        }
    }
}
