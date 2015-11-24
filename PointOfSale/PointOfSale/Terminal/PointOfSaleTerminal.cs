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

            data = products.ToDictionary(p => p.ItemCode, p => new ProductItemsCounter(p.PriceCalculator));
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
            return data.Aggregate(0.0, (a, pr) => a + pr.Value.GetTotalPrice());
        }
    }
}
