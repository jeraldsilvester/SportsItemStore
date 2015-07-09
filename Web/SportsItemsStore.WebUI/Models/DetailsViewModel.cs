using SportsItemsStore.Domain.Entities;

namespace SportsItemsStore.WebUI.Models
{
    public class DetailsViewModel
    {
        public Product product { get; set; }

        public string ReturnUrl { get; set; }

        public int ManfacturerId { get; set; }

        public string Manfacturer { get; set; }
    }
}