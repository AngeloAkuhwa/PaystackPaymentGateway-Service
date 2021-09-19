using EcommerceApi_dotNetFramework.App_Start;
using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Data;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Mappings;
using EcommerceApi_dotNetFramework.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceApi_dotNetFramework.ServicesImplementation
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;


        public ApplicationUserManager UserMgr
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ApplicationSignInManager SignInMgr
        {
            get => _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationRoleManager RoleMgr
        {
            get => _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            set => _roleManager = value;
        }


        public ApplicationDbContext Context
        {
            get => _context ?? HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            set => _context = value;
        }

        public async Task<Response<AppUser>> CreateUserAsync(CreateUserDTO model)
        {
            Response<AppUser> response = new Response<AppUser>();

            var newUser = AutoMap.Mapper.Map<AppUser>(model);
            newUser.UserName = model.Email;

            var createUserResult = await UserMgr.CreateAsync(newUser, model.Password);

            if (!createUserResult.Succeeded)
            {
                throw new Exception("ooops something went wrong, operation failed");
            }

            response.Message = "user cerated successfully";
            response.StatusCode = 201;
            response.Success = true;
            response.Data = await UserMgr.FindByEmailAsync(newUser.Email);

            return response;

        }

        public async Task<Response<AppUser>> IsUserAlreadyExistAsync(string email)
        {
            Response<AppUser> response = new Response<AppUser>();

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("email field is required");
            }

            var user = await UserMgr.FindByEmailAsync(email);

            if(user != null)
            {
                response.Message = "user already exist";
                response.StatusCode = 409;
                response.Success = true;

                return response;
            }

            response.Message = "user does not exist";
            response.StatusCode = 404;
            response.Success = false;

            return response;
        }

        public async Task<Response<ReturnLoggedInUserDTO>> LoginAsync(LoginDTO model)
        {
            var response = new Response<ReturnLoggedInUserDTO>();

           

            var user = await UserMgr.FindByEmailAsync(model.Email);
            ReturnLoggedInUserDTO returnUser = new ReturnLoggedInUserDTO();

            if (user != null && await UserMgr.CheckPasswordAsync(user, model.Password))
            {
                //returnUser.Token = await _jwtService.GenerateToken(user);
                returnUser.Email = user.Email;
                returnUser.Id = user.Id;
                response.Message = "Login successful";
                response.Success = true;
                response.Data = returnUser;
                response.StatusCode = 200;
                

                return response;
            }

            response.Message = "Invalid login details";
            return response; 
        }

        public async Task<Response<AppUser>> RegisterUserAsyn(CreateUserDTO model)
        {
            Response<AppUser> response = new Response<AppUser>();

            var isUserExist = await IsUserAlreadyExistAsync(model.Email);

            if (!isUserExist.Success)
            {
                var createUserResult = await CreateUserAsync(model);

                if (!createUserResult.Success)
                {
                    throw new Exception($"ooops something went wrong. user: {model.FirstName} {model.LastName} not created");
                }

                //create roles 
                await CreateRoles();
                // add user to role
                var userToRole = await AddToRoleAsync(createUserResult.Data.Id, UserRoles.generalRole);

                var user = await UserMgr.FindByEmailAsync(model.Email);

                if (userToRole)
                {
                    
                    response.Message = "user cerated successfully";
                    response.StatusCode = 201;
                    response.Success = true;
                    response.Data = user;

                    return response;
                }

                await UserMgr.DeleteAsync(user);
                response.Message = "oops something went wrong. Unsuccessful operation";
                response.StatusCode = 404;
                response.Success = false;
              
                return response;
            }

            response.Message = "user already exist";
            response.StatusCode = 409;
            response.Success = false;

            return response;
        }

        private async Task<bool> AddToRoleAsync(string userid, string userRole)
        {
            var addToRole = await UserMgr.AddToRoleAsync(userid, userRole);

            return addToRole.Succeeded != false;
        }

        private async Task CreateRoles()
        {

            var roles = new List<IdentityRole>()
                {
                    new IdentityRole(UserRoles.generalRole),
                    new IdentityRole(UserRoles.adminRole),
                };

            foreach (var item in roles)
            {
                await RoleMgr.CreateAsync(item);
                await Context.SaveChangesAsync();
            }
        }
    }
}