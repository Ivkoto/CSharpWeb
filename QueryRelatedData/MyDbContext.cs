using Microsoft.EntityFrameworkCore;
using QueryRelatedData.Models;

namespace QueryRelatedData
{
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
            builder
                .Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .HasConstraintName("FK_Employee_Departmet_DepartmentId");

            builder
                .Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department);
        }
    }
}