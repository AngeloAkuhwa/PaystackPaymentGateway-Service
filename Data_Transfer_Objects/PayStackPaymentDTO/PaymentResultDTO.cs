using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class PaymentResultDTO
    {
        [JsonProperty("paymentId")]
        public string PaymentId { get; set; }

        [JsonProperty("paymentStatus")]
        public string PaymentStatus { get; set; }

        [JsonProperty("paymentUrl")]
        public string PaymentUrl { get; set; }
    }
}