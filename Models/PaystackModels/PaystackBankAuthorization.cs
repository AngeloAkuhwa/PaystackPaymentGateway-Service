using Newtonsoft.Json;
using System;

namespace EcommerceApi_dotNetFramework.Models.PaystackModels
{
    public class PaystackBankAuthorization
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ReferenceId { get; set; }

        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }

        [JsonProperty("last4")]
        public string LastFour { get; set; }

        [JsonProperty("exp_month")]
        public string ExpiryMonth { get; set; }

        [JsonProperty("exp_year")]
        public string ExpiryYear { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }
    }
}