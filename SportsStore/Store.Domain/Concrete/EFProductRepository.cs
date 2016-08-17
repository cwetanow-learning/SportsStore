using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Domain.Models;

namespace Store.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDBContext context = new EFDBContext();
        public IEnumerable<IProduct> Products
        {
            get
            {
                return context.Products;
            }
        }

        public Product DeleteProduct(int productId)
        {
            var dbEntry = this.context.Products.Find(productId);
            if (dbEntry != null)
            {
                this.context.Products.Remove(dbEntry);
                this.context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                this.context.Products.Add(product);
            }
            else
            {
                var dbEntry = this.context.Products.Find(product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Price = product.Price;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            this.context.SaveChanges();
        }
    }
}
