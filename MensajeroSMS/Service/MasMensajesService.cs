using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MensajeroSMS.Model.MasMensajes;
using RestSharp;

namespace MensajeroSMS.Service
{
    public class MasMensajesService
    {

        private const String BaseUrl = "https://www.masmensajes.com.mx/wss/";

        public String GetSaldo(SMSWSS10 data)
        {
            data.SERVICE = SMSWSS10.CHECK_CREDIT_OP;
            var request = new RestRequest("smswss10.php", Method.POST);
            request.AddParameter("xml", data.ToXML());
            return Execute(request);
        }

        public String BatchSend(SMSWSS10 data)
        {
            data.SERVICE = SMSWSS10.BATCH_SEND_IDS_RESPONSE;
            var request = new RestRequest("smswss10.php", Method.POST);
            request.AddParameter("xml", data.ToXML());
            return Execute(request);
        }

        private String Execute(IRestRequest request)
        {

            var client = new RestClient {BaseUrl = new Uri(BaseUrl)};
            var response = client.Execute(request);

            if (response.ErrorException == null) return response.Content;

            const string message = "Error retrieving response.  Check inner details for more info.";
            var twilioException = new ApplicationException(message, response.ErrorException);
            throw twilioException;
        }

    }
}
