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
    class DeleteTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public void TestDeleteProduct_PassValidData_ShouldRemoveCorrectly(int id)
        {
            var mockedRepository = new Mock<IProductRepository>();

            var firstMockedProduct = new Mock<IProduct>();
            firstMockedProduct.SetupGet(p => p.ProductID).Returns(1);
            firstMockedProduct.SetupGet(p => p.Name).Returns("P1");

            var secondMockedProduct = new Mock<IProduct>();
            secondMockedProduct.SetupGet(p => p.ProductID).Returns(2);
            secondMockedProduct.SetupGet(p => p.Name).Returns("P2");

            var thirdMockedProduct = new Mock<IProduct>();
            thirdMockedProduct.SetupGet(p => p.ProductID).Returns(3);
            thirdMockedProduct.SetupGet(p => p.Name).Returns("P3");

            mockedRepository.SetupGet(x => x.Products).Returns(new List<IProduct> { firstMockedProduct.Object, secondMockedProduct.Object, thirdMockedProduct.Object });

            var controller = new AdminController(mockedRepository.Object);

            controller.Delete(id);

            mockedRepository.Verify(x => x.DeleteProduct(id), Times.Once);
        }
    }
}
