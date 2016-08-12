using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.Domain.Models;
using Store.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.ProductControllerTest
{
    [TestFixture]
    class PagenationTest
    {
        [Test]
        public void TestPaging_ShouldPageCorrectly()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var firstMockedProduct = new Mock<IProduct>();
            firstMockedProduct.SetupGet(p => p.ProductID).Returns(1);
            firstMockedProduct.SetupGet(p => p.Name).Returns("P1");

            var secondMockedProduct = new Mock<IProduct>();
            secondMockedProduct.SetupGet(p => p.ProductID).Returns(2);
            secondMockedProduct.SetupGet(p => p.Name).Returns("P2");

            var thirdtMockedProduct = new Mock<IProduct>();
            thirdtMockedProduct.SetupGet(p => p.ProductID).Returns(3);
            thirdtMockedProduct.SetupGet(p => p.Name).Returns("P3");

            var listOfProducts = new List<IProduct> { firstMockedProduct.Object, secondMockedProduct.Object, thirdtMockedProduct.Object };

            mockedRepository.Setup(s => s.Products).Returns(listOfProducts);

            var controller = new ProductController(mockedRepository.Object);

            var result = ((IEnumerable<IProduct>)controller.List(2).Model).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(thirdtMockedProduct.Object, result[0]);
        }
    }
}
