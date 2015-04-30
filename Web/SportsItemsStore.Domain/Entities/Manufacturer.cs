using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsItemsStore.Domain.Entities
{
    public class Manufacturer
    {
        public int ManufacturerID { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public Nullable<int> Order { get; set; }

        public virtual IList<ProductManufacturer> ProductManufacturers { get; set; }
    }
}
