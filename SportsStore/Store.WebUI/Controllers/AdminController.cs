using Store.Domain.Contracts;
using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.WebUI.Controllers
{
    [Authorize(Users = "admin")]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            this.repository = repo;
        }
        public ViewResult Index()
        {
            return View(this.repository.Products.Where(p => !p.isDeleted));
        }

        public ViewResult Edit(int productId)
        {
            var result = this.repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                this.repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved!", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            var deletedProduct = this.repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was successfully deleted ! ", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public ViewResult Restore()
        {
            return View(this.repository.Products.Where(p => p.isDeleted));
        }

        [HttpPost]
        public ActionResult Restore(int productId)
        {
            var productToRestore = this.repository.RestoreProduct(productId);
            if (productToRestore != null)
            {
                TempData["message"] = string.Format("{0} was successfully restored ! ", productToRestore.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public PartialViewResult Search(string pattern = "")
        {
            var result = this.repository.Products.Where(p => p.Name.ToLower().Contains(pattern.ToLower()) && !p.isDeleted);
            return this.PartialView(result);
        }

        public ActionResult RestoreAll()
        {
            var restorationSuccessfull = this.repository.RestoreAll();
            if (restorationSuccessfull)
            {
                TempData["message"] = string.Format("Products were successfully restored ! ");
            }
            else
            {
                TempData["message"] = string.Format("No products found ");
            }
            return this.RedirectToAction("Index");
        }
    }
}