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
using System.Net;

namespace WorseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private LocalData localAppData;
        private ContactData contactAppData;
        private bool AutoSize { get; set; }

        List<Button> contactButtons = new List<Button>();
        private int lastContactIndex = 0;

        List<ContactData> chats = new List<ContactData>();

        List<TextBox> textBoxes = new List<TextBox>();
        private int MessageSent = 0;

        private int currentContactIndex;

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
                    ContactData contactData = new ContactData();
                    contactData.RecipientName = localAppData.ContactNames[i];

                    button.Content = localAppData.ContactNames[i];
                    button.Height = AddToContactsButton.Height;
                    button.Width = AddToContactsButton.Width;
                    button.HorizontalAlignment = HorizontalAlignment.Left;
                    button.VerticalAlignment = VerticalAlignment.Top;

                    button.Click += new RoutedEventHandler(ContactButtonClick);
                    chats.Add(contactData);

                    contactButtons.Add(button);
                }

                IPAddressWindow iPAddressWindow = new IPAddressWindow();
                iPAddressWindow.Show();

                for (int i = lastContactIndex; i < contactButtons.Count; ++i)
                {
                    double buttonOffset = 15.0;
                    if (i != 0)
                    {
                        contactButtons[i].Margin = new Thickness(contactButtons[i - 1].Margin.Left, buttonOffset, contactButtons[i - 1].Margin.Right, contactButtons[i - 1].Margin.Bottom);
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

        private void ContactButtonClick(object sender, EventArgs e)
        {
            string eventSenderName = ((Button)sender).Content.ToString();
            MessageInputBox.IsEnabled = true;
            CurrentContactNameLabel.Content = eventSenderName;
            for (int i = 0; i < chats.Count; ++i)
            {
                if (chats[i].RecipientName == eventSenderName)
                {
                    currentContactIndex = i;
                }
            }
            MessageStackPanel.Children.Clear();
            ShowMessage();
        }

        private void ShowMessage()
        {
            textBoxes.Clear();
            if(chats[currentContactIndex].MyMessages.Count == 0)
            {
                return;
            }
            for (int i = 0; i < chats[currentContactIndex].MyMessages.Count; ++i)
            {
                TextBox textbox = new TextBox();
                ContactData contactData = new ContactData();
                textbox.Height = 28;
                textbox.FontSize = 16;
                textbox.FontWeight = MessageInputBox.FontWeight;
                textbox.HorizontalAlignment = HorizontalAlignment.Right;
                textbox.VerticalAlignment = VerticalAlignment.Bottom;

                textbox.Text = chats[currentContactIndex].MyMessages[i];
                //textbox.Margin = new Thickness(412, 326, 9, 9);

                SendButton.Click += new RoutedEventHandler(SendButton_Click);

                textBoxes.Add(textbox);
            }

            for (int i = 0; i < textBoxes.Count; ++i)
            {
                double buttonOffset = 340.0;

                if (i != 0)
                {
                    textBoxes[i].Margin = new Thickness(textBoxes[i - 1].Margin.Left, 10, textBoxes[i - 1].Margin.Right, textBoxes[i - 1].Margin.Bottom);
                }
                else
                {
                    if (textBoxes.Count > 1)
                    {
                        textBoxes[i].Margin = new Thickness(textBoxes[i].Margin.Left, buttonOffset - (textBoxes.Count - 1) * 38, textBoxes[i].Margin.Right, textBoxes[i].Margin.Bottom);
                    }
                    else
                    {
                        textBoxes[i].Margin = new Thickness(textBoxes[i].Margin.Left, textBoxes[i].Margin.Top + buttonOffset, textBoxes[i].Margin.Right, textBoxes[i].Margin.Bottom);
                    }
                }
            }

            MessageStackPanel.Children.Clear();
            for (int j = 0; j < textBoxes.Count; j++)
            {
                MessageStackPanel.Children.Add(textBoxes[j]);
                textBoxes[j].IsEnabled = false;
            }

            MessageSent++;
            MessageInputBox.Text = "";

        }


        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string messageToSend = MessageInputBox.Text;
            if (messageToSend == "")
            {
                return;
            }

            chats[currentContactIndex].MyMessages.Add(messageToSend);

            ShowMessage();

            MessageInputBox.Text = "";
        }
    }
}



