using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OperatingManagement.RemotingObjectInterface;
using OperatingManagement.RemotingHelper;
using System.Net;
using WpfObserver;

namespace OperatingManagement.WpfObserver
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private static string ValidateUser(string userName, string password)
        {
            string strPort = "8085";
            string strIP = Dns.GetHostEntry(Environment.MachineName).AddressList[0].ToString();
            
            IAccount account = OperatingManagement.RemotingClient.RemotingActivator.GetObject<IAccount>(strIP, strPort);
            string pwd = PasswordEncryptHelper.EncryptPasswordBySalt(password, "MSFTJOM@web#");

            string xml = account.ValidateUser(userName, pwd);
            //xml to entity
            AuthStatus status = EntityConvert.ToAuthStatus(xml);
            return xml;
        }
        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            //string strResult = "";
            //strResult = ValidateUser(textBoxUId.Text,textBoxPwd.Password);
            //Console.WriteLine(strResult);
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxUId.Focus();
        }
    }
}
