namespace BankSystem.Client.Core
{
    using BankSystem.Client.Interfaces;
    using BankSystem.Client.IO;
    using BankSystem.Models;
    using System;

    public class AuthenticationManager : IAuthenticationManager
    {
        private User currentUser;
        
        public bool IsAuthenticated()
        {
            return this.currentUser != null;
        }

        public User GetCurrentUser()
        {
            return this.currentUser;
        }

        public void Login(User user)
        {
            if (IsAuthenticated())
            {
                throw new InvalidOperationException(OutputMessages.UserShouldLogOut);
            }

            this.currentUser = user ?? throw new InvalidOperationException(OutputMessages.InvalidUsername);
        }

        public void Logout()
        {
            if (!IsAuthenticated())
            {
                throw new InvalidOperationException(OutputMessages.UserCannotLogOut);
            }

            this.currentUser = null;
        }
    }
}