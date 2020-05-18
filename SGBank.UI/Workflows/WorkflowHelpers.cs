using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public static class WorkflowHelpers
    {
        public static void MenuInputValidation (string userinput)
        {
            if (userinput != "1" && userinput != "2" && userinput != "3" && 
                userinput != "Q" && userinput != "q")
            {
                Console.WriteLine("Invalid entry, press any key to enter a new selection");
                Console.ReadKey();
                return;
                
            }

          
        }

        public static decimal DecimalInputValidation(string message)
        {
            decimal output;
            
            while(true)
            {
                Console.Write(message);
                string input = Console.ReadLine();


                if (decimal.TryParse(input, out output))
                {
                    return output;
                }
                else
                {
                    Console.WriteLine("That was not a valid entry");
                }

            }

        }

        
    }
}
