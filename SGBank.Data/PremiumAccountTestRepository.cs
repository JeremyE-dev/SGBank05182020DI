using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;


namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account()
        { 
            Name = "Premium Account",
            Balance = 100M,
            AccountNumber = "44444",
            Type = AccountType.Premium
         
        };

        public Account LoadAccount(string AccountNumber)
        {

            if (!AccountNumber.Equals(_account.AccountNumber))
                return null;
            else
            {
                return _account;
            }

        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}
