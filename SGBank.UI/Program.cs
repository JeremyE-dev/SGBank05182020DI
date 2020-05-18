using SGBank.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    class Program
    {
        static void Main(string[] args)
        {
        Menu.Start();


     
         //string path = @"C:\Users\Jeremy\source\repos\SGBank052020\SGBank.Data\Accounts.txt";

         //List<Account> AccountsList = new List<Account>();

         //   ReadTheFile(path, AccountsList);

         //   foreach(Account x in AccountsList)
         //   {
         //       Console.WriteLine(x.AccountNumber);
         //   }

         //   Console.ReadLine();




        }


        //public static  void ReadTheFile(string path,List<Account> AccountsList)
        //{
        //    if (File.Exists(path))
        //    {
        //        string[] rows = File.ReadAllLines(path);
        //        for (int i = 1; i < rows.Length; i++)
        //        {
        //            string[] columns = rows[i].Split(',');
        //            Account a = new Account();
        //            a.AccountNumber = columns[0];
        //            a.Name = columns[1];
        //            a.Balance = Decimal.Parse(columns[2]);

        //            switch (columns[3])
        //            {
        //                case "F":
        //                    a.Type = AccountType.Free;
        //                    break;
        //                case "B":
        //                    a.Type = AccountType.Basic;
        //                    break;
        //                case "P":
        //                    a.Type = AccountType.Premium;
        //                    break;
        //                default:
        //                    Console.WriteLine("Account Type Not Supported or Null");
        //                    break;//maybe adapt this to return??

        //            }

        //            AccountsList.Add(a);

        //        }
        //    }

        //    else
        //    {
        //        Console.WriteLine("could not find file at {0}", path);

        //    }

        //}





    }

}

        
    

