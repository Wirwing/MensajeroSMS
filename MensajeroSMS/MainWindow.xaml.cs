using MensajeroSMS.Model;
using MensajeroSMS.Service;
using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MensajeroSMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ObservableCollection<Contacto> contacts;

        public ObservableCollection<Contacto> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public MainWindow()
        {
            contacts = new ObservableCollection<Contacto>();
            InitializeComponent();
        }

        private void Load_Contacts_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box 
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xlsx"; // Default file extension 
            dlg.Filter = "Excel (.xlsx)|*.xlsx"; // Filter files by extension 

            // Show open file dialog box 
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result.HasValue && result.Value)
            {

                ExcelReader reader = new ExcelReader(dlg.FileName);

                List<Contacto> contactsRead = reader.readData();

                contacts.Clear();
                contactsRead.ForEach(contact =>
                {
                    contacts.Add(contact);
                });

                logger.Info(contacts.Count);
                logger.Info(contacts[0].Name);

            }
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (Contacto c in Contacts)
            {
                c.Selected = true;
            }
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (Contacto c in Contacts)
            {
                c.Selected = false;
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            List<Contacto> selected = contacts.Where(contact => contact.Selected == true).ToList();
            selected.ForEach(contact => logger.Info(contact.Name));

        }

    }
}