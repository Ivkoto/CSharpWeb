namespace SocialNetwork
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Client;
    using SocialNetwork.Data;

    public class Program
    {
        public static void Main()
        {
            using (var db = new SocialNetworkDbContext())
            {
                //db.Database.EnsureDeleted();
                db.Database.Migrate();

                //var seeder = new SeedingData();
                //seeder.SeedUsers(db);
                //seeder.SeedFriendships(db);

                var dbRequest = new DatabaseRequests();
                dbRequest.MakeRequest(db);
            }
        }
    }
}