using AutoMapper;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Models;

namespace EcommerceApi_dotNetFramework.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddAddressDTO>().ReverseMap();

            CreateMap<AppUser, CreateUserDTO>().ReverseMap();

            CreateMap<PaystackRequestDto, RecievePaymentDto>().ReverseMap();
        }
    }
}