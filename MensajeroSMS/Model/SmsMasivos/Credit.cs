﻿using System;
using Newtonsoft.Json;

namespace MensajeroSMS.Model.SmsMasivos
{
    internal class Credit
    {
        [JsonProperty("estatus")]
        public String Status { get; set; }

        [JsonProperty("credito")]
        public String Quantity { get; set; }
    }
}