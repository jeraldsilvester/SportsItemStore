using System;

namespace SportsItemsStore.Domain.Entities
{
    public class ProductColor
    {
        public int ProductColorID { get; set; }

        public int ProductID { get; set; }

        public int ColorID { get; set; }

        public Nullable<int> Order { get; set; }

        public virtual Color Color { get; set; }

        public virtual Product Product { get; set; }
    }
}