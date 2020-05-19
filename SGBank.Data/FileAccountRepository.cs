using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGBank.Models;
using SGBank.Models.Interfaces;


namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        //convert back to private after testing
        public string path = "C:/Users/Jeremy/source/repos/SGBank05182020DI/SGBank.Data/Accounts.txt";


        
        //change back to private after testing
        public List<Account> AccountsList = new List<Account>();

        //change back to private after testing
        public Account _account;

        public FileAccountRepository()
        {
            ReadTheFile();

        }

        public FileAccountRepository(string path)
        {
            this.path = path;
            ReadTheFile();

        }



        //this method will read the file and 
        //place each account in a list of acounts it in an list of accounts
        private void ReadTheFile()
        { 


            //applied try/catch to read file
            try
            {
                string[] rows = File.ReadAllLines(path);
                for (int i = 1; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(',');
                    Account a = new Account();
                    a.AccountNumber = columns[0];
                    a.Name = columns[1];
                    //could be an issue if decimal is not input in file correctlt
                    a.Balance = Decimal.Parse(columns[2]);

                    switch (columns[3])
                    {
                        case "F":
                            a.Type = AccountType.Free;
                            break;
                        case "B":
                            a.Type = AccountType.Basic;
                            break;
                        case "P":
                            a.Type = AccountType.Premium;
                            break;
                        default:
                            Console.WriteLine("Account Type Not Supported or Null");
                            break;

                    }

                    AccountsList.Add(a);

                }

            }

            catch(Exception e)
            {
                Console.WriteLine("There was an error when reading the file (ReadAccount), please contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);
               
            }

           

        }
        
        //change to private after testing
        public Account ExtractAccount(string AccountNumber) //fixed - code breaks because extract account is null
        {
            Account extractedAccount = null;
           foreach(Account x in AccountsList)
            {
                if (x.AccountNumber == AccountNumber)
                    extractedAccount = x;
                
            }

            return extractedAccount;
         

        }
        
        //
        public Account LoadAccount(string AccountNumber)
        {
            try
            {
                //get account from list using account number param
                _account = ExtractAccount(AccountNumber); //add this to DI version!

                if (_account == null)

                    return _account;


                else if (!AccountNumber.Equals(_account.AccountNumber))
                    return null;
                else
                {
                    return _account;
                }

            }

            catch(Exception e)
            {
                Console.Write("There was a error the File System (LoadAccount), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);
                return null;

            }


        }

        //saveaccount takes in updated account information
        public void SaveAccount(Account account)
        {
        

            _account = account;

            //find the  account number in the AccountsList
            Account accountToReplace = AccountsList.Find(x => x.AccountNumber.Contains(_account.AccountNumber));

            //find old account index
            int oldAccountIndex = AccountsList.IndexOf(accountToReplace);

            //remove the item at old accoutn index
            AccountsList.RemoveAt(oldAccountIndex);

            //insert updated account into that spot
            AccountsList.Insert(oldAccountIndex, _account);


            //write the updated AccountsList to the file    

            try 
            { 

            using (StreamWriter writer = new StreamWriter(path))
            {

                string accountTypeSymbol = "";
                string accountInfoString = "";

                writer.WriteLine("AccountNumber, Name, Balance, Type");
                foreach (var item in AccountsList)
                {
                    accountTypeSymbol = convertToAccountType(item);
                    accountInfoString = item.AccountNumber + "," + item.Name + "," + item.Balance + "," + accountTypeSymbol;

                    writer.WriteLine(accountInfoString);

                }
            }

            }

            catch (Exception e)
            {
                Console.Write("There was a error in the File System (SaveAccount), Contact IT");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
                System.Environment.Exit(0);
               

            }


        }

        private string convertToAccountType(Account a)
        {
            string result = "";
            
            switch(a.Type)
            {
                case AccountType.Free:
                    result =  "F";
                    break;
                case AccountType.Basic:
                    result = "B";
                    break;
                case AccountType.Premium:
                    result = "P";
                    break;

            }

            return result;
        }

       
    }
}
