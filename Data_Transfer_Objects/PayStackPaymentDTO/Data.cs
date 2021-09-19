using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class Data
    {
        [JsonProperty("authorization_url")]
        public string AuthorizationUrl { get; set; }

        [JsonProperty("access_code")]
        public string AccessCode { get; set; }

        public string Reference { get; set; }

    }
}