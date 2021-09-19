using System.ComponentModel.DataAnnotations;

namespace EcommerceApi_dotNetFramework.Data_Transfer_Objects
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string Email { get; set; }

        public string Gender { get; set; }

        public string CountryCode { get; set; }

        public string Number { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}