using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal
{
    public class PointOfSaleProduct
    {
        public PointOfSaleProduct(string productCode, IPriceCalculator priceCalculator)
        {
            ProductCode = productCode;
            PriceCalculator = priceCalculator;
        }

        public string ProductCode { get; private set; }

        public IPriceCalculator PriceCalculator { get; private set; }
    }
}
