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
        private readonly double singleUnitPrice;

        public SingleUnitPriceCalculator(double singleUnitPrice)
        {
            Contract.Requires(singleUnitPrice > 0.0);

            this.singleUnitPrice = singleUnitPrice;
        }

        public double CalculatePrice(int itemsCount, double discountRate)
        {
            return singleUnitPrice * itemsCount * (1.0 - discountRate);
        }
    }
}
