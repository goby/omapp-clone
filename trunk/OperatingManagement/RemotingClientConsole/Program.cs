using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.RemotingObjectInterface;

namespace RemotingClientConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            IAccount account = OperatingManagement.RemotingClient.RemotingActivator.GetObject<IAccount>();
            Console.WriteLine(account.ValidateUser("admin", "123456"));
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
