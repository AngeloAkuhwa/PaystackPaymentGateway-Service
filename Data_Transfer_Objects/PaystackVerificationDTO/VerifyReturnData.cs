using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects.PaystackVerificationDTO
{
    
    public class VerifyReturnData
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public ReturnData ReturnData { get; set; }

       
        
    }

   
}