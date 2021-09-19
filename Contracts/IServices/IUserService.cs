using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Models;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.Contracts.IServices
{
    public interface IUserService
    {
        Task<Response<AppUser>> RegisterUserAsyn(CreateUserDTO model);

        Task<Response<AppUser>> CreateUserAsync(CreateUserDTO model);

        Task<Response<ReturnLoggedInUserDTO>> LoginAsync(LoginDTO model);

        Task<Response<AppUser>> IsUserAlreadyExistAsync(string email);

    }
}