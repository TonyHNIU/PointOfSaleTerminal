using System;
using PointOfSale.Terminal;
using System.Collections.Generic;
using NUnit.Framework;

namespace PointOfSale.Tests.Terminal
{
    [TestFixture]
    public class PointOfSaleTerminalTests
    {
        private const double Delta = 1e-5;

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

        private void ScanStringAsChars(PointOfSaleTerminal terminal, string productCodes)
        {
            foreach(char ch in productCodes)
            {
                terminal.Scan(ch.ToString());
            }
        }
    }
}
