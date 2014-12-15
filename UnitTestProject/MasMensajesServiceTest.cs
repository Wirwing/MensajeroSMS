using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MensajeroSMS.Model.MasMensajes;
using MensajeroSMS.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class MasMensajesServiceTest
    {

        [TestMethod]
        public void GetSaldoTest()
        {

            var data = new SMSWSS10 { USER = "Wirwing", PASS = "63ac23"};
            var service = new MasMensajesService();
            var response = service.GetSaldo(data);
            Debug.WriteLine(response);

        }

        [TestMethod]
        public void BatchSendTest()
        {
            var sms = new SMSWSS10 { USER = "Wirwing", PASS = "63ac23" };
            var numbers = new List<string> { "9999188636", "99992564186" };
            var message = "Buenos dias, amor :)";
            sms.SetBatchMessage(numbers, message);

            var service = new MasMensajesService();
            var response = service.BatchSend(sms);
            Debug.WriteLine(response);

        }

    }

}
