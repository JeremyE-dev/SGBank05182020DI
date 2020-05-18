using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;

namespace SGBank.Data
{
    public class FreeAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Free Account",
            Balance = 100.00M,
            AccountNumber = "12345",
            Type = AccountType.Free
        };

        //P1 Task 1 - returns null if AccountNumber not equal to _account
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
