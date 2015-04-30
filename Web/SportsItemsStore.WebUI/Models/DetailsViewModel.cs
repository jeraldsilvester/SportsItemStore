using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsItemsStore.Domain.Entities;

namespace SportsItemsStore.WebUI.Models
{
    public class DetailsViewModel
    {
        public Product  product{ get; set; }
        public string ReturnUrl { get; set; }
    }
}