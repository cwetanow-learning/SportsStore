using Store.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Models
{
    public class CartLine : ICartLine
    {
        public CartLine(IProduct product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }
        public IProduct Product
        {
            get; set;
        }

        public int Quantity
        {
            get; set;
        }
    }
}
