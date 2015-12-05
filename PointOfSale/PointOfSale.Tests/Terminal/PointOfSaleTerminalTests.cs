using System;
using PointOfSale.Terminal;
using System.Collections.Generic;
using NUnit.Framework;
using PointOfSale.Terminal.DiscountCalculations;

namespace PointOfSale.Tests.Terminal
{
    [TestFixture]
    public class PointOfSaleTerminalTests
    {
        private const double Delta = 1e-5;
        private const char DefaultProductCode = 'A';

        [TestCase("", 0.0)]
        [TestCase("A", 1.25)]
        [TestCase("AA", 2.5)]
        [TestCase("AAA", 3.0)]
        // Test cases from test statement
        [TestCase("ABCDABA", 13.25)]
        [TestCase("CCCCCCC", 6.0)]
        [TestCase("ABCD", 7.25)]
        public void ScanProductsSequence_WithDefaultPricesSet_CorrectTotalValue(string productCodes, double expectedResult)
        {
            var terminal = new PointOfSaleTerminal(
                new ProductsPriceSetBuilder()
                    .AddProduct("A", 1.25, 3, 3.0)
                    .AddProduct("B", 4.25)
                    .AddProduct("C", 1.0, 6, 5.0)
                    .AddProduct("D", 0.75)
                    .GetAllProducts());

            ScanStringAsChars(terminal, productCodes);

            Assert.AreEqual(expectedResult, terminal.CalculateTotal(), Delta);
        }

        [Test]
        public void ScanNonExistingProduct_WithEmptyPricesSet_ExceptionThrown()
        {
            var terminal = new PointOfSaleTerminal(new PointOfSaleProduct[0]);

            Assert.Throws(typeof(ArgumentException), () => terminal.Scan("A"));
        }

        [TestCase(1, 99.0)]
        [TestCase(3, 200.0)]
        [TestCase(4, 299.0)]
        public void ApplyDiscountCard_WithDefaultDiscountCard_CorrectTotalValue(int productsCount, double expectedResult)
        {
            var terminal = new PointOfSaleTerminal(
                new ProductsPriceSetBuilder()
                    .AddProduct(DefaultProductCode.ToString(), 100.0, 3, 200.0)
                    .GetAllProducts());
            var discountCard = CreateDefaultDiscountCard();

            ScanStringAsChars(terminal, new string(DefaultProductCode, productsCount));

            Assert.AreEqual(expectedResult, terminal.CalculateTotal(discountCard), Delta);
        }


        [TestCase(1, 99.0)]
        [TestCase(9, 99.0)]
        [TestCase(10, 95.0)]
        [TestCase(100, 90.0)]
        public void DiscountRateChanges_WithDefaultDiscountCard_CorrectTotalValue(int productsCount, double expectedResult)
        {
            var discountCard = CreateDefaultDiscountCard();
            Func<int, double> scanItems = count =>
                {
                    var terminal = new PointOfSaleTerminal(
                        new ProductsPriceSetBuilder()
                            .AddProduct(DefaultProductCode.ToString(), 100.0)
                            .GetAllProducts());
                    ScanStringAsChars(terminal, new string(DefaultProductCode, count));
                    return terminal.CalculateTotal(discountCard);
                };

            scanItems(productsCount);
            double actualResult = scanItems(1);

            Assert.AreEqual(expectedResult, actualResult, Delta);
        }
        

        private void ScanStringAsChars(PointOfSaleTerminal terminal, string productCodes)
        {
            foreach(char ch in productCodes)
            {
                terminal.Scan(ch.ToString());
            }
        }

        private DiscountCard CreateDefaultDiscountCard()
        {
            return new DiscountCard(1, new[] 
                {
                    new DiscountPoint(1000.0, 5),
                    new DiscountPoint(10000.0, 10),
                });
        }
    }
}
