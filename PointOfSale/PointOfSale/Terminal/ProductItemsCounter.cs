using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal
{
    internal class ProductItemsCounter
    {
        private readonly IPriceCalculator priceCalculator;
        private int itemsCount = 0;

        public ProductItemsCounter(IPriceCalculator priceCalculator)
        {
            this.priceCalculator = priceCalculator;
        }

        public void AddItem()
        {
            itemsCount++;
        }

        public double GetTotalPrice()
        {
            return priceCalculator.CalculatePrice(itemsCount);
        }
    }
}
