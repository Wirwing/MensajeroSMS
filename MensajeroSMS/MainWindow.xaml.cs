using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MensajeroSMS.Model;
using MensajeroSMS.Model.SmsMasivos;
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

        public ObservableCollection<Contacto> Contacts { get; set; }

        public bool ShowProgress { get; set; }

        public string Message { get; set; }

        public MainWindow()
        {
            ShowProgress = false;

            Contacts = new ObservableCollection<Contacto>();
            smsApi = new SmsMasivoService();

            InitializeComponent();

            btnSendMessage.IsEnabled = false;
            TbCharactersLef.Text = 0.ToString();

            getSaldo();

        }

        private void getSaldo()
        {

            var credit = smsApi.GetSaldo();
            //TbMessage.Text = "Mensajes disponibles: " + credit.Quantity;

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
                widow.IsEnabled = false;
                ShowProgress = true;

                var reader = new ExcelReader(dlg.FileName);

                List<Contacto> contactsRead = reader.readData();

                Contacts.Clear();
                contactsRead.ForEach(contact => { Contacts.Add(contact); });
                LoadedContacts.Text = "Contactos Cargados (" + Contacts.Count + ")";

                widow.IsEnabled = true;
                ShowProgress = false;

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
            List<Contacto> selected = Contacts.Where(contact => contact.Selected).ToList();

            string numbers = NumberFetcher.FromContacts(selected);

            BatchResponse response = smsApi.SendMessageToNumbers(Message, numbers);

            getSaldo();

        }

        private void tbMessageArea_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var length = tbMessageArea.Text.ToCharArray().Length;
            TbCharactersLef.Text = length.ToString();

            btnSendMessage.IsEnabled = length > 0 && length < 140;

        }

        private void AddContact_Click(object sender, RoutedEventArgs e)
        {
            Contacts.Add(new Contacto("", ""));
        }
     
    }
}