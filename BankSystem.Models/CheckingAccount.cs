namespace BankSystem.Models
{
    public class CheckingAccount : Account
    {
        public decimal Fee { get; set; }

        public void DeductFee()
        {
            base.Balance -= base.Balance - this.Fee;
        }
    }
}