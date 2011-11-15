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
            IAccount account = OperatingManagement.RemotingClient.RemotingActivator.GetObject<IAccount>();
            string pwd = PasswordEncryptHelper.EncryptPassword("password");

            string xml = account.ValidateUser("xiongjun", pwd);

            Console.WriteLine(xml);

            AuthStatus status = EntityConvert.ToAuthStatus(xml); 

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
