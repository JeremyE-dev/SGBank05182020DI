using NUnit.Framework;
using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Responses;
using SGBank.Models.Interfaces;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTests
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {
            AccountManager manager = DIContainer.Kernel.Get<AccountManager>();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [Test]
        [TestCase("12345", "Free Account", 100, AccountType.Free, 250, false)]//fail to much deposited
        [TestCase("12345", "Free Account", 100, AccountType.Free, -100, false)]//fail negative number deposited
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, false)]//fail, not a free acount type
        [TestCase("12345", "Free Account", 100, AccountType.Free, 100, true)]

        public void FreeAccountDepositRuleTest(string accountNumber, string name, decimal balance, 
            AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new FreeAccountDepositRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);

        }

        [Test]
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, false)]//positive withdrawal-fail
        [TestCase("12345", "Free Account", 100, AccountType.Free, -150, false)]//negative withdrawal over limit
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, false)]//wrong account type
        [TestCase("12345", "Free Account", 98, AccountType.Free, -99, false)]//overdraft
        [TestCase("12345", "Free Account", 100, AccountType.Free, -100, true)]
        public void FreeAccountWithdrawRuleTest(string accountNumber, string name, decimal balance,
            AccountType accountType, decimal amount, bool expectedResult)
        {
            IWithdraw withdraw = new FreeAccountWithdrawRule();
            Account account = new Account();
            account.Name = name;
            account.AccountNumber = accountNumber;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);

        }
    }
}
