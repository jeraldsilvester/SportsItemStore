using System;

namespace SportsItemsStore.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        //public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Nullable<int> SizeId { get; set; }

        public Nullable<int> ColorId { get; set; }

        public Nullable<int> ManufacturerId { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public virtual Color Color { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Product Product { get; set; }

        public virtual Size Size { get; set; }
    }
}