using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Models;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.Contracts.IServices
{
    public interface IAddressService
    {
        Task<Response<Address>> CreateAddress(AppUser user, AddAddressDTO model);

        Task<Response<Address>> ModifyAddress(AppUser user, AddAddressDTO model, string addressId);

        Task<Response<bool>> RemoveAddress(AppUser user, string addressId);

        Task<Response<Address>> IsAddressAlreadyExist(string addressId);

        Task<Response<Address>> GetAddress(AppUser user, string sddressId);
    }
}
