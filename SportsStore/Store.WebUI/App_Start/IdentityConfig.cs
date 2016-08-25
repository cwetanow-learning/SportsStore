using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Store.Domain.Concrete;
using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WebUI.App_Start
{
    public class IdentityConfig
    {
        public static Func<MyUserManager> UserManagerFactory { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new EFDBContext());
            app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);
            app.CreatePerOwinContext<RoleManager<Role>>((options, context) =>
                new RoleManager<Role>(
                    new RoleStore<Role>(context.Get<EFDBContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            UserManagerFactory = () =>
            {
                var userManager = new MyUserManager(new UserStore<User>(new EFDBContext()));

                return userManager;
            };
        }
    }
}