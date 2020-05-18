using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using SGBank.Data;
using SGBank.Models.Interfaces;

namespace SGBank.BLL
{
    public class DIContainer
    {
        public static IKernel Kernel = new StandardKernel();

        static DIContainer()
        {
            string chooserType = ConfigurationManager.AppSettings["Mode"].ToString();

            if (chooserType == "File")
                Kernel.Bind<IAccountRepository>().To<FileAccountRepository>();
            else if (chooserType == "FreeTest")
                Kernel.Bind<IAccountRepository>().To<FreeAccountTestRepository>();
            else if (chooserType == "BasicTest")
                Kernel.Bind<IAccountRepository>().To<BasicAccountTestRepository>();
            else if (chooserType == "PremiumTest")
                Kernel.Bind<IAccountRepository>().To<PremiumAccountTestRepository>();
            else
                throw new Exception("Mode key in app.config not set properly!");


        }
    }
}
