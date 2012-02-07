using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.RemotingObjectInterface;
using OperatingManagement.RemotingHelper;

namespace RemotingClientConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IAccount account = OperatingManagement.RemotingClient.RemotingActivator.GetObject<IAccount>("127.0.0.1", "8085");
            string pwd = PasswordEncryptHelper.EncryptPasswordBySalt("w12345", "MSFTJOM@web#");

            string xml = account.ValidateUser("wangrong", pwd);

            Console.WriteLine(xml);

            AuthStatus status = EntityConvert.ToAuthStatus(xml); 

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
