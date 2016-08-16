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
using System.Web.Mvc;

namespace Store.Tests.AdministrationTests
{
    [TestFixture]
    class EditTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public void TestEdit_PassValidId_ShouldReturnCorrectProduct(int id)
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(x => x.ProductID).Returns(1);

            var mockedSecondProduct = new Mock<IProduct>();
            mockedSecondProduct.SetupGet(x => x.ProductID).Returns(2);

            var products = new List<IProduct> { mockedFirstProduct.Object, mockedSecondProduct.Object };
            mockedRepository.SetupGet(x => x.Products).Returns(products);

            var controller = new AdminController(mockedRepository.Object);

            var result = controller.Edit(id).ViewData.Model;
            var expected = mockedRepository.Object.Products.FirstOrDefault(x => x.ProductID == id);

            Assert.AreEqual(expected, result);
        }

        [TestCase(5)]
        [TestCase(6)]
        public void TestEdit_PassInvalidId_ShouldReturnNull(int id)
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(x => x.ProductID).Returns(1);

            var products = new List<IProduct> { mockedFirstProduct.Object };
            mockedRepository.SetupGet(x => x.Products).Returns(products);

            var controller = new AdminController(mockedRepository.Object);

            var result = controller.Edit(id).ViewData.Model;

            Assert.IsNull(result);
        }

        [Test]
        public void TestEdit_PassValidChanges_ShouldSaveCorrectly()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var controller = new AdminController(mockedRepository.Object);

            var mockedProduct = new Mock<Product>();

            var result = controller.Edit(mockedProduct.Object);

            mockedRepository.Verify(x => x.SaveProduct(It.IsAny<Product>()), Times.Once);

            Assert.IsNotInstanceOf<ViewResult>(result);
        }

        [Test]
        public void TestEdit_PassInvalidChanges_ShouldNotSave()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var controller = new AdminController(mockedRepository.Object);
            controller.ModelState.AddModelError("error", "error");

            var mockedProduct = new Mock<Product>();

            var result = controller.Edit(mockedProduct.Object);

            mockedRepository.Verify(x => x.SaveProduct(It.IsAny<Product>()), Times.Never());

            Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
