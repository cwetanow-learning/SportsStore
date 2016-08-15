using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Models
{
    public class Cart : ICart
    {
        private List<ICartLine> lineCollection = new List<ICartLine>();

        public IEnumerable<ICartLine> Lines
        {
            get
            {
                return this.lineCollection;
            }
        }

        public void AddItem(IProduct product, int quantity)
        {
            var line = this.lineCollection
                 .FirstOrDefault(p => p.Product.ProductID == product.ProductID);
            if (line == null)
            {
                lineCollection.Add(new CartLine(product, quantity));
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Quantity * e.Product.Price);
        }

        public void RemoveLine(IProduct product)
        {
            lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }
    }
}
