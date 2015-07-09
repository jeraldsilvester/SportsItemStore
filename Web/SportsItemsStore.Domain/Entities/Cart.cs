using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsItemsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity, string size, string color, string mnfc, int sizeId, int colorId, int mnId, string shippedTo, DateTime ordDate)
        {
            CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID && p.Size == size && p.Color == color && p.Manufacterer == mnfc).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    Size = size,
                    Color = color,
                    Manufacterer = mnfc,
                    SizeId = sizeId,
                    ColorId = colorId,
                    ManufactererId = mnId,
                    ShippedTo = shippedTo,
                    OrderDate = ordDate
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product, int sizeId, int colorId, int mnId)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID && l.SizeId == sizeId && l.ColorId == colorId && l.ManufactererId == mnId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public int SizeId { get; set; }

        public string Color { get; set; }

        public int ColorId { get; set; }

        public string Manufacterer { get; set; }

        public int ManufactererId { get; set; }

        public string ShippedTo { get; set; }

        public DateTime OrderDate { get; set; }
    }
}