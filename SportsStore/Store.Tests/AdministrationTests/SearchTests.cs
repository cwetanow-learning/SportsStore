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
    public class SearchTests
    {
        [TestCase("b")]
        [TestCase("kiro")]
        [TestCase("")]
        [TestCase("asd")]
        public void TestSearch_PassValidName_ShouldReturnCorrectProducts(string pattern)
        {
            var mockedProduct = new Mock<IProduct>();
            mockedProduct.SetupGet(x => x.Name).Returns("abc");
            mockedProduct.SetupGet(x => x.isDeleted).Returns(false);

            var mockedProductTwo = new Mock<IProduct>();
            mockedProductTwo.SetupGet(x => x.Name).Returns("kiro");
            mockedProductTwo.SetupGet(x => x.isDeleted).Returns(false);

            var products = new List<IProduct> { mockedProduct.Object, mockedProductTwo.Object };

            var mockedRepository = new Mock<IProductRepository>();
            mockedRepository.SetupGet(x => x.Products).Returns(products);

            var controller = new AdminController(mockedRepository.Object);

            var result = controller.Search(pattern).ViewData.Model;
            var expected = products.Where(x => pattern == string.Empty || x.Name.ToLower().Contains(pattern.ToLower()));

            Assert.AreEqual(result, expected);
        }
    }
}
