﻿using Store.Domain.Contracts;
using Store.WebUI.Models;
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
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = repository.Products
                .Where(p => (category == null || p.Category == category) && !p.isDeleted)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize);

            var totalItems = category == null ?
                        repository.Products.Where(p => !p.isDeleted).Count() :
                        repository.Products.Where(p => p.Category == category && !p.isDeleted).Count();
            var model = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = totalItems
                },
                SelectedCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            var product = this.repository.Products.FirstOrDefault(x => x.ProductID == id);
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}