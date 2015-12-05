using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal.PriceCalculations
{
    internal class ComplexPriceCalculator : IPriceCalculator
    {
        private readonly SingleUnitPriceCalculator singleUnitCalculator;
        private readonly double volumePrice;
        private readonly int volumeSize;

        public ComplexPriceCalculator(double singleUnitPrice, int volumeSize, double volumePrice)
        {
            Contract.Requires(volumeSize > 0);
            Contract.Requires(volumePrice > 0.0);

            singleUnitCalculator = new SingleUnitPriceCalculator(singleUnitPrice);
            this.volumeSize = volumeSize;
            this.volumePrice = volumePrice;
        }

        public double CalculatePrice(int itemsCount, double discountRate)
        {
            return (itemsCount / volumeSize) * volumePrice + singleUnitCalculator.CalculatePrice(itemsCount % volumeSize, discountRate);
        }
    }
}
