using EcommerceApi_dotNetFramework.App_Start;
using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace EcommerceApi_dotNetFramework.Controllers
{
    public class AddressController : ApiController
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserMgr
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }


        [HttpPost, Route("api/create-address")]
        [Authorize(Roles = UserRoles.generalRole)]
        [ResponseType(typeof(Response<Address>))]
        public async Task<IHttpActionResult> CreateAddress([FromBody] AddAddressDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All fields are vrequired");
            }

            var userId = RequestContext.Principal.Identity.GetUserId();
            var user = await UserMgr.FindByIdAsync(userId);

            if (HttpContext.Current.User.IsInRole(UserRoles.generalRole) && user != null)
            {
                var result = await _addressService.CreateAddress(user, model);
                if (result.Success) return Ok(result);

                var message = string.Join(", ", result.Errors.Select(x => x).ToString());
                return BadRequest(message);
            }

            return BadRequest("Invalid User");
        }




        [HttpPatch, Route("api/update-address{addressId}")]
        [Authorize(Roles = UserRoles.generalRole)]
        [ResponseType(typeof(Response<Address>))]
        public async Task<IHttpActionResult> ModifyUserAddress(string addressId, [FromBody] AddAddressDTO model)
        {
            var loggedInUser = await UserMgr.FindByIdAsync(RequestContext.Principal.Identity.GetUserId());
            var result = await _addressService.ModifyAddress(loggedInUser, model, addressId);

            if (result.Success) return Ok(result);

            return BadRequest(result.Message);
        }

        [HttpDelete, Route("api/delete-address/{addressId}")]
        [Authorize(Roles = UserRoles.generalRole)]
        [ResponseType(typeof(Response<bool>))]
        public async Task<IHttpActionResult> DeleteAddress(string addressId)
        {
            var loggedInUser = await UserMgr.FindByIdAsync(RequestContext.Principal.Identity.GetUserId());
            var result = await _addressService.RemoveAddress(loggedInUser, addressId);

            if (result.Success) return Ok(result);

            return BadRequest(result.Message);
        }

    }
}
