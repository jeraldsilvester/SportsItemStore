using SportsItemsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsItemsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<ProductSize> ProductSizes { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<ProductManufacturer> ProductManufacturers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Order>()
        //        .HasKey(c => new { c.OrderId});

        //   // modelBuilder.Entity<Product>()
        //   //     .HasKey(c => new { c.ProductID });

        //   // modelBuilder.Entity<Size>()
        //   //   .HasKey(c => new { c.SizeID });

        //   // modelBuilder.Entity<Color>()
        //   // .HasKey(c => new { c.ColorID });

        //   // modelBuilder.Entity<Manufacturer>()
        //   //.HasKey(c => new { c.ManufacturerID });

        //    modelBuilder.Entity<OrderDetail>()
        //        .HasRequired(p => p.Order)
        //        .WithMany(c => c.OrderDetails)
        //        .HasForeignKey(p => new { p.OrderId});

        //    //modelBuilder.Entity<OrderDetail>()
        //    //  .HasRequired(p => p.Product)
        //    //  .HasForeignKey(p => new { p.OrderId });

        //    //modelBuilder.Entity<OrderDetail>()
        //    //  .HasRequired(p => p.Order)
        //    //  .WithMany(c => c.OrderDetails)
        //    //  .HasForeignKey(p => new { p.OrderId });

        //    //modelBuilder.Entity<OrderDetail>()
        //    //  .HasRequired(p => p.Order)
        //    //  .WithMany(c => c.OrderDetails)
        //    //  .HasForeignKey(p => new { p.OrderId });

        //    //configure model with fluent API
        //    //modelBuilder.Entity<OrderDetail>()
        //    //  .HasRequired(p => p.Order);
        //     // .HasForeignKey(p => p.OrderID);
        //    //modelBuilder.Entity<OrderDetail>()
        //    //            .HasRequired<Order>(s => s.Order)
        //    //            .WithMany(s => s.OrderDetails)
        //    //            .HasForeignKey(s => s.OrderId);

        //}
    }
}