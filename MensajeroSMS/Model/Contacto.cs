using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroSMS.Model
{
    public class Contacto : INotifyPropertyChanged
    {

        private Boolean selected;

        public Boolean Selected
        {
            get { return selected; }
            set { 
                selected = value;
                OnChanged("Selected");
            }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String cellphone;

        public String Cellphone
        {
            get { return cellphone; }
            set { cellphone = value; }
        }

        public Contacto(String name, String cellphone)
        {
            this.name = name;
            this.cellphone = cellphone;
            this.selected = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
