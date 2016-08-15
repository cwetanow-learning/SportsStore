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
using Store.WebUI.HTMLHelpers;
using Store.WebUI.Models;
using System.Web.Mvc;

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
            controller.PageSize = 2;

            var result = ((ProductsListViewModel)controller.List(null, 2).Model);

            Assert.AreEqual(1, result.Products.Count());
            Assert.AreEqual(thirdtMockedProduct.Object, result.Products.ToList()[0]);
        }

        [Test]
        public void TestPaging_ShouldGeneratePageLinks()
        {
            HtmlHelper helper = null;

            var pagingInfo = new PagingInfo { CurrentPage = 2, ItemsPerPage = 10, TotalItems = 28 };

            Func<int, string> urlDelegate = i => "Page" + i;

            MvcHtmlString result = helper.PageLinks(pagingInfo, urlDelegate);

            var expectedResult = @"<a class=""btn btn-default"" href=""Page1"">1</a>"
                                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                                + @"<a class=""btn btn-default"" href=""Page3"">3</a>";

            Assert.AreEqual(expectedResult, result.ToString());
        }
        [Test]
        public void TestPaging_ShouldSendPaginationViewModel()
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
            controller.PageSize = 2;

            var result = ((ProductsListViewModel)controller.List(null, 2).Model);

            Assert.AreEqual(result.PagingInfo.CurrentPage, 2);
            Assert.AreEqual(result.PagingInfo.ItemsPerPage, 2);
            Assert.AreEqual(result.PagingInfo.TotalItems, 3);
            Assert.AreEqual(result.PagingInfo.TotalPages, 2);
        }

        [TestCase("Cat1")]
        [TestCase("Cat2")]
        public void TestPaging_ShouldFilterByCategoryCorrectly(string category)
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
            thirdtMockedProduct.SetupGet(p => p.Category).Returns("Cat3");

            var listOfProducts = new List<IProduct> { firstMockedProduct.Object, secondMockedProduct.Object, thirdtMockedProduct.Object };

            mockedRepository.Setup(s => s.Products).Returns(listOfProducts);

            var controller = new ProductController(mockedRepository.Object);

            var result = ((ProductsListViewModel)controller.List(category).Model).Products.ToList();
            var expected = mockedRepository.Object.Products.Where(c => c.Category == category);

            Assert.AreEqual(result.Count, expected.Count());
            Assert.AreEqual(result, expected);
        }
    }
}
