using Moq;
using NUnit.Framework;
using Store.Domain.Concrete;
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
    class RestoreTests
    {
        [Test]
        public void TestRestore_NoDeletedProducts_ShouldReturnNothing()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedProduct = new Mock<IProduct>();
            mockedProduct.SetupGet(x => x.isDeleted).Returns(false);

            mockedRepository.SetupGet(x => x.Products).Returns(new List<IProduct> { mockedProduct.Object });

            var controller = new AdminController(mockedRepository.Object);

            var result = (controller.Restore().ViewData.Model as IEnumerable<IProduct>).Count();

            Assert.AreEqual(0, result);
        }

        [Test]
        public void TestRestore_HasDeletedProducts_ShouldReturnCorrectProduct()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedProduct = new Mock<IProduct>();
            mockedProduct.SetupGet(x => x.isDeleted).Returns(true);

            mockedRepository.SetupGet(x => x.Products).Returns(new List<IProduct> { mockedProduct.Object });

            var controller = new AdminController(mockedRepository.Object);

            var result = (controller.Restore().ViewData.Model as IEnumerable<IProduct>);

            Assert.IsTrue(result.Contains(mockedProduct.Object));
        }

        [TestCase(2)]
        [TestCase(1)]
        public void TestRestore_HasDeletedProducts_ShouldRestoreCorrectly(int id)
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedProductOne = new Mock<IProduct>();
            mockedProductOne.SetupGet(x => x.isDeleted).Returns(true);
            mockedProductOne.SetupGet(x => x.ProductID).Returns(1);

            var mockedProductTwo = new Mock<IProduct>();
            mockedProductTwo.SetupGet(x => x.isDeleted).Returns(true);
            mockedProductTwo.SetupGet(x => x.ProductID).Returns(2);

            mockedRepository.SetupGet(x => x.Products).Returns(new List<IProduct> { mockedProductOne.Object, mockedProductTwo.Object });

            var controller = new AdminController(mockedRepository.Object);

            var result = controller.Restore(id);

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }

        
    }
}
