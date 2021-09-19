using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class PaystackPaymentReturnDto
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}