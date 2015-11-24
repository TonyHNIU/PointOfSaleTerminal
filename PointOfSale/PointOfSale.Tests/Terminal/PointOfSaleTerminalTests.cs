using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSale.Terminal;
using System.Collections.Generic;

namespace PointOfSale.Tests.Terminal
{
    [TestClass]
    public class PointOfSaleTerminalTests
    {
        const double Delta = 1e-5;

        [TestMethod]
        public void TestMethod1()
        {
            var terminal = new PointOfSaleTerminal(CreateDefaultProducts());

            ScanStringAsChars(terminal, "ABCDABA");

            Assert.AreEqual(terminal.CalculateTotal(), 13.25, Delta);
        }

        private void ScanStringAsChars(PointOfSaleTerminal terminal, string productCodes)
        {
            foreach(char ch in productCodes)
            {
                terminal.Scan(ch.ToString());
            }
        }

        private PointOfSaleProduct[] CreateDefaultProducts()
        {
            return new ProductsPriceSetBuilder()
                    .AddProduct("A", 1.25, 3, 3.0)
                    .AddProduct("B", 4.25)
                    .AddProduct("C", 1.0, 6, 5.0)
                    .AddProduct("D", 0.75)
                    .GetAllProducts();
        }
    }
}
