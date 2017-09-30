namespace L02_OnetoManyRelation
{
    using L02_OnetoManyRelation.Models;
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
    }
}