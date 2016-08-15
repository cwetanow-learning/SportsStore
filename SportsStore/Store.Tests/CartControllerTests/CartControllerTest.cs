using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.Domain.Models;
using Store.WebUI.Controllers;
using Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.CartControllerTests
{
    [TestFixture]
    class CartControllerTest
    {
        [Test]
        public void TestCartController_ShouldAddToCartCorrectly()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            mockedRepository.SetupGet(r => r.Products).Returns(new List<IProduct> { mockedFirstProduct.Object });

            var cart = new Cart();

            var controller = new CartController(mockedRepository.Object, null);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Product, mockedFirstProduct.Object);
        }

        [Test]
        public void TestCartController_WhenAddToCart_ShouldGoToCartScreen()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var mockedRepository = new Mock<IProductRepository>();
            mockedRepository.SetupGet(r => r.Products).Returns(new List<IProduct> { mockedFirstProduct.Object });

            var cart = new Cart();

            var controller = new CartController(mockedRepository.Object, null);

            var result = controller.AddToCart(cart, 1, "myUrl");

            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestCase("myUrl")]
        public void TestCartController_WhenAddToCart_ShouldOpenCorrectCart(string url)
        {
            var cart = new Cart();

            var controller = new CartController(null, null);

            var result = (CartIndexViewModel)controller.Index(cart, url).Model;

            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, url);
        }

        [Test]
        public void TestCartController_CheckoutEmptyCart_ShouldNotHappen()
        {
            var mockedProcessor = new Mock<IOrderProcessor>();
            var mockedCart = new Mock<Cart>();
            var mockedDetails = new Mock<ShippingDetails>();

            var controller = new CartController(null, mockedProcessor.Object);
            var result = controller.Checkout(mockedCart.Object, mockedDetails.Object);

            Assert.AreEqual(mockedCart.Object.Lines.Count(), 0);
            mockedProcessor.Verify(x => x.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [Test]
        public void TestCartController_CheckoutCart_ShouldCheckoutCorrectly()
        {
            var mockedProcessor = new Mock<IOrderProcessor>();

            var mockedProduct = new Mock<IProduct>();
            mockedProduct.SetupGet(x => x.ProductID).Returns(2);

            var cart = new Cart();
            cart.AddItem(mockedProduct.Object, 2);

            var mockedDetails = new Mock<ShippingDetails>();

            var controller = new CartController(null, mockedProcessor.Object);
            var result = controller.Checkout(cart, mockedDetails.Object);

            mockedProcessor.Verify(x => x.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);
            Assert.AreEqual("Completed", result.ViewName);
            Assert.IsTrue(result.ViewData.ModelState.IsValid);
        }
    }
}
