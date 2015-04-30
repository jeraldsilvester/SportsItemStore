using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
