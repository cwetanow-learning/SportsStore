using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IProductRepository
    {
        IEnumerable<IProduct> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);

        Product RestoreProduct(int productId);
    }
}
