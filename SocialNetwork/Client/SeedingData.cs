namespace SocialNetwork.Client
{
    using SocialNetwork.Data;
    using SocialNetwork.Data.EntityDataModels;
    using System;
    using System.Linq;

    public class SeedingData
    {
        private static Random random = new Random();
        private const int initialUsersCount = 50;
        private DateTime currentDate = DateTime.Now;
        private const int initialAlbumCount = 250;
        private const int initialPictureCount = 1000;

        public void SeedUsers(SocialNetworkDbContext db)
        {
            Console.Write("UsersLoading");
            var biggestUserId = db.Users.OrderByDescending(u => u.Id).Select(u => u.Id).FirstOrDefault();

            for (int i = biggestUserId + 1; i < biggestUserId + initialUsersCount; i++)
            {
                db.Users.Add(new User
                {
                    Username = $"User_{i}",
                    Password = $"UseR#{i}_<Password>$",
                    Email = $"User-{i}@email.com",
                    RegisteredOn = currentDate.AddDays(-(20 + i)),
                    LastTymeLoggedIn = currentDate.AddDays(-i),
                    Age = 20 + i
                });
                Console.Write(".");
            }
            Console.WriteLine();
            db.SaveChanges();
        }

        public void SeedFriendships(SocialNetworkDbContext db)
        {
            Console.Write("Friendships Loading");
            var allUserIds = db.Users.Select(u => u.Id).ToList();

            for (int i = 0; i < allUserIds.Count; i++)
            {
                var currentUserId = allUserIds[i];
                var totalFriends = random.Next(5, 11);

                for (int j = 0; j < totalFriends; j++)
                {
                    var validFriendship = true;
                    var friendId = allUserIds[random.Next(0, allUserIds.Count)];

                    if (friendId == currentUserId)
                    {
                        validFriendship = false;
                    }

                    var friendshipExist = db.Friendships.Any(f =>
                        (f.FromFriendId == currentUserId && f.ToFriendId == friendId) ||
                        (f.FromFriendId == friendId && f.ToFriendId == currentUserId));
                    if (friendshipExist)
                    {
                        validFriendship = false;
                    }
                    if (!validFriendship)
                    {
                        j--;
                        continue;
                    }

                    db.Friendships.Add(new Friendship
                    {
                        FromFriendId = currentUserId,
                        ToFriendId = friendId
                    });
                    Console.Write(".");
                    db.SaveChanges();
                }
            }
            Console.WriteLine();
        }

        public void SeedAlbumsAndPictures(SocialNetworkDbContext db)
        {
            //Add pictures
            Console.Write("Add pictures to database");
            var biggestPictureId = db.Pictures.OrderByDescending(p => p.Id).Select(p => p.Id).FirstOrDefault() + 1;
            for (int j = 0; j < initialPictureCount; j++)
            {
                db.Pictures.Add(new Picture
                {
                    Title = $"Picture_{j}",
                    Caption = $"This is the caption of picture {j}",
                    Path = $"C://User/Pictures/picture{j}.jpg"
                });
                Console.Write(".");
            }
            Console.WriteLine();
            db.SaveChanges();

            //Add albums and user
            Console.Write("Add albums to database");
            var userIds = db.Users.Select(u => u.Id).ToList();
            var biggestAlbumId = db.Albums.OrderByDescending(a => a.Id).Select(a => a.Id).FirstOrDefault() + 1;

            for (int i = biggestAlbumId; i < biggestAlbumId + initialAlbumCount; i++)
            {
                db.Albums.Add(new Album
                {
                    Name = $"Album_{i}",
                    BackgroundColor = $"Colour{i}",
                    IsPublic = random.Next(0, 2) == 0 ? true : false,
                    UserId = userIds[random.Next(0, userIds.Count)]
                });                
            }
            Console.Write(".");
            db.SaveChanges();
            Console.WriteLine();

            //Add pictures to albums
            Console.Write("Adding pictures in albums");
            var allAlbums = db.Albums.ToList();
            var allPictureIds = db.Pictures.Select(p => p.Id).ToList();

            foreach (var album in allAlbums)
            {
                var pictureCount = random.Next(0, 10);
                for (int i = 0; i < pictureCount; i++)
                {
                    var currentPictureId = allPictureIds[random.Next(0, allPictureIds.Count)];
                    if (album.Pictures.Any(p => p.PictureId == currentPictureId))
                    {
                        i--;
                        continue;
                    }
                    album.Pictures.Add(new AlbumPicture
                    {
                        PictureId = currentPictureId
                    });
                }
                Console.Write(".");
            }
            db.SaveChanges();
            Console.WriteLine();
        }
    }
}