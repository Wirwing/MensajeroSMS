using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroSMS.Model
{
    class Credit
    {
        [JsonProperty("estatus")]
        public String Status { get; set; }

        [JsonProperty("credito")]
        public String Quantity{ get; set; }

    }
}
