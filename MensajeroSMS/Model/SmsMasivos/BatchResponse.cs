using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MensajeroSMS.Model
{
    public class BatchResponse
    {
        [JsonProperty("respuestas")]
        public List<SingleResponse> Responses { get; set; }
    }

    public class SingleResponse
    {
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