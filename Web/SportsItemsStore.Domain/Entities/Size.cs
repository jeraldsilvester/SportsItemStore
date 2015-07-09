using System;
using System.Collections.Generic;

namespace SportsItemsStore.Domain.Entities
{
    public class Size
    {
        public int SizeID { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string IsActive { get; set; }

        public Nullable<int> Order { get; set; }

        public virtual IList<ProductSize> ProductSizes { get; set; }
    }
}