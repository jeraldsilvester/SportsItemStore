using System.Collections.Generic;

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