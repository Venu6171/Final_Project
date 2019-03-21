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
using System.Net.Sockets;
using System.ComponentModel;

namespace WorseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        BackgroundWorker BackgroundWorker = new BackgroundWorker();
        IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint remoteEP;

        Socket senderSocket;
        private LocalData localAppData;
        
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
            ReceiveMessageStackPanel.Children.Clear();
            ShowMessage();
            ReceiveMessages();

            ConnectHost();
        }

        public void ConnectHost()
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            
            remoteEP = new IPEndPoint(ipAddress, 10000);

            senderSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            senderSocket.Connect(remoteEP);


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
                        textBoxes[i].Margin = new Thickness(textBoxes[i].Margin.Left, buttonOffset - (textBoxes.Count - 1) * 38, 10, textBoxes[i].Margin.Bottom);
                    }
                    else
                    {
                        textBoxes[i].Margin = new Thickness(textBoxes[i].Margin.Left, textBoxes[i].Margin.Top + buttonOffset, 10, textBoxes[i].Margin.Bottom);
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

        public void ReceiveMessages()
        {
            textBoxes.Clear();
            if (chats[currentContactIndex].OtherMessages.Count == 0)
            {
                return;
            }
            for (int i = 0; i < chats[currentContactIndex].OtherMessages.Count; ++i)
            {
                TextBox textBox = new TextBox();
                ContactData contactData = new ContactData();
                textBox.FontSize = 18;
                textBox.Height = 28;
                textBox.FontWeight = MessageInputBox.FontWeight;
                textBox.HorizontalAlignment = HorizontalAlignment.Left;
                textBox.VerticalAlignment = VerticalAlignment.Bottom;

                textBox.Text = chats[currentContactIndex].OtherMessages[i];
                SendButton.Click += new RoutedEventHandler(SendButton_Click);

                textBoxes.Add(textBox);

            }

            for (int i = 0; i < textBoxes.Count; ++i)
            {
                double buttonOffset = 300.0;
                if (i != 0)
                {
                    textBoxes[i].Margin = new Thickness(textBoxes[i - 1].Margin.Left, 10, textBoxes[i - 1].Margin.Right, textBoxes[i - 1].Margin.Bottom);
                }
                else
                {
                    if (textBoxes.Count > 1)
                    {
                        textBoxes[i].Margin = new Thickness(10, buttonOffset - (textBoxes.Count - 1) * 38, textBoxes[i].Margin.Right, textBoxes[i].Margin.Bottom);

                    }
                    else
                    {
                        textBoxes[i].Margin = new Thickness(10, textBoxes[i].Margin.Top + buttonOffset, textBoxes[i].Margin.Right, textBoxes[i].Margin.Bottom);
                    }
                }

            }
            ReceiveMessageStackPanel.Children.Clear();
            for (int j = 0; j < textBoxes.Count; j++)
            {

                ReceiveMessageStackPanel.Children.Add(textBoxes[j]);
                textBoxes[j].IsEnabled = false;


            }


        }



        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string messageToSend = MessageInputBox.Text;
            if (messageToSend == "")
            {
                return;
            }

            chats[currentContactIndex].MyMessages.Add(messageToSend);

            MessageStackPanel.Children.Clear();

            ReceiveMessageStackPanel.Children.Clear();

            ShowMessage();
            ReceiveMessages();

            MessageInputBox.Text = "";

            StartClient(currentContactIndex);
        }

        private void StartClient(int CurrentIndex)
        {
            if (!senderSocket.Connected)
            {
                ConnectHost();
            }
            byte[] bytes = new byte[1024];
            try
            {
               
                int CurrentMessageIndex = chats[CurrentIndex].MyMessages.Count - 1;
                string SendMessage = chats[CurrentIndex].MyMessages[CurrentMessageIndex];
               
                byte[] msg = Encoding.ASCII.GetBytes(SendMessage);

                int bytesSent = senderSocket.Send(msg);
               
                int bytesRec = senderSocket.Receive(bytes);
                int ReceiveMessageIndex = chats[currentContactIndex].MyMessages.Count;
                string ReceiveMessage = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                chats[currentContactIndex].OtherMessages.Add(ReceiveMessage);
              
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

            ReceiveMessages();
        }

    }
}



