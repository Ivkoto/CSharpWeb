using System;

namespace L02_OnetoManyRelation
{
    public class Program
    {
        public static void Main()
        {
            var db = new MyDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}