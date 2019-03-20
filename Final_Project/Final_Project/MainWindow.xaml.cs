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
using Microsoft.Win32;

namespace WorseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private LocalData localAppData;
        List<Button> contactButtons = new List<Button>();
        int lastContactIndex = 0;
        public LocalData LocalAppData
        {
            get { return localAppData; }
        }
        public MainWindow()
        {
            InitializeComponent();
            localAppData = new LocalData();
        }

        private string inputPath;



        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                inputPath = openFileDialog.FileName;
            }

        }

        private void AddToContactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactWindow contactWindow = new ContactWindow();
            if (contactWindow.ShowDialog() == false)
            {
                for (int i = lastContactIndex; i < localAppData.ContactNames.Count; ++i)
                {
                    Button button = new Button();

                    button.Content = localAppData.ContactNames[i];
                    button.Height = AddToContactsButton.Height;
                    button.Width = AddToContactsButton.Width;
                    button.HorizontalAlignment = HorizontalAlignment.Left;
                    button.VerticalAlignment = VerticalAlignment.Top;

                    contactButtons.Add(button);
                }

                for (int i = lastContactIndex; i < contactButtons.Count; ++i)
                {
                    double buttonOffset = 15.0;
                    if (i != 0)
                    {
                        contactButtons[i].Margin = new Thickness(contactButtons[i - 1].Margin.Left, buttonOffset , contactButtons[i - 1].Margin.Right, contactButtons[i - 1].Margin.Bottom);
                    }
                    else
                    {
                        contactButtons[i].Margin = new Thickness(AddToContactsButton.Margin.Left, AddToContactsButton.Margin.Top + AddToContactsButton.Height + buttonOffset, AddToContactsButton.Margin.Right, AddToContactsButton.Margin.Bottom);
                    }
                    //button.Name = button.Content + "Button";
                    contactButtons[i].FontSize = contactWindow.NameTextBox.FontSize;

                    ContactsStackPanel.Children.Add(contactButtons[i]);

                    lastContactIndex++;
                }
            }
        }
    }
}
