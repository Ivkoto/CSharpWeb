namespace SocialNetwork.Client
{
    using SocialNetwork.Data;
    using SocialNetwork.Data.Logic;
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
            //PrintAllAlbumsByUserId(db);
            //PrintAlbumWithGiventag(db);
            //PrintUsersAlbumsWithMoreThan3Tags(db);
        }

        private void PrintUsersAlbumsWithMoreThan3Tags(SocialNetworkDbContext db)
        {
            var result = db.Users
                .Where(u => u.Albums.All(a => a.Tags.Count > 3))
                .Select(u => new
                {
                    u.Username,
                    AlbumsCount = u.Albums.Count,
                    Albums = u.Albums
                        .Select(a => new
                        {
                            a.Name,
                            Tags = string.Join(", ", a.Tags.Select(t => t.Tag.TagTitle).ToList())
                        }),
                    TagCount = u.Albums.Sum(a => a.Tags.Count)
                })
                .OrderByDescending(u => u.AlbumsCount)
                .ThenByDescending(u => u.TagCount)
                .ThenBy(u => u.Username)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine(user.Username);
                foreach (var album in user.Albums)
                {
                    Console.WriteLine($"  {album.Name}");
                    Console.WriteLine($"  {album.Tags}");
                }
                Console.WriteLine(new string('-', 20));
            }
        }

        private void PrintAlbumWithGiventag(SocialNetworkDbContext db)
        {
            var input = string.Empty;
            Console.Write("Input \"end\" to exit or enter sourched tag: ");
            while ((input = Console.ReadLine()) != "end")
            {
                var tag = TagTransformer.Transform(input);
                
                var result = db.Albums
                    .Where(a => a.Tags.Any(t => t.Tag.TagTitle == tag))
                    .OrderByDescending(a => a.Tags.Count)
                    .ThenBy(a => a.Name)
                    .Select(a => new
                    {
                        a.Name,
                        Owner = a.User.Username
                    }).ToList();

                foreach (var album in result)
                {
                    Console.WriteLine($"Album \"{album.Name}\" own by {album.Owner}");
                }

                Console.Write("Input \"end\" to exit or enter sourched tag: ");
            }
        }

        private void PrintAllAlbumsByUserId(SocialNetworkDbContext db)
        {
            var minUserId = db.Users.Select(u => u.Id).FirstOrDefault();
            var maxUserId = db.Users.Select(u => u.Id).LastOrDefault();
            Console.WriteLine($"Enter user Id between {minUserId} and {maxUserId} or \"end\" for stop the program!");
            Console.Write("Enter your choise: ");
            var input = string.Empty;
            while ((input = Console.ReadLine()) != "end")
            {
                var userId = int.Parse(input);
                if (userId < minUserId || userId > maxUserId)
                {
                    Console.WriteLine("Incorrect user ID!");
                    Console.WriteLine();
                    Console.WriteLine($"Enter user Id between {minUserId} and {maxUserId} or \"end\" for stop the program!");
                    Console.Write("Enter your choise: ");
                    continue;
                }
                var albumsOwner = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                var result = db.Albums
                    .Where(a => a.UserId == userId)
                    .Select(a => new
                    {
                        a.Name,
                        a.IsPublic
                        ,Pictures = a.Pictures.Select(p => new
                        {
                            p.Picture.Title,
                            p.Picture.Path
                        })
                    })
                    .OrderBy(a => a.Name).ToList();

                Console.WriteLine($"Username: {albumsOwner.Username}");
                foreach (var album in result)
                {                    
                    if (album.IsPublic)
                    {
                        Console.WriteLine($"  {album.Name}:");
                        foreach (var picture in album.Pictures)
                        {
                            Console.WriteLine($"   {picture.Title} ({picture.Path})");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"  {album.Name}: Private content!");
                    }
                }
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Enter user Id between {minUserId} and {maxUserId} or \"end\" for stop the program!");
                Console.Write("Enter your choise: ");
            }
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