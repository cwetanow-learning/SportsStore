using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private int PageSize = 2;
        public ProductController(IProductRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            return View(this.repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1)*PageSize)
                .Take(PageSize));
        }
    }
}