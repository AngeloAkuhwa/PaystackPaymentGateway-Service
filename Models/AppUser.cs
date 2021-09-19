using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi_dotNetFramework.Models
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Gender { get; set; }


        [Required]
        public string CountryCode { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public bool IsDetailsFilled { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public override string PhoneNumber
        {
            get => CountryCode + Number;
        }        

    }
}