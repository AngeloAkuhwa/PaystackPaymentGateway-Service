using EcommerceApi_dotNetFramework.Data;
using EcommerceApi_dotNetFramework.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace EcommerceApi_dotNetFramework.App_Start
{
    public class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store) : base(store)
        {

        }


        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<AppUser>(context.Get<ApplicationDbContext>()));

            manager.UserValidator = new UserValidator<AppUser>(manager)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = false,
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = true,
                RequiredLength = 6
            };

            // Configure user lockout defaults  
            manager.UserLockoutEnabledByDefault = true;

            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);

            manager.MaxFailedAccessAttemptsBeforeLockout = 10;


            var dataProtectionProvider = options.DataProtectionProvider;

            if(dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
                
        }

    }
}