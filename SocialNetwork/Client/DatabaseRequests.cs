namespace SocialNetwork.Client
{
    using SocialNetwork.Data;
    using SocialNetwork.Data.Enums;
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
            //PrintUsersWithSharedUsers(db);
            //PrintAlbumsSharedWithMoreThan2(db);
            //PrintAlbumSharedWithGivenUser(db);
            //PrintAlbumsWithUsersAndUserRoles(db);
            //PrintGivenUserOwnedAndViewedAlbums(db);
            PrintUsersWithLessOneViewedAlbumAndPublicAlbums(db);
        }

        private void PrintUsersWithLessOneViewedAlbumAndPublicAlbums(SocialNetworkDbContext db)
        {
            var result = db.Users
                .Select(u => new
                {
                    u.Username,
                    AlbumsCount = u.SharedAlbums
                        .Where(sa => sa.SharedAlbum.IsPublic)
                        .Select(sa => sa.UserRole == UserRole.Viewer)
                        .Count()
                })
                .Where(u => u.AlbumsCount > 7)
                .ToList();

            foreach (var user in result)
            {
                Console.WriteLine($"{user.Username} are viewer of {user.AlbumsCount} public albums.");
            }
        }

        private void PrintGivenUserOwnedAndViewedAlbums(SocialNetworkDbContext db)
        {
            Console.Write("Write User username.Users must be from User_1 to User_49: ");
            var currentUser = Console.ReadLine().Trim();

            var result = db.Users
                .Where(u => u.Username == currentUser)
                .Select(u => new
                {
                    u.Username,
                    AlbumsCount = u.Albums.Count(),
                    AlbumsViewed = u.SharedAlbums.Where(a => a.UserRole == UserRole.Viewer).Count()
                }).FirstOrDefault();

            Console.WriteLine($@"Username: {result.Username}
  Own {result.AlbumsCount} albums
  Viewer at {result.AlbumsViewed} albums");
        }

        private void PrintAlbumsWithUsersAndUserRoles(SocialNetworkDbContext db)
        {
            var result = db.Albums
                .Where(a => a.UsersWithRoles.Any())
                .Select(a => new
                {
                    a.Name,
                    Owner = a.User.Username,
                    ViewersCount = a.UsersWithRoles
                        .Where(ur => ur.UserRole == UserRole.Viewer)
                        .Count(),
                    Users = a.UsersWithRoles.Select(ur => new
                    {
                        Username = ur.User.Username,
                        UserRole = ur.UserRole
                    })
                })
                .OrderBy(u => u.Owner)
                .ThenByDescending(u => u.ViewersCount);

            foreach (var album in result)
            {
                Console.WriteLine($"Album name: {album.Name}");
                Console.WriteLine($"Owner: {album.Owner}");
                Console.WriteLine($"Viewers: {album.ViewersCount}");
                Console.WriteLine("All users:");
                Console.WriteLine(string.Join("\n", album.Users));
                Console.WriteLine(new string('-', 30));
            }
        }

        private void PrintAlbumSharedWithGivenUser(SocialNetworkDbContext db)
        {
            Console.WriteLine("Write username. Users must be from User_1 to User_49...");
            var givenUser = Console.ReadLine();
            var result = db.Users
                .Where(u => u.Username == givenUser)
                .Select(u => new
                {
                    u.Username,
                    SharedAlbums = u.SharedAlbums.Select(sa => new
                    {
                        sa.SharedAlbum.Name,
                        picturesCount = sa.SharedAlbum.Pictures.Count
                    })
                    .OrderByDescending(a => a.picturesCount)
                    .ThenBy(a => a.Name)
                    .ToList()
                }).FirstOrDefault();

            Console.WriteLine(result.Username);
            foreach (var album in result.SharedAlbums)
            {
                Console.WriteLine($" album name: {album.Name}");
                Console.WriteLine($" picture count: {album.picturesCount}");
            }
        }

        private void PrintAlbumsSharedWithMoreThan2(SocialNetworkDbContext db)
        {
            var result = db.Albums
                .Where(a => a.UsersWithRoles.Count > 2)
                .Select(a => new
                {
                    a.Name,
                    NumberOfPeople = a.UsersWithRoles.Count,
                    a.IsPublic
                })
                .OrderByDescending(a => a.NumberOfPeople)
                .ThenBy(a => a.Name);

            foreach (var album in result)
            {
                Console.WriteLine(album.Name);
                Console.WriteLine($"   shared with: {album.NumberOfPeople}");
                Console.Write("   Album is: ");
                Console.WriteLine(album.IsPublic == true ? "public" : "not public");
            }
        }

        private void PrintUsersWithSharedUsers(SocialNetworkDbContext db)
        {
            var result = db.Users
                .Where(u => u.SharedAlbums.Any())
                .Select(u => new
                {
                    Name = u.Username,
                    SharedAlbums = u.SharedAlbums.Select(sa => new
                    {
                        AlbumName = sa.SharedAlbum.Name,
                        SharedWith = sa.SharedAlbum.UsersWithRoles.Select(a => new
                        {
                            a.User.Username
                        }).ToList()
                    }).ToList()
                });

            foreach (var user in result)
            {
                Console.WriteLine(user.Name);
                foreach (var album in user.SharedAlbums)
                {
                    Console.WriteLine($"  {album.AlbumName}");
                    foreach (var sUser in album.SharedWith)
                    {
                        Console.WriteLine($"--shared with: {sUser.Username}");
                    }
                }
                Console.WriteLine($"   ");
            }
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
                        ,
                        Pictures = a.Pictures.Select(p => new
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