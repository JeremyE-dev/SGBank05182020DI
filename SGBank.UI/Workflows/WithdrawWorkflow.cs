using Ninject;
using SGBank.BLL;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    class WithdrawWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            AccountManager manager = DIContainer.Kernel.Get<AccountManager>();

            Console.Write("Enter an account number: ");
            string accountNumber = Console.ReadLine();

           

            decimal amount = WorkflowHelpers.DecimalInputValidation("Enter a withdrawal amount: ");

            AccountWithdrawResponse response = manager.Withdraw(accountNumber, amount);


            if (response.Success)
            {
                Console.WriteLine("Withdrawal completed!");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Withdrawn: {response.Amount:c}");
                Console.WriteLine($"New balance: {response.Account.Balance:c}");
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


        }
    }
}
