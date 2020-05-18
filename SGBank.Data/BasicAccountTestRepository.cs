using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models.Interfaces;
using SGBank.Models;
namespace SGBank.Data
{
    public class BasicAccountTestRepository:IAccountRepository
    {
        private static Account _account = new Account()
        {
            Name = "Basic Account",
            Balance = 100M,
            AccountNumber = "33333",
            Type = AccountType.Basic

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
        //hmmm

        //what does the save account do/ which account is it saving/START Here 5-11-20
        //ASk Ishwar
        public void SaveAccount(Account account)
        {
            _account = account;
        }

    }
}
