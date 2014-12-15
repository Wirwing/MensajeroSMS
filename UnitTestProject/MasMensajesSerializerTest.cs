using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MensajeroSMS.Model.MasMensajes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MasMensajesSerializerTest
    {
        [TestMethod]
        public void SerializeBasic()
        {

            var document = new XmlDocument();
            var data = new SMSWSS10 { USER = "wirwing", PASS = "63ac23", SERVICE = SMSWSS10.CHECK_CREDIT_OP };

            using (Stream stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(SMSWSS10));
                serializer.Serialize(stream, data);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                document.Load(stream);
            }

            Debug.Write(document.OuterXml);

        }

        [TestMethod]
        public void SerializeBatchSend()
        {

            var document = new XmlDocument();
            var sms = new SMSWSS10 { USER = "Wirwing", PASS = "63ac23" };

            var numbers = new List<string> {"9999188636", "99992564186"};
            var message = "Buenos dias, amor :)";
            sms.SetBatchMessage(numbers, message);

            using (Stream stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(SMSWSS10));
                serializer.Serialize(stream, sms);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                document.Load(stream);
            }

            Debug.Write(document.OuterXml);

        }

    }
}
