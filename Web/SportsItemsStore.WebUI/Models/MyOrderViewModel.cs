using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsItemsStore.Domain.Entities;

namespace SportsItemsStore.WebUI.Models
{
    public class MyOrderViewModel
    {
        public IList<CartLine> orderLines { get; set; }
        public string ReturnUrl { get; set; }
    }
}