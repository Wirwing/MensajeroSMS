using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MensajeroSMS.Model
{
    public class Contacto : INotifyPropertyChanged
    {
        private Boolean selected;

        public Contacto(String name, String cellphone)
        {
            this.Name = name;
            this.Cellphone = cellphone;
            selected = true;
        }

        public Boolean Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnChanged("Selected");
            }
        }

        public string Name { get; set; }

        public string Cellphone { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public bool validCellphone()
        {
            //Accepts only 10 digits, no more no less. (Like Mike's answer)
            var pattern = new Regex(@"(?<!\d)\d{10}(?!\d)");
            return pattern.IsMatch(Cellphone);
        }
    }
}