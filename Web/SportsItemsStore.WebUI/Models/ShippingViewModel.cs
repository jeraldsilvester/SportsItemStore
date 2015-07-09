using SportsItemsStore.Domain.Entities;

namespace SportsItemsStore.WebUI.Models
{
    public class ShippingViewModel
    {
        public Cart crt { get; set; }

        public ShippingDetails shipDtls { get; set; }
    }
}