using SportsItemsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsItemsStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        //public PagingInfo PagingInfo { get; set; }
        //public string CurrentCategory { get; set; }
        //public string SearchTerm { get; set; }
    }
}