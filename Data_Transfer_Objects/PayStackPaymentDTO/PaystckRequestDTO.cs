using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class PaystackRequestDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("amount")]
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value * 100; }
        }

        [JsonProperty("callback_url")]
        public string Callback_url { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; } = Guid.NewGuid().ToString();
    }
}