using SportsItemsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsItemsStore.WebUI.Models
{
    public class MyOrderViewModel
    {
        public IList<CartLine> orderLines { get; set; }

        public string ReturnUrl { get; set; }
    }
}