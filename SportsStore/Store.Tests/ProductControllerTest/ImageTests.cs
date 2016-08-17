using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Tests.ProductControllerTest
{
    [TestFixture]
    class ImageTests
    {
        [TestCase(new byte[] { }, "image/png", 1)]
        [TestCase(new byte[] { 1, 0, 1 }, "image/jpeg",2)]
        public void TestGetImage_PassValidData_ShouldReturnImage(byte[] data, string type, int id)
        {
            var mockedProduct = new Mock<IProduct>();
            mockedProduct.SetupGet(x => x.ProductID).Returns(id);
            mockedProduct.SetupGet(x => x.ImageData).Returns(data);
            mockedProduct.SetupGet(x => x.ImageMimeType).Returns(type);

            var mockedRepository = new Mock<IProductRepository>();
            mockedRepository.SetupGet(x => x.Products).Returns(new List<IProduct> { mockedProduct.Object });

            var controller = new ProductController(mockedRepository.Object);

            var result = controller.GetImage(id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<FileResult>(result);
            Assert.AreEqual(mockedProduct.Object.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestCase(new byte[] { }, "image/png", 1)]
        [TestCase(new byte[] { 1, 0, 1 }, "image/jpeg", 2)]
        public void TestGetImage_PassInvalidData_ShouldNotReturnImage(byte[] data, string type, int id)
        {
            var mockedProduct = new Mock<IProduct>();
            mockedProduct.SetupGet(x => x.ProductID).Returns(5);
            mockedProduct.SetupGet(x => x.ImageData).Returns(data);
            mockedProduct.SetupGet(x => x.ImageMimeType).Returns(type);

            var mockedRepository = new Mock<IProductRepository>();
            mockedRepository.SetupGet(x => x.Products).Returns(new List<IProduct> { mockedProduct.Object });

            var controller = new ProductController(mockedRepository.Object);

            var result = controller.GetImage(id);

            Assert.IsNull(result);            
        }
    }
}
