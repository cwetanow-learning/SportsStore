using Store.Domain.Contracts;
using Store.Domain.Models;
using Store.WebUI.Infrastructure.Contracts;
using Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Store.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider provider;

        public AccountController(IAuthProvider prov)
        {
            this.provider = prov;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string url)
        {
            if (ModelState.IsValid)
            {
                if (provider.Authenticate(model.Username, model.Password))
                {
                    return Redirect(url ?? Url.Action("List", "Product"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("List", "Product");
        }

        public ViewResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            return View("Login");
        }
    }
}