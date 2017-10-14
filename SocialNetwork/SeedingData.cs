using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Data.EntityDataModels;
using System;

namespace SocialNetwork
{
    public class SeedingData
    {
        private static Random random = new Random();
        const int initialUsersCount = 10;
        DateTime currentDate = DateTime.Now;

        public void SeedUsers(SocialNetworkDbContext db)
        {
            for (int i = 1; i < initialUsersCount * 5; i++)
            {
                db.Users.Add(new User
                {
                    Username = $"User_{i}",
                    Password = $"User#{i}_<password>$",
                    Email = $"User-{i}@email.com",
                });
            }
        }
    }
}