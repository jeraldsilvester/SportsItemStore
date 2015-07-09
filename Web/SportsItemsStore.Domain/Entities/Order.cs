using System;
using System.Collections.Generic;

namespace SportsItemsStore.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public decimal OrderTotal { get; set; }

        public int AddressId { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public Nullable<DateTime> ShipmentDate { get; set; }

        public Nullable<DateTime> DeliveryDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Address Address { get; set; }

        public virtual User User { get; set; }
    }
}