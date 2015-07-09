using System.Collections.Generic;

namespace SportsItemsStore.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string ProductImageUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        //public virtual IList<Category> Categories { get; set; }
        public virtual IList<ProductSize> ProductSizes { get; set; }

        public virtual IList<ProductColor> ProductColors { get; set; }

        public virtual IList<ProductManufacturer> ProductManufacturers { get; set; }
    }
}