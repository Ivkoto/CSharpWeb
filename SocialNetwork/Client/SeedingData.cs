namespace SocialNetwork.Client
{
    using SocialNetwork.Data;
    using SocialNetwork.Data.EntityDataModels;
    using System;
    using System.Linq;

    public class SeedingData
    {
        private static Random random = new Random();
        private const int initialUsersCount = 10;
        private DateTime currentDate = DateTime.Now;

        public void SeedUsers(SocialNetworkDbContext db)
        {
            Console.Write("UsersLoading");
            var biggestUserId = db.Users.OrderByDescending(u => u.Id).Select(u => u.Id).FirstOrDefault();

            for (int i = biggestUserId + 1; i < biggestUserId + initialUsersCount * 5; i++)
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
    }
}