using Moq;
using NUnit.Framework;
using Store.Domain.Contracts;
using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.CartTests
{
    [TestFixture]
    class CartTest
    {
        [Test]
        public void TestAdd_AddProductsWhenEmpty_ShouldAddCorrectly()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var mockedSecondProduct = new Mock<IProduct>();
            mockedSecondProduct.SetupGet(p => p.ProductID).Returns(2);
            mockedSecondProduct.SetupGet(p => p.Name).Returns("P2");

            var cart = new Cart();

            cart.AddItem(mockedFirstProduct.Object, 2);
            cart.AddItem(mockedSecondProduct.Object, 4);

            Assert.AreEqual(cart.Lines.Count(), 2);
            Assert.AreEqual(cart.Lines.ToList()[0].Product, mockedFirstProduct.Object);
            Assert.AreEqual(cart.Lines.ToList()[1].Product, mockedSecondProduct.Object);
        }

        [Test]
        public void TestAdd_AddProductsWhenProductAlreadyThere_ShouldAddCorrectly()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var initialQuantity = 2;
            var quantity = 5;
            var cart = new Cart();

            cart.AddItem(mockedFirstProduct.Object, initialQuantity);
            cart.AddItem(mockedFirstProduct.Object, quantity);

            Assert.Contains(mockedFirstProduct.Object, cart.Lines.Select(x => x.Product).ToList());
            Assert.AreEqual(initialQuantity + quantity, cart.Lines.FirstOrDefault(p => p.Product.ProductID == mockedFirstProduct.Object.ProductID).Quantity);
        }

        [Test]
        public void TestRemove_ShouldRemoveCorrectly()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var quantity = 5;
            var cart = new Cart();

            cart.AddItem(mockedFirstProduct.Object, quantity);
            cart.RemoveLine(mockedFirstProduct.Object);

            Assert.IsFalse(cart.Lines.Select(x => x.Product).Contains(mockedFirstProduct.Object));
        }

        [Test]
        public void TestCalculate_ShouldCalculateCorrectly()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var mockedSecondProduct = new Mock<IProduct>();
            mockedSecondProduct.SetupGet(p => p.ProductID).Returns(2);
            mockedSecondProduct.SetupGet(p => p.Name).Returns("P2");

            var cart = new Cart();

            cart.AddItem(mockedFirstProduct.Object, 2);
            cart.AddItem(mockedSecondProduct.Object, 4);

            var result = cart.ComputeTotalValue();
            var expected = cart.Lines.Sum(p => p.Product.Price * p.Quantity);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void TestClear_ShouldEmtyCart()
        {
            var mockedFirstProduct = new Mock<IProduct>();
            mockedFirstProduct.SetupGet(p => p.ProductID).Returns(1);
            mockedFirstProduct.SetupGet(p => p.Name).Returns("P1");

            var mockedSecondProduct = new Mock<IProduct>();
            mockedSecondProduct.SetupGet(p => p.ProductID).Returns(2);
            mockedSecondProduct.SetupGet(p => p.Name).Returns("P2");

            var cart = new Cart();

            cart.AddItem(mockedFirstProduct.Object, 2);
            cart.AddItem(mockedSecondProduct.Object, 4);

            cart.Clear();

            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
