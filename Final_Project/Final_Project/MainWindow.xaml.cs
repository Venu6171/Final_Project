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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleTCP;
using Final_Project;
using System.Windows.Forms;

namespace Sender_Reciever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {     
            InitializeComponent();
        }

        SimpleTcpClient Sender_Reciever;
        private string inputPath;

        private void AddContactsButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();



            window.AddToContactsButton.IsPressed.ToString();
            
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog openFolderDialog = new FolderBrowserDialog())
            {
                if (openFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    inputPath = openFolderDialog.SelectedPath;
                }
                TextBoxButton.Text = inputPath;
            }
        }
    }
}
