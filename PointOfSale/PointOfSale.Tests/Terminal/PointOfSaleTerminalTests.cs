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
        public void ScanProductsSequence_WithDefaultPricesSet_CorrectTotalValue(string productCodes, decimal expectedResult)
        {
            var terminal = new PointOfSaleTerminal(
                new ProductsPriceSetBuilder()
                    .AddProduct("A", 1.25M, 3, 3.0M)
                    .AddProduct("B", 4.25M)
                    .AddProduct("C", 1.0M, 6, 5.0M)
                    .AddProduct("D", 0.75M)
                    .GetAllProducts());

            ScanStringAsChars(terminal, productCodes);

            AssertDecimalEquals(expectedResult, terminal.CalculateTotal());
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
        public void ApplyDiscountCard_WithDefaultDiscountCard_CorrectTotalValue(int productsCount, decimal expectedResult)
        {
            var terminal = new PointOfSaleTerminal(
                new ProductsPriceSetBuilder()
                    .AddProduct(DefaultProductCode.ToString(), 100.0M, 3, 200.0M)
                    .GetAllProducts());
            var discountCard = CreateDefaultDiscountCard();

            ScanStringAsChars(terminal, new string(DefaultProductCode, productsCount));

            AssertDecimalEquals(expectedResult, terminal.CalculateTotal(discountCard));
        }


        [TestCase(1, 99.0)]
        [TestCase(9, 99.0)]
        [TestCase(10, 95.0)]
        [TestCase(100, 90.0)]
        public void DiscountRateChanges_WithDefaultDiscountCard_CorrectTotalValue(int productsCount, decimal expectedResult)
        {
            var discountCard = CreateDefaultDiscountCard();
            Func<int, decimal> scanItems = count =>
                {
                    var terminal = new PointOfSaleTerminal(
                        new ProductsPriceSetBuilder()
                            .AddProduct(DefaultProductCode.ToString(), 100.0M)
                            .GetAllProducts());
                    ScanStringAsChars(terminal, new string(DefaultProductCode, count));
                    return terminal.CalculateTotal(discountCard);
                };

            scanItems(productsCount);
            decimal actualResult = scanItems(1);

            AssertDecimalEquals(expectedResult, actualResult);
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
                    new DiscountPoint(1000.0M, 5),
                    new DiscountPoint(10000.0M, 10),
                });
        }

        private void AssertDecimalEquals(decimal expected, decimal actual)
        {
            Assert.AreEqual((double)expected, (double)actual, Delta);
        }
    }
}
