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
using Sender_Reciever;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Name = TextBoxLabel.Text;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string IPAddress = TextBoxLabel_1.Text;
        }

        private void AddToContactsButton_Click(object sender, RoutedEventArgs e)
        {
            Label NewName = new Label();
            for (int i = 0; i < TextBoxLabel.SelectionLength; i++)
            {
                NewName.Name = TextBoxLabel.Text;

                NewName.Width = 45;
                NewName.Height = 15;
                NewName.FontSize = 24;
                
            }
        }
    }
}
