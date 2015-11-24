using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal
{
    public class PointOfSaleProduct
    {
        public PointOfSaleProduct(string itemCode, IPriceCalculator priceCalculator)
        {
            ItemCode = itemCode;
            PriceCalculator = priceCalculator;
        }

        public string ItemCode { get; private set; }

        public IPriceCalculator PriceCalculator { get; private set; }
    }
}
