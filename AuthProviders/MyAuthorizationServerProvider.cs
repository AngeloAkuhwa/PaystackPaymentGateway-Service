using EcommerceApi_dotNetFramework.App_Start;
using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceApi_dotNetFramework.AuthProviders
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private static ApplicationUserManager _userMgr;

        public static ApplicationUserManager UserMgr
        {
            get => _userMgr ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            set => _userMgr = value;
        }

        //private static AppUser user = Utils.GetLoggedInUserDetails().Result;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
             context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            AppUser user = await UserMgr.FindAsync(context.UserName, context.Password);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == "admin-user" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin-user"));
                identity.AddClaim(new Claim("username", "admin-user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "admin-user"));
                context.Validated(identity);
            }
            else if (context.UserName == user.UserName && user != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, UserRoles.generalRole));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                context.Validated(identity);
            }

            else
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }

        }
    }
}