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

namespace WorseApp
{
    /// <summary>
    /// Interaction logic for SaveORLoad.xaml
    /// </summary>
    public partial class SaveORLoad : Window
    {
        SaveAndLoadXML saveAndLoadXML;
        public SaveORLoad()
        {
            InitializeComponent();

            saveAndLoadXML = new SaveAndLoadXML();
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            saveAndLoadXML.SavecontactData("ChatHistory.xml", ref mainWindow.ContactHistory);
            Close();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            mainWindow.ContactHistory.contactData.Clear();
            mainWindow.ContactHistory = saveAndLoadXML.LoadcontactData("ChatHistory.xml");
            mainWindow.ShowMessage();
            mainWindow.ReceiveMessages();
            mainWindow.GenerateContactButtons();


            
            Close();
        }
    }
}
