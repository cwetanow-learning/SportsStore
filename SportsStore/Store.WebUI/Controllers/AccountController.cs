using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Store.Domain.Contracts;
using Store.Domain.Models;
using Store.WebUI.App_Start;
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
        private MyUserManager manager;

        public AccountController() : this(IdentityConfig.UserManagerFactory.Invoke())
        {

        }

        public AccountController(MyUserManager manager)
        {
            this.manager = manager;
        }
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", new { controller = "Admin" });
            }
            else
            {
                return View();
            }

        }

        public MyUserManager UserManager
        {
            get
            {
                return this.manager ?? HttpContext.GetOwinContext().GetUserManager<MyUserManager>();
            }
            private set
            {
                this.manager = value;
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string url)
        {
            if (ModelState.IsValid)
            {

                return View();

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
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Username, Name = model.Name, Email = model.Email };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["message"] = "Success";
                    return RedirectToAction("List", "Product");
                }

            }

            return View(model);
        }
    }
}