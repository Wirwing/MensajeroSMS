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
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class SMSWSS10
    {

        public const int CHECK_CREDIT_OP = 12;
        public const int BATCH_SEND_IDS_RESPONSE = 4;

        /// <remarks/>
        public string USER { get; set; }

        /// <remarks/>
        public string PASS { get; set; }

        /// <remarks/>
        public byte SERVICE { get; set; }

        public SMSWSS10DATA DATA { get; set; }

        public String ToXML()
        {
            var document = new XmlDocument();

            using (Stream stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(SMSWSS10));
                serializer.Serialize(stream, this);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                document.Load(stream);
            }

            return document.OuterXml;
        }

        public void SetBatchMessage(List<String> numbers, String message)
        {
            
            var data = new SMSWSS10DATA { BLOCK = new SMSWSS10DATABLOCK() };
            var builder = new StringBuilder();

            for (var i = 0; i < numbers.Count; i++)
            {
                builder.Append(string.Format("{0},JohnDoe", numbers[i]));

                if (i != numbers.Count - 1)
                {
                    builder.Append(";");
                }
            }

            data.BLOCK.CELS = builder.ToString();
            data.BLOCK.TEXT = message;
            this.DATA = data;
        }


    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SMSWSS10DATA
    {
        /// <remarks/>
        public SMSWSS10DATABLOCK BLOCK { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SMSWSS10DATABLOCK
    {
        /// <remarks/>
        public string CELS { get; set; }

        /// <remarks/>
        public string TEXT { get; set; }
    }


}
