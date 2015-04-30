using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsItemsStore.Domain.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public virtual IList<ProductSize> ProductSizes { get; set; }

        public virtual IList<ProductColor> ProductColors { get; set; }
        public virtual IList<ProductManufacturer> ProductManufacturers { get; set; }
    }
}
