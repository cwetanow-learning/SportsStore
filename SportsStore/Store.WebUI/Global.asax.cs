using Store.Domain.Concrete;
using Store.Domain.Models;
using Store.WebUI.Infrastructure.Binders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Store.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EFDBContext>());
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
