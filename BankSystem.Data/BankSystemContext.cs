namespace BankSystem.Data
{
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class BankSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        public DbSet<SavingAccount> SavingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyTempDB;Integrated Security=True");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.SavingAccounts)
                .WithOne(sa => sa.User)
                .HasForeignKey(sa => sa.UserId);

            builder.Entity<User>()
                .HasMany(u => u.CheckingAccounts)
                .WithOne(ca => ca.User)
                .HasForeignKey(ca => ca.UserId);
        }
    }
}