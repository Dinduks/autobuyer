using System.Collections.Generic;
using autobuyer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutobuyerTests
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void RoundPriceTests()
        {
            var x = new Dictionary<double, double>
            {
                {0, 150},
                {150, 200},
                {157.1, 200},
                {900, 950},
                {907.1, 950},
                {950, 1000},
                {957.1, 1000},
                {1000, 1100},
                {1007.1, 1100},
                {9800, 9900},
                {9807.1, 9900},
                {9900, 10000},
                {9907.1, 10000},
                {10000, 10250},
                {10007.1, 10250},
                {49750, 50000},
                {49757.1, 50000},
                {50000, 50500},
                {50007.1, 50500},
                {99500, 100000},
                {99507.1, 100000},
                {100000, 101000},
                {100007.1, 101000},
                {999000, 1000000},
                {999007.1, 1000000},
                {1000000, 1001000},
                {1000007.1, 1001000}
            };

            foreach (var pair in x)
            {
                Assert.AreEqual(pair.Value, Util.RoundPrice(pair.Key));
            }
        }
    }
}