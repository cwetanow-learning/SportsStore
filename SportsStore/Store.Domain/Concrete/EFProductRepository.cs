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
        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }
    }
}
