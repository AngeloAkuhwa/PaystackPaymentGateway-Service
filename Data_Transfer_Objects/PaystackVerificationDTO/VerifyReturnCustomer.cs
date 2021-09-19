using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects.PaystackVerificationDTO
{
    public class VerifyReturnCustomer
    {

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }
    }
}