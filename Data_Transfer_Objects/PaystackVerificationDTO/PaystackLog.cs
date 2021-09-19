using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects.PaystackVerificationDTO
{
    public class PaystackLog
    {
        [JsonProperty("time_spent")]
        public int TimeSpent { get; set; }

        [JsonProperty("attempts")]
        public int AttemptsCount { get; set; }

        [JsonProperty("errors")]
        public int Errors { get; set; }

        [JsonProperty("mobile")]
        public bool FromMobile { get; set; }
    }
}