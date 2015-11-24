using PointOfSale.Terminal.PriceCalculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Terminal
{
    public class ProductsPriceSetBuilder
    {
        private readonly List<PointOfSaleProduct> products = new List<PointOfSaleProduct>();

        public ProductsPriceSetBuilder AddProduct(string productCode, double singleUnitPrice)
        {
            DoAddProduct(productCode, new SingleUnitPriceCalculator(singleUnitPrice));
            return this;
        }

        public ProductsPriceSetBuilder AddProduct(string productCode, double singleUnitPrice, int volumeSize, double volumePrice)
        {
            DoAddProduct(productCode, new ComplexPriceCalculator(singleUnitPrice, volumeSize, volumePrice));
            return this;
        }

        public PointOfSaleProduct[] GetAllProducts()
        {
            return products.ToArray();
        }

        private void DoAddProduct(string productCode, IPriceCalculator priceCalculator)
        {
            products.Add(new PointOfSaleProduct(productCode, priceCalculator));
        }
    }
}
