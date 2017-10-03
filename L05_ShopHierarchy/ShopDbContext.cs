namespace L05_ShopHierarchy
{
    using L05_ShopHierarchy.Entities;
    using L05_ShopHierarchy.Models;
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmans { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<OrdersItems> OrdersItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=LENOVO-KOSTOV;Database=MyTempDB;Integrated security=True;");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Customer>(customer =>
                {
                    customer.HasKey(c => c.Id);

                    customer.HasOne(c => c.Salesman)
                        .WithMany(s => s.Customers)
                        .HasForeignKey(c => c.SalesmanId)
                        .HasConstraintName("FK_Customer_Salesman_SalesmanId");                    
                });

            builder
                .Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Order_Customer_CustomerId");

            builder
                .Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .HasConstraintName("FK_Review_Customer_CustomerId");

            builder.Entity<OrdersItems>(entity =>
            {
                entity
                    .HasKey(oi => new { oi.OrderId, oi.ItemId });

                entity
                    .HasOne(oi => oi.Order)
                    .WithMany(o => o.Items)
                    .HasForeignKey(oi => oi.OrderId)
                    .HasConstraintName("FK_Order_Item_OrderId");

                entity
                    .HasOne(oi => oi.Item)
                    .WithMany(i => i.Orders)
                    .HasForeignKey(oi => oi.ItemId)
                    .HasConstraintName("FK_Item_Order_ItemId");
            });
                
        }
    }
}