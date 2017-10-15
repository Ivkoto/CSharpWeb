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
            PrintAllActiveUsersWithMoreThan5Friends(db);
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