using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    class PremiumAccountTests
    {
        [Test]
        //what shoudl I test?
        //wrong account type - fail
        // amout deposited is negative - fail
        // deposit is successfil
        [TestCase("44444","Premium Account",100, AccountType.Free, 100, false )]//wrong account type/not passing
        [TestCase("44444", "Premium Account", 100, AccountType.Premium, -100, false)]//amount not positive- fail
        [TestCase("44444", "Premium Account", 100, AccountType.Premium, 250, true)]//success
        //what will this test do
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, 
            AccountType accountType, decimal amount, bool expectedResult)
        {
            //IDeposit contains: accountdeposit response -- 
            //AccountDepositResponse contains: Account object, Amount, Old Balance
            //Account object contains: Name, Account Number, Balance, and Type
            //Response Oject(General) Success: bool, Message: string
            //NoLimitDepositRule contains: returns response object, sets success annd Message based on 
            ////condition/rule

            IDeposit deposit = new NoLimitDepositRule();//Ideposit - contains accountdeposit resp[onse
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Type = accountType;
           
            //takes in locat account var and amount param
            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);




        }

       [Test]
       //test cases
       //fail, cannot overdraft more than 500
       [TestCase("44444", "Premium Account", 100, AccountType.Premium, -601, false )]//fail, cannot overdraft more than 500
       [TestCase("44444", "Premium Account", 100, AccountType.Premium, -599, true)]//pass, sucessful overdraft less than 500 over
       [TestCase("44444", "Premium Account", 100, AccountType.Basic, -10, false)] //fail, not a basic account type
       [TestCase("44444", "Premium Account", 100, AccountType.Premium, 10, false)] //fail, positive number withdrawn
       [TestCase("44444", "Premium Account", 100, AccountType.Premium, -10, true)]//sucessful withdrawal
        //sucessful withdrawal
        public void PremiumAccountWithdrawalTest(string accountNumber, string name, decimal balance,
          AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();

            Account account = new Account()
            {
                Name = name,
                AccountNumber = accountNumber,
                Balance = balance,
                Type = accountType

            };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expectedResult, response.Success);


        }
    }
}
