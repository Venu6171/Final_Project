using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;

namespace WorseApp
{
    /// <summary>
    /// Interaction logic for IPAddressWindow.xaml
    /// </summary>
    public partial class IPAddressWindow : Window
    {
        public IPAddressWindow()
        {
            InitializeComponent();
        }

        private void ShowIPAddressWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            IPHostEntry IPHost = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (var ipAddress in IPHost.AddressList)

            {

                ShowIPAddressWindow.Text = ipAddress.ToString();

            }
        }
    }
}
