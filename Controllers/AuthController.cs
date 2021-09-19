using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace EcommerceApi_dotNetFramework.Controllers
{
    //[RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        public AuthController() : base()
        {

        }
        private IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }
        
        [HttpPost]
        [Route("user-register")]
        [AllowAnonymous]
        
        [ResponseType(typeof(AppUser))]
        // GET: api/Values/5
        public async Task<IHttpActionResult> RegisterUser([FromBody] CreateUserDTO dto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest("All fields are required");
            }
           
           var result = await _service.RegisterUserAsyn(dto);

            return Ok(result);
        }

        [HttpPost]
        [Route("user-login")]
        [AllowAnonymous]
        [ResponseType(typeof(ReturnLoggedInUserDTO))]
        public async Task<IHttpActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All fields are required");
            }

            var result = await _service.LoginAsync(dto);

            return Ok(result);
        }
    }
}
