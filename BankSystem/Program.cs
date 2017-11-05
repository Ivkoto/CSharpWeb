namespace BankSystem.Client
{
    using BankSystem.Data;
    using Microsoft.EntityFrameworkCore;

    internal class Program
    {
        private static void Main()
        {
            using (var context = new BankSystemContext())
            {
                context.Database.Migrate();
            }
        }
    }
}