namespace BankSystem.Client
{
    using BankSystem.Client.IO;
    using BankSystem.Client.Core;

    internal class Program
    {
        private static void Main()
        {
            var reader = new Reader();
            var writer = new Writer();

            var authenticateManager = new AuthenticationManager();
        }
    }
}