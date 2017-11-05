using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Models
{
    public class SavingAccount : Account
    {
        public decimal InterestRate { get; set; }

        public void AddInterestRate()
        {
            base.Balance += base.Balance * this.InterestRate;
        }
    }
}
