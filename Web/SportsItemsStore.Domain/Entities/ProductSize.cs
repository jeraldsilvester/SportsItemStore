using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsItemsStore.Domain.Entities
{
    public class ProductSize
    {
        public int ProductSizeID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public Nullable<int> Order { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
    }
}
