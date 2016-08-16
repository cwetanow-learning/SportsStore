using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            this.repository = repo;
        }
        public ViewResult Index()
        {
            return View(this.repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var result = this.repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(result);
        }
    }
}