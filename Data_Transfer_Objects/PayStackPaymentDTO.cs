using Newtonsoft.Json;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class PayStackPaymentDTO
    {
        [JsonProperty]
        public string Email { get; set; }

        [JsonProperty]
        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set => _amount = 100 * value;
        }
        //public string Currency { get; set; }

        ////optional properties
        //public string FirstName { get; set; }

        //public string LastName { get; set; }
    }
}