namespace L03_SelfReferencedTable
{
    using L03_SelfReferencedTable.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var db = new MyDbContext();
            DatabaseClear(db);
        }

        private static void DatabaseClear(MyDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}