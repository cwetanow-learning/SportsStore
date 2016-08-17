using Moq;
using NUnit.Framework;
using Store.WebUI.Controllers;
using Store.WebUI.Infrastructure.Contracts;
using Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Store.Tests.AuthenticationTests
{
    [TestFixture]
    class LoginTests
    {
        [TestCase("bad", "bad")]
        [TestCase("not valid", "nope")]
        public void TestLogin_PassInvalidCredentials_ShouldNotAuthenticate(string user, string pass)
        {
            var mockedAuthenticator = new Mock<IAuthProvider>();
            mockedAuthenticator.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockedAuthenticator.Setup(x => x.Authenticate("admin", "secret")).Returns(true);

            var mockedLogin = new Mock<LoginViewModel>();
            mockedLogin.SetupGet(x => x.Password).Returns(pass);
            mockedLogin.SetupGet(x => x.Username).Returns(user);

            var controller = new AccountController(mockedAuthenticator.Object);

            var result = controller.Login(mockedLogin.Object, "");

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

        [TestCase("admin", "secret")]
        public void TestLogin_PassValidCredentials_ShouldAuthenticate(string user, string pass)
        {
            var mockedAuthenticator = new Mock<IAuthProvider>();
            mockedAuthenticator.Setup(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockedAuthenticator.Setup(x => x.Authenticate("admin", "secret")).Returns(true);

            var mockedLogin = new Mock<LoginViewModel>();
            mockedLogin.SetupGet(x => x.Password).Returns(pass);
            mockedLogin.SetupGet(x => x.Username).Returns(user);

            var controller = new AccountController(mockedAuthenticator.Object);
            var url = "url";

            var result = controller.Login(mockedLogin.Object, url);

            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.AreEqual(((RedirectResult)result).Url, url);
        }
    }
}
