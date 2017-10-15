namespace SocialNetwork.Client
{
    using SocialNetwork.Data;
    using System;
    using System.Linq;

    public class DatabaseRequests
    {
        public void MakeRequest(SocialNetworkDbContext db)
        {
            //PrintAllUsersWithFriendsCount(db);
            //PrintAllActiveUsersWithMoreThan5Friends(db);
            //PrintAllAlbums(db);
            //PrintPictureInMoreThan2Albums(db);
        }

        private void PrintPictureInMoreThan2Albums(SocialNetworkDbContext db)
        {
            var result = db.Pictures
                .Where(p => p.Albums.Count > 2)
                .Select(p => new
                {
                    p.Title,
                    AlbumsCount = p.Albums.Count,
                    Albums = p.Albums.Select(a => new
                    {
                        a.Album.Name,
                        Owner = a.Album.User.Username
                    })
                })
                .OrderByDescending(p => p.AlbumsCount)
                .ThenBy(p => p.Title).ToList();

            foreach (var picture in result)
            {
                Console.WriteLine($"{picture.Title} is present in:");
                foreach (var album in picture.Albums)
                {
                    Console.WriteLine($"  {album.Name} that owned by {album.Owner}");
                }
                Console.WriteLine();
            }
        }

        private void PrintAllAlbums(SocialNetworkDbContext db)
        {
            var result = db.Albums
                .Select(a => new
                {
                    a.Name,
                    Owner = a.User.Username,
                    Pictures = a.Pictures.Count
                })
                .OrderByDescending(a => a.Pictures)
                .ThenBy(a => a.Owner)
                .ToList();

            foreach (var album in result)
            {
                Console.WriteLine($"{album.Name} is owned by {album.Owner} and have {album.Pictures} pictures");
            }
        }

        private void PrintAllActiveUsersWithMoreThan5Friends(SocialNetworkDbContext db)
        {
            var result = db.Users
                .Where(u => (!u.IsDeleted))
                .Where(u => (u.RelatedTo.Count + u.RelatedFrom.Count) > 5)                
                .Select(u => new
                {
                    u.Username,
                    u.RegisteredOn,
                    Friends = u.RelatedFrom.Count + u.RelatedTo.Count,
                    Membership = DateTime.Now.Subtract(u.RegisteredOn)
                })
                .OrderBy(u => u.RegisteredOn)
                .OrderByDescending(u => u.Friends)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username} have {user.Friends} friends for {(int)user.Membership.TotalDays} day in our network.");
            }
        }

        private void PrintAllUsersWithFriendsCount(SocialNetworkDbContext db)
        {
            var result = db.Users
                .Select(u => new
                {
                    u.Username,
                    Friends = u.RelatedTo.Count + u.RelatedFrom.Count,
                    Status = u.IsDeleted ? "Inactive" : "Active"
                })
                .OrderByDescending(u => u.Friends)
                .ThenBy(u => u.Username).ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username} - {user.Friends} friends - status: {user.Status}");
            }
        }
    }
}