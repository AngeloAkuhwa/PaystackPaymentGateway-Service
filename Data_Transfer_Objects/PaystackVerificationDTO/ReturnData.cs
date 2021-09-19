using Newtonsoft.Json;
using System;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects.PaystackVerificationDTO
{
    public class ReturnData
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("transaction_date")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("gateway_response")]
        public string GatewayResponse { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("log")]
        public PaystackLog PaystackLog { get; set; }

        [JsonProperty("authorization")]
        public VerifyReturnAuthorizationDTO Authorization { get; set; }

        [JsonProperty("customer")]
        public VerifyReturnCustomer Customer { get; set; }
    }
}