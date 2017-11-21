namespace BankSystem.Client
{
    using BankSystem.Client.Interfaces;
    using BankSystem.Data;
    using Microsoft.EntityFrameworkCore;

    internal class Initialize : IInitializer
    {
        private IWriter writer;

        public Initialize(IWriter writer)
        {
            this.writer = writer;
        }

        public void InitializeDatabase()
        {
            using (var db = new BankSystemContext())
            {
                this.writer.WriteMessage("Initialize and check if exist database...");

                db.Database.Migrate();

                this.writer.WriteMessage("Database is up to date and ready!");
            }
        }
    }
}