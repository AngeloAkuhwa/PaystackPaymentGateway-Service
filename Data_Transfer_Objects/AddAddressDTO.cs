using System.ComponentModel.DataAnnotations;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class AddAddressDTO
    {
        [Required]
        public string UserId { get; set; }

        public string StreetInfo { get; set; }

        [Required]
        public bool IsContactFilled { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

    }
}