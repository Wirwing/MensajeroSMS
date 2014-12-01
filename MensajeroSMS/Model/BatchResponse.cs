using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroSMS.Model
{
    class BatchResponse
    {

        [JsonProperty("respuestas")]
        public List<SingleResponse> Responses { get; set; }

    }

    class SingleResponse{

        [JsonProperty("estatus")]
        public String Status { get; set; }

        [JsonProperty("mensaje")]
        public String Message { get; set; }

        [JsonProperty("codigo")]
        public String Code { get; set; }

        [JsonProperty("numcelular")]
        public String CellNumber { get; set; }

    }

}
