using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MensajeroSMS.Model;
using MensajeroSMS.Model.MasMensajes;
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

        private readonly MasMensajesService _service;

        public ObservableCollection<Contacto> Contacts { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public bool ShowProgress { get; set; }

        public string Message { get; set; }

        public MainWindow()
        {
            ShowProgress = false;

            Contacts = new ObservableCollection<Contacto>();
            Messages = new ObservableCollection<Message>();
            _service = new MasMensajesService();

            InitializeComponent();

            btnSendMessage.IsEnabled = false;
            TbCharactersLef.Text = 0.ToString();

            Init();

        }

        private void Init()
        {
            GetSaldo();
            GetMessages();
        }

        private void GetMessages()
        {

            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {

                var sms = new MessageHistory { USER = "Wirwing", PASS = "63ac23" };
                var mesages = _service.GetHistorialMessages(sms);

                Dispatcher.Invoke((Action)(() =>
                {
                    Messages.Clear();
                    foreach (var message in mesages)
                        Messages.Add(message);
                }));

            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                BusyIndicator.IsBusy = false;
            };

            BusyIndicator.IsBusy = true;
            BusyIndicator.BusyContent = "Cargando información...";
            worker.RunWorkerAsync();

        }

        private void GetSaldo()
        {

            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                var username = Properties.Settings.Default.Username;
                var password = Properties.Settings.Default.Password;

                var sms = new SMSWSS10 { USER = username, PASS = password };
                var credit = _service.GetSaldo(sms);

                Dispatcher.Invoke((Action)(() => TbMessage.Text = credit.Replace("\r", "").Replace("\n", "")));
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                BusyIndicator.IsBusy = false;
            };

            BusyIndicator.IsBusy = true;
            BusyIndicator.BusyContent = "Cargando información...";
            worker.RunWorkerAsync();

        }

        private void Load_Contacts_Click(object sender, RoutedEventArgs e)
        {

            // Configure open file dialog box 
            var dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xlsx"; // Default file extension 
            dlg.Filter = "Excel (.xlsx)|*.xlsx"; // Filter files by extension 

            // Show open file dialog box 
            var result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (!result.HasValue || !result.Value) return;

            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {
                var reader = new ExcelReader(dlg.FileName);
                var contactsRead = reader.readData();

                Dispatcher.Invoke((Action)(() =>
                {
                    Contacts.Clear();
                    foreach (var contacto in contactsRead)
                        Contacts.Add(contacto);

                    LoadedContacts.Text = "Contactos Cargados (" + Contacts.Count + ")";

                }));
            };

            worker.RunWorkerCompleted += (o, ea) =>
            {
                BusyIndicator.IsBusy = false;
            };

            BusyIndicator.IsBusy = true;
            BusyIndicator.BusyContent = "Cargando información...";
            worker.RunWorkerAsync();
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
            foreach (var c in Contacts)
            {
                c.Selected = false;
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (o, ea) =>
            {

                try
                {
                    var selected = Contacts.Where(contact => contact.Selected).ToList();
                    var numbers = selected.Select(contacto =>
                    {
                        if (contacto.validCellphone())
                            return contacto.Cellphone;

                        throw new Exception("Número invalido para contacto " + contacto.Name);
                    }).ToList();

                    var username = Properties.Settings.Default.Username;
                    var password = Properties.Settings.Default.Password;

                    var sms = new SMSWSS10 { USER = username, PASS = password };
                    sms.SetBatchMessage(numbers, Message);

                    _service.BatchSend(sms);

                }
                catch (Exception error)
                {
                    Dispatcher.Invoke((Action)(() => MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)));
                }

            };
            worker.RunWorkerCompleted += (o, ea) =>
            {
                BusyIndicator.IsBusy = false;
                Init();
            };

            BusyIndicator.IsBusy = true;
            BusyIndicator.BusyContent = "Enviando mensaje...";
            worker.RunWorkerAsync();

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

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            Init();
        }

        private void RefreshMessages_OnClick(object sender, RoutedEventArgs e)
        {
            GetMessages();
        }
    }
}