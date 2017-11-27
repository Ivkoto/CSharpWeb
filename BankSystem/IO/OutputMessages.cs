namespace BankSystem.Client.IO
{
    public class OutputMessages
    {
        public const string UserShouldLogOut = "Current user should logout first!";
        public const string UserCannotLogOut = "Cannot log out. No user was logged in!";

        public const string InvalidUsername = "Incorrect sername!";
        public const string InvalidCommand = "Command not supported!";
    }
}