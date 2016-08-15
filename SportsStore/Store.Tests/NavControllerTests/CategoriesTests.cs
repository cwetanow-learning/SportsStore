using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.NavControllerTests
{
    [TestFixture]
    class CategoriesTests
    {
        [Test]
        public void TestCategories_ShouldReturnCorrectList()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var firstMockedProduct = new Mock<IProduct>();
            firstMockedProduct.SetupGet(p => p.ProductID).Returns(1);
            firstMockedProduct.SetupGet(p => p.Name).Returns("P1");
            firstMockedProduct.SetupGet(p => p.Category).Returns("Cat1");

            var secondMockedProduct = new Mock<IProduct>();
            secondMockedProduct.SetupGet(p => p.ProductID).Returns(2);
            secondMockedProduct.SetupGet(p => p.Name).Returns("P2");
            secondMockedProduct.SetupGet(p => p.Category).Returns("Cat2");

            var thirdtMockedProduct = new Mock<IProduct>();
            thirdtMockedProduct.SetupGet(p => p.ProductID).Returns(3);
            thirdtMockedProduct.SetupGet(p => p.Name).Returns("P3");
            thirdtMockedProduct.SetupGet(p => p.Category).Returns("Cat1");

            var listOfProducts = new List<IProduct> { firstMockedProduct.Object, secondMockedProduct.Object, thirdtMockedProduct.Object };

            mockedRepository.Setup(s => s.Products).Returns(listOfProducts);

            var controller = new NavController(mockedRepository.Object);

            var result = ((IEnumerable<string>)controller.Menu().Model).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(result[0], firstMockedProduct.Object.Category);
            Assert.AreEqual(result[1], secondMockedProduct.Object.Category);
        }
    }
}
