namespace L03_SelfReferencedTable
{
    using L03_SelfReferencedTable.Models;
    using Microsoft.EntityFrameworkCore;

    public class MyDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=LENOVO-KOSTOV;Database=MyTempDB;Integrated Security=True;");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder
            //    .Entity<Employee>()
            //    .HasOne(e => e.Department)
            //    .WithMany(d => d.Employees)
            //    .HasForeignKey(e => e.DepartmentId);

            //builder
            //    .Entity<Employee>()
            //    .HasOne(e => e.Menager)
            //    .WithMany(m => m.Subordinates)
            //    .HasForeignKey(e => e.MenagerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>(employee =>
            {
                employee
                    .HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .HasConstraintName("FK_Employee_Department_DepartmentId")
                    .OnDelete(DeleteBehavior.Restrict);

                employee
                    .HasOne(e => e.Menager)
                    .WithMany(m => m.Subordinates)
                    .HasForeignKey(e => e.MenagerId)
                    .HasConstraintName("FK_Employee_Manager_ManagerId")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}