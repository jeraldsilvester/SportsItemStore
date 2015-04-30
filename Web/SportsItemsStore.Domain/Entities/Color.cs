using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsItemsStore.Domain.Entities
{
    public class Color
    {
        public int ColorID { get; set; }
        public string Name { get; set; }
        public string IsActive { get; set; }
        public string Order { get; set; }

        public virtual IList<ProductColor> ProductColors { get; set; }
    }
}
