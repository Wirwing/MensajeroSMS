using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroSMS.Model.MasMensajes
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class SMSWSS10
    {

        public const int CHECK_CREDIT_OP = 12;

        private string uSERField;

        private string pASSField;

        private byte sERVICEField;

        /// <remarks/>
        public string USER
        {
            get
            {
                return this.uSERField;
            }
            set
            {
                this.uSERField = value;
            }
        }

        /// <remarks/>
        public string PASS
        {
            get
            {
                return this.pASSField;
            }
            set
            {
                this.pASSField = value;
            }
        }

        /// <remarks/>
        public byte SERVICE
        {
            get
            {
                return this.sERVICEField;
            }
            set
            {
                this.sERVICEField = value;
            }
        }
    }

}
