namespace BankSystem.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string AccountNumeber { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public void DepositMoney(decimal value)
        {
            this.Balance += value;
        }

        public void WithdrowMoney(decimal value)
        {
            this.Balance -= value;
        }
    }
}