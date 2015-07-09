﻿using SportsItemsStore.Domain.Entities;
using System.Linq;

namespace SportsItemsStore.Domain.Abstract
{
    public interface IProductsRepository
    {
        IQueryable<Product> Products { get; }

        IQueryable<Size> Sizes { get; }

        IQueryable<ProductSize> ProductSizes { get; }

        IQueryable<Category> Categories { get; }

        IQueryable<Color> Colors { get; }

        IQueryable<ProductColor> ProductColors { get; }

        IQueryable<Manufacturer> Manufacturers { get; }

        IQueryable<ProductManufacturer> ProductManufacturers { get; }

        IQueryable<User> Users { get; }

        IQueryable<Address> Addresses { get; }

        IQueryable<Order> Orders { get; }

        IQueryable<OrderDetail> OrderDetails { get; }

        void SaveUser(User user);

        void SaveAddress(Address add);

        void SaveOrder(Order order);
    }
}