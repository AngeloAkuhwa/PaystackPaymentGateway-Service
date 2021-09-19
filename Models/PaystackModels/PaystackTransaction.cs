using System;

namespace EcommerceApi_dotNetFramework.Models.PaystackModels
{
    public class PaystackTransaction
    {
        public PaystackTransaction()
        {
            Id = Guid.NewGuid().ToString();
        }
        //compulsory properties
        public string Id { get; set; }

        public string ReferenceId { get; set; } 

        public string Email { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        //optional properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}