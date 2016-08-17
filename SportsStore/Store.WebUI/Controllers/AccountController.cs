using Store.WebUI.Infrastructure.Contracts;
using Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider provider;

        public AccountController(IAuthProvider prov)
        {
            this.provider = prov;
        }
        public ViewResult Login()
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
                    return Redirect(url ?? Url.Action("Index", "Admin"));
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
    }
}