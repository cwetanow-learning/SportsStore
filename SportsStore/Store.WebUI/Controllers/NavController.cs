using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            this.repository = repo;
        }
        public PartialViewResult Menu()
        {
            var categories = this.repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(p => p);

            return PartialView(categories);
        }
    }
}