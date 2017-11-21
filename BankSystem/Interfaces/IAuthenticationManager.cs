namespace BankSystem.Client.Interfaces
{
    using BankSystem.Models;

    public interface IAuthenticationManager
    {
        bool IsAuthenticated();

        void Login(User user);

        void Logout();

        User GetCurrentUser();
    }
}