using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface ICart
    {
        void AddItem(IProduct product, int quantity);

        void RemoveLine(IProduct product);

        decimal ComputeTotalValue();

        void Clear();

        IEnumerable<ICartLine> Lines { get; }


    }
}
