using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace WorseApp
{
    /// <summary>
    /// Interaction logic for IPAddress.xaml
    /// </summary>
    public partial class IPAddressWindow : Window
    {
        public IPAddressWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

                mainWindow.serverIPAddress = IpAddressTextBox.Text;
                mainWindow.myName = YourNameTextBox.Text;

                mainWindow.client = new TcpClient(IpAddressTextBox.Text, 8000);

                byte[] bytes = new byte[1024];
                string message;

                message = mainWindow.myName;
                bytes = System.Text.Encoding.ASCII.GetBytes(message);
                NetworkStream stream = mainWindow.client.GetStream();
                stream.Write(bytes, 0, bytes.Length);

                Close();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "IP Address did not matched");
            }
        }
    }
}
