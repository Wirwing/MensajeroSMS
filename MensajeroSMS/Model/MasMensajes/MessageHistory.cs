using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MensajeroSMS.Model.MasMensajes
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", ElementName = "SMSWSS10", IsNullable = false)]
    public partial class MessageHistory
    {

        public const int CHECK_CREDIT_OP = 12;
        public const int MESSAGES_HISTORY_OP = 11;
        public const int BATCH_SEND_IDS_RESPONSE = 4;

        public const string ALL_MESSAGES_DATA = "1..4";

        /// <remarks/>
        public string USER { get; set; }

        /// <remarks/>
        public string PASS { get; set; }

        /// <remarks/>
        public int SERVICE { get; set; }

        public string DATA { get; set; }

        public String ToXML()
        {
            var document = new XmlDocument();

            using (Stream stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(MessageHistory));
                serializer.Serialize(stream, this);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                document.Load(stream);
            }

            return document.OuterXml;
        }

    }

}
