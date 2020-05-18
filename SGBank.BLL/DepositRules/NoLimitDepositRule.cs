using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.BLL.DepositRules
{
    public class NoLimitDepositRule : IDeposit
    {
       
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();

            //if(account.Type != AccountType.Basic && account.Type != AccountType.Premium)
            if (account.Type == AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: Only basic and premium accounts can deposit with no limit. Contact IT";
                return response;
            }

            else if(amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amounts must be positive";
                return response;
            }

            else
            {
                response.Success = true;
                response.Account = account;
                response.Amount = amount;
                response.OldBalance = account.Balance;
                response.Account.Balance = response.Account
                    .Balance + amount;
                return response;
            }

         

        }
    }
}
