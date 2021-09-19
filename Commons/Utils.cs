using EcommerceApi_dotNetFramework.App_Start;
using EcommerceApi_dotNetFramework.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceApi_dotNetFramework.Commons
{
    public static class Utils
    {
        private static ApplicationUserManager _userMgr;

        public static ApplicationUserManager UserMgr
        {
            get => _userMgr ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            set => _userMgr = value;
        }
        public static string Email { get; set; }

        public static async Task<AppUser> GetLoggedInUserDetails()
        {
           return await UserMgr.FindByEmailAsync(Email);
        }

    }
}