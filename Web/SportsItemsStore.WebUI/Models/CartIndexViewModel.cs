using SportsItemsStore.Domain.Entities;

namespace SportsItemsStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}