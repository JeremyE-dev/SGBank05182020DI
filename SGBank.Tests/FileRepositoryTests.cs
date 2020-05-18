using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Data;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    class FileRepositoryTests
    {

        [Test]
        public static void LoadedAccountInfoIsCorrect() {


            FileAccountRepository TestFileRepo =
                new FileAccountRepository("C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/AccountsForTestingLoad.txt");

            //Did the file load the correct line 
            //of text into the correct slot of the array
            // 0 == 11111
            // 1 == 22222
            // 2 == 33333

            Assert.AreEqual(TestFileRepo.AccountsList[0].AccountNumber, "11111");
            Assert.AreEqual(TestFileRepo.AccountsList[1].AccountNumber, "22222");
            Assert.AreEqual(TestFileRepo.AccountsList[2].AccountNumber, "33333");

            Assert.AreEqual(TestFileRepo.AccountsList[0].Name, "Free Customer");
            Assert.AreEqual(TestFileRepo.AccountsList[1].Name, "Basic Customer");
            Assert.AreEqual(TestFileRepo.AccountsList[2].Name, "Premium Customer");

            Assert.AreEqual(TestFileRepo.AccountsList[0].Balance, 100);
            Assert.AreEqual(TestFileRepo.AccountsList[1].Balance, 100);
            Assert.AreEqual(TestFileRepo.AccountsList[2].Balance, 100);

            Assert.AreEqual(TestFileRepo.AccountsList[0].Type, AccountType.Free);
            Assert.AreEqual(TestFileRepo.AccountsList[1].Type, AccountType.Basic);
            Assert.AreEqual(TestFileRepo.AccountsList[2].Type, AccountType.Premium);

        }

        //correct account is ex
        [Test]
        [TestCase("11111")]
        [TestCase("22222")]
        [TestCase("33333")]
        public static void CorrectAccountExtractedFromFile(string accountNumber)
        {
            FileAccountRepository TestFileRepo =
                   new FileAccountRepository("C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/AccountsForTestingDeposit.txt");
            Account a = TestFileRepo.ExtractAccount(accountNumber);


            Assert.AreEqual(a.AccountNumber, accountNumber);
        }
        [Test]
        [TestCase("11111")]
        [TestCase("22222")]
        [TestCase("33333")]
        public static void CanLoadAccountTestDataFromFile(string accountNumber)
        {
            FileAccountRepository TestFileRepo =
                  new FileAccountRepository("C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/AccountsForTestingLoad.txt");
            Account a = TestFileRepo.LoadAccount(accountNumber);


            Assert.AreEqual(a.AccountNumber, accountNumber);
            Assert.IsNotNull(TestFileRepo._account);

        }

        [Test]
        [TestCase("11111")]

        public static void CorrectAmountDepositAndWithdrawFree(string accountNumber)
        {
            //after this files are in arraylist
            FileAccountRepository TestFileRepo =
                      new FileAccountRepository("C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/AccountsForTestingDeposit.txt");
            Account a = TestFileRepo.LoadAccount(accountNumber);
            decimal beginningBalance = a.Balance; //100
            IDeposit deposit = new FreeAccountDepositRule();
            deposit.Deposit(a, 10); //stores new balance in account
            TestFileRepo.SaveAccount(a);
            Assert.AreEqual(a.Balance, 110);

            IWithdraw withdraw = new FreeAccountWithdrawRule();
            withdraw.Withdraw(a, -10);
            TestFileRepo.SaveAccount(a);
            Assert.AreEqual(a.Balance, 100);

        }
        [Test]
        [TestCase("22222")]

        public static void CorrectAmountDepositedWithdrawBasic(string accountNumber)
        {
            //after this files are in arraylist
            FileAccountRepository TestFileRepo =
                      new FileAccountRepository("C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/AccountsForTestingDeposit.txt");
            Account a = TestFileRepo.LoadAccount(accountNumber);
            decimal beginningBalance = a.Balance; //100
            IDeposit deposit = new NoLimitDepositRule();
            deposit.Deposit(a, 10); //stores new balance in account
            TestFileRepo.SaveAccount(a);
            Assert.AreEqual(a.Balance, 110);

            IWithdraw withdraw = new BasicAccountWithdrawRule();
            withdraw.Withdraw(a, -10);
            TestFileRepo.SaveAccount(a);
            Assert.AreEqual(a.Balance, 100);

        }
        [Test]
        [TestCase("33333")]
        public static void CorrectAmountDepositedWithdrawPremium(string accountNumber)
        {
            //after this files are in arraylist
            FileAccountRepository TestFileRepo =
                      new FileAccountRepository("C:/Users/Jeremy/source/repos/SGBank052020/SGBank.Data/AccountsForTestingDeposit.txt");
            Account a = TestFileRepo.LoadAccount(accountNumber);
            decimal beginningBalance = a.Balance; //100
            IDeposit deposit = new NoLimitDepositRule();
            deposit.Deposit(a, 10); //stores new balance in account
            TestFileRepo.SaveAccount(a);
            Assert.AreEqual(a.Balance, 110);

            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            withdraw.Withdraw(a, -10);
            TestFileRepo.SaveAccount(a);
            Assert.AreEqual(a.Balance, 100);

        }


    }
}
