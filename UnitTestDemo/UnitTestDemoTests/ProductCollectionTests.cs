namespace UnitTestDemo.Tests
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class ProductCollectionTests
    {
        [TestMethod()]
        public void DistributeProductTest()
        {
            IDistribute distribute = new Distribute();

            ProductCollection productCollection = new ProductCollection(distribute);
            productCollection.Products =
                new List<Product>()
                    {
                        new Product { PId = 1, PName = "Test1" },
                        new Product { PId = 2, PName = "Test2" },
                        new Product { PId = 3, PName = "Test3" }
                    };
            var testResult = productCollection.DistributeProduct(new List<int>() { 1, 2 });

            Assert.AreEqual(testResult.Count, 2);
            Assert.AreEqual(testResult[0].PId, 1);
            Assert.AreEqual(testResult[1].PId, 2);
        }
    }
}