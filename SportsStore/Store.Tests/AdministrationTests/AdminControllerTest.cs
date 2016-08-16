using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.AdministrationTests
{
    [TestFixture]
    class AdminControllerTest
    {
        [Test]
        public void TestIndex_ShouldReturnCorrectListOfProducts()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedProduct = new Mock<IProduct>();

            var products = new List<IProduct> { mockedProduct.Object };
            mockedRepository.SetupGet(x => x.Products).Returns(products);

            var controller = new AdminController(mockedRepository.Object);

            var result = ((IEnumerable<IProduct>)controller.Index().ViewData.Model).ToList();

            Assert.AreEqual(result, products);
        }
    }
}
