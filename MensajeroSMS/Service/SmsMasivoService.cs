using System;
using MensajeroSMS.Model;
using MensajeroSMS.Model.SmsMasivos;
using Newtonsoft.Json;
using RestSharp;

namespace MensajeroSMS.Service
{
    internal class SmsMasivoService
    {
        private const String BaseUrl = "https://smsmasivos.com.mx/sms/";
        private const int RegionCodeMx = 52;

        public Credit GetSaldo()
        {
            var request = new RestRequest("api.credito.new.php");
            return JsonConvert.DeserializeObject<Credit>(Execute(request));
        }

        public BatchResponse SendMessageToNumbers(String message, String numbers)
        {
            var request = new RestRequest("api.multienvio.new.php", Method.POST);
            request.AddParameter("mensaje", message);
            request.AddParameter("numcelular", numbers);
            request.AddParameter("numregion", RegionCodeMx);
            //request.AddParameter("sandbox", 1);
            return JsonConvert.DeserializeObject<BatchResponse>(Execute(request));
        }

        private String Execute(RestRequest request)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            request.AddParameter("apikey", "5338fe9645e9d810ef5e091cfc5c988c60cdf8bf");
            IRestResponse response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }

            return response.Content;
        }
    }
}