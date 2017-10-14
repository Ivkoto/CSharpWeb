using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;

namespace SocialNetwork
{
    public class Program
    {
        public static void Main()
        {
            using (var db = new SocialNetworkDbContext())
            {
                //db.Database.EnsureDeleted();
                db.Database.Migrate();

                var seeder = new SeedingData();
                seeder.SeedUsers(db);
                
            }
        }
    }
}