using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsItemsStore.Domain.Abstract;
using SportsItemsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsItemsStore.Domain.Concrete
{
    public class EFProductRepository : IProductsRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

        public IQueryable<Size> Sizes
        {
            get { return context.Sizes; }
        }

        public IQueryable<ProductSize> ProductSizes
        {
            get { return context.ProductSizes; }
        }

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IQueryable<Color> Colors
        {
            get { return context.Colors; }
        }

        public IQueryable<ProductColor> ProductColors
        {
            get { return context.ProductColors; }
        }



        public IQueryable<Manufacturer> Manufacturers
        {
            get { return context.Manufacturers; }
        }

        public IQueryable<ProductManufacturer> ProductManufacturers
        {
            get { return context.ProductManufacturers; }
        }

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

        public IQueryable<Address> Addresses
        {
            get { return context.Addresses; }
        }

        public IQueryable<Order> Orders
        {
            get { return context.Orders; }
        }

        public IQueryable<OrderDetail> OrderDetails
        {
            get { return context.OrderDetails; }
        }

        public void SaveUser(User user)
        {
            if (user.ID == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.ID);
                if (dbEntry != null)
                {
                    dbEntry.Username = user.Username;
                    dbEntry.Password = user.Password;
                    dbEntry.Email = user.Email;
                }
            }
            context.SaveChanges();
        }

        public void SaveAddress(Address addrs)
        {
            int adressId = 0;
            if (addrs.AddressID == 0)
            {
                context.Addresses.Add(addrs);
                adressId = addrs.AddressID;
            }
            else
            {
                Address dbEntry = context.Addresses.Find(addrs.AddressID);
                if (dbEntry != null)
                {
                    dbEntry.Line1 = addrs.Line1;
                    dbEntry.Line2 = addrs.Line2;
                    dbEntry.Line3 = addrs.Line3;
                    dbEntry.City = addrs.City;
                    dbEntry.State = addrs.State;
                    dbEntry.Zip = addrs.Zip;
                    dbEntry.Country = addrs.Country;
                    dbEntry.UserId = addrs.UserId;

                }

                adressId = addrs.AddressID;
            }
            context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {

            using (var context = new EFDbContext())
            {
                var _order = new Order
                {
                    AddressId = order.AddressId,
                    UserId = order.UserId,
                    OrderDate = DateTime.Now,
                    OrderTotal = order.OrderTotal
                };
                context.Orders.Add(_order);
                foreach (var detail in order.OrderDetails)
                {
                    var _orderDetail = new OrderDetail
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        SizeId = detail.SizeId,
                        ColorId = detail.ColorId,
                        ManufacturerId = detail.ManufacturerId,
                        SubTotal = detail.SubTotal,
                        Order = _order
                    };
                    context.OrderDetails.Add(_orderDetail);
                }
               context.SaveChanges();
            }

        }
    }
}

