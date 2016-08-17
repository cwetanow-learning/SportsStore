using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    public interface IProduct
    {
        int ProductID { get; }
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        string Category { get; }
        byte[] ImageData { get; }
        string ImageMimeType { get; }
    }
}
