namespace L05_ShopHierarchy
{
    using L05_ShopHierarchy.Models;
    using Microsoft.EntityFrameworkCore;

    public class ShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesmans { get; set; }

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
        }
    }
}