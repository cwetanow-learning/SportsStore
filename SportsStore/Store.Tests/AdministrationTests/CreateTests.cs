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

namespace Store.Tests.AdministrationTests
{
    [TestFixture]
    class CreateTests
    {
        [Test]
        public void TestCreate_ShouldReturnEditView()
        {
            var mockedRepository = new Mock<IProductRepository>();

            var controller = new AdminController(mockedRepository.Object);

            var result = controller.Create();

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual("Edit", result.ViewName);
        }
    }
}
