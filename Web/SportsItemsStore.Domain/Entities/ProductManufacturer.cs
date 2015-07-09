using System;

namespace SportsItemsStore.Domain.Entities
{
    public class ProductManufacturer
    {
        public int ProductManufacturerID { get; set; }

        public int ProductID { get; set; }

        public int ManufacturerID { get; set; }

        public Nullable<int> Order { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Product Product { get; set; }
    }
}