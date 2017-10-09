namespace StudentSystem
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Client;
    using StudentSystem.EntityDataModels;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new SystemDbContext())
            {
                //ClearDatabase(db);

                db.Database.Migrate();

                //var dataSeed = new SeedDatabase();
                //dataSeed.SeedData(db);

                var request = new DatabaseRequests();
                request.MakeRequest(db);
            }
        }

        private static void ClearDatabase(SystemDbContext db)
        {
            db.Database.EnsureDeleted();
        }
    }
}