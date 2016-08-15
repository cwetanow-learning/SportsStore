using Store.Domain.Contracts;
using Store.Domain.Models;
using Store.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            this.repository = repo;
        }

        public RedirectToRouteResult AddToCart(ICart cart, int productId, string returnUrl)
        {
            var product = this.repository.Products.FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(ICart cart, int productId, string returnUrl)
        {
            var product = this.repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult Index(ICart cart, string returnUrl)
        {
            return View(new CartIndexViewModel(cart, returnUrl));
        }
    }
}