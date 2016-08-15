using Store.Domain.Contracts;
using System.Collections.Generic;


namespace Store.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<IProduct> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string SelectedCategory { get; set; }
    }
}