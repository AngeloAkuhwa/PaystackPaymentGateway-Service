using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class RecievePaymentDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("callback_url")]
        public string Callback_Url { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}