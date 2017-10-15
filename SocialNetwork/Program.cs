namespace SocialNetwork
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Client;
    using SocialNetwork.Data;
    using System;

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
                //seeder.SeedAlbumsAndPictures(db);

                var dbRequest = new DatabaseRequests();
                dbRequest.MakeRequest(db);
            }
        }
    }
}