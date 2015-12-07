using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal.PriceCalculations
{
    internal class SingleUnitPriceCalculator : IPriceCalculator
    {
        private readonly decimal singleUnitPrice;

        public SingleUnitPriceCalculator(decimal singleUnitPrice)
        {
            Contract.Requires(singleUnitPrice > 0.0M);

            this.singleUnitPrice = singleUnitPrice;
        }

        public decimal CalculatePrice(int itemsCount, decimal discountRate)
        {
            return singleUnitPrice * itemsCount * (1.0M - discountRate);
        }
    }
}
