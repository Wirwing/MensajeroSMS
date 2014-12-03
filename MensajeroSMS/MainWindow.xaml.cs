using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MensajeroSMS.Model;
using MensajeroSMS.Service;
using Microsoft.Win32;
using NLog;

namespace MensajeroSMS
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly SmsMasivoService smsApi;

        private ObservableCollection<Contacto> _contacts;

        private ObservableCollection<SingleResponse> _responses;

        private String message;

        public ObservableCollection<Contacto> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; }
        }

        public String Message
        {
            get { return message; }
            set { message = value; }
        }

        public ObservableCollection<SingleResponse> Responses
        {
            get { return _responses; }
            set { _responses = value; }
        }

        public MainWindow()
        {

            _contacts = new ObservableCollection<Contacto>();
            _responses = new ObservableCollection<SingleResponse>();

            smsApi = new SmsMasivoService();

            InitializeComponent();

            getSaldo();

        }

        private void getSaldo()
        {

            Credit credit = smsApi.GetSaldo();
            TbCreditAvailable.Text = "Mensajes disponibles: " + credit.Quantity;

        }


        private void Load_Contacts_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box 
            var dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xlsx"; // Default file extension 
            dlg.Filter = "Excel (.xlsx)|*.xlsx"; // Filter files by extension 

            // Show open file dialog box 
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result.HasValue && result.Value)
            {
                var reader = new ExcelReader(dlg.FileName);

                List<Contacto> contactsRead = reader.readData();

                _contacts.Clear();
                contactsRead.ForEach(contact => { _contacts.Add(contact); });
                LoadedContacts.Text = "Contactos Cargados (" + Contacts.Count + ")";

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
            List<Contacto> selected = _contacts.Where(contact => contact.Selected).ToList();

            string numbers = NumberFetcher.FromContacts(selected);

            BatchResponse response = smsApi.SendMessageToNumbers(message, numbers);

            _responses.Clear();
            response.Responses.ForEach(single => _responses.Add(single));

            getSaldo();

            //try
            //{
            //    Credit credit = smsApi.GetSaldo();
            //    logger.Info(credit.Quantity);
            //}
            //catch (ApplicationException exception)
            //{
            //    logger.Info(exception.Message);
            //}
        }
    }
}