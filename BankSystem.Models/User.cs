namespace BankSystem.Models
{
    using BankSystem.Models.Validators;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        [Required]
        [MinLength(3)]
        [UsernameValidator]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [PasswordValidator]
        public string Password { get; set; }

        [Required]
        [EmailValidator]
        public string Email { get; set; }

        public ICollection<SavingAccount> SavingAccounts { get; set; } = new List<SavingAccount>();

        public ICollection<CheckingAccount> CheckingAccounts { get; set; } = new List<CheckingAccount>();
    }
}