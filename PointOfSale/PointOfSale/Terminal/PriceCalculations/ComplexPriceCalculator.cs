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
        private readonly decimal volumePrice;
        private readonly int volumeSize;

        public ComplexPriceCalculator(decimal singleUnitPrice, int volumeSize, decimal volumePrice)
        {
            Contract.Requires(volumeSize > 0);
            Contract.Requires(volumePrice > 0.0M);

            singleUnitCalculator = new SingleUnitPriceCalculator(singleUnitPrice);
            this.volumeSize = volumeSize;
            this.volumePrice = volumePrice;
        }

        public decimal CalculatePrice(int itemsCount, decimal discountRate)
        {
            return (itemsCount / volumeSize) * volumePrice + singleUnitCalculator.CalculatePrice(itemsCount % volumeSize, discountRate);
        }
    }
}
