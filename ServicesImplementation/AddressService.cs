using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Mappings;
using EcommerceApi_dotNetFramework.Models;
using System;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.ServicesImplementation
{
    public class AddressService : IAddressService
    {

        private readonly IAddressRepository _addressRepository;

        public AddressService( IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<Response<Address>> CreateAddress(AppUser user, AddAddressDTO model)
        {

            Response<Address> response = new Response<Address>();

            if (user != null)
            {
                var addressToCreate = AutoMap.Mapper.Map<Address>(model);
                addressToCreate.UserId = user.Id;
                addressToCreate.IsActive = true;
                addressToCreate.IsContactFilled = true;


                var addressExist = await IsAddressAlreadyExist(addressToCreate.Id);

                if (addressExist.Success)
                {
                    response.Message = "Address already exist";
                    response.Data = addressExist.Data;
                    response.StatusCode = 409;
                    return response;
                }

                var storageResult = await _addressRepository.Insert(addressToCreate);

                if (!storageResult)
                {
                    response.Data = null;
                    response.StatusCode = 400;
                    response.Message = "oops... something went wrong";
                    response.Success = false;
                    return response;
                }

                var updatedAddress = await _addressRepository.Get(addressToCreate.Id);
                response.Data = AutoMap.Mapper.Map<Address>(updatedAddress);
                response.StatusCode = 201;
                response.Message = "Address stored successfully";
                response.Success = true;
                return response;

            }

            throw new Exception("user can not be null");



        }

        public async Task<Response<Address>> GetAddress(AppUser user, string addressId)
        {
            Response<Address> response = new Response<Address>();

            if (user != null && !string.IsNullOrWhiteSpace(addressId))
            {
                var address = await _addressRepository.Get(addressId);
                if (address != null)
                {
                    response.StatusCode = 200;
                    response.Success = true;
                    response.Data = address;
                    response.Message = "Address Retrieved successfully";
                    return response;
                }

                response.StatusCode = 404;
                response.Success = false;
                response.Data = null;
                return response;
            }
            throw new Exception("user or addressId can not be null");
        }

        public async Task<Response<Address>> IsAddressAlreadyExist(string addressId)
        {
            Response<Address> response = new Response<Address>();

            var address = await _addressRepository.Get(addressId);
            if (address != null)
            {
                response.Data = address;
                response.Message = "Address already exist";
                response.StatusCode = 409;
                response.Success = true;
                return response;
            }

            response.Data = null;
            response.Message = "Address does not exist";
            response.StatusCode = 204;
            response.Success = false;
            return response;
        }

        public async Task<Response<Address>> ModifyAddress(AppUser user, AddAddressDTO model, string addressId)
        {

            Response<Address> response = new Response<Address>();

            var address = await _addressRepository.Get(addressId);

            if (address != null && user != null)
            {

                var addressToUpdate = AutoMap.Mapper.Map(model, address);

                var updateResult = await _addressRepository.Update(addressToUpdate);

                if (!updateResult)
                {
                    response.Data = null;
                    response.StatusCode = 404;
                    response.Message = "something went wrong, failed to update phoneNumber";
                    response.Success = false;
                    return response;
                }

                response.Data = await _addressRepository.Get(addressId);
                response.StatusCode = 200;
                response.Message = "Address updated successfully";
                response.Success = true;
                return response;
            }
            throw new Exception("user does not exist");
        }

        public async Task<Response<bool>> RemoveAddress(AppUser user, string addressId)
        {
            Response<bool> response = new Response<bool>();

            if (user != null && !string.IsNullOrWhiteSpace(addressId))
            {
                var adress = await _addressRepository.Get(addressId);

                if (adress != null)
                {
                    var deleteResult = await _addressRepository.Delete(adress);
                    if (!deleteResult)
                    {
                        response.Data = false;
                        response.Message = "oops something went wrong, unable to process your request";
                        response.StatusCode = 400;
                        response.Success = false;
                        return response;
                    }

                    response.Data = true;
                    response.Message = "Address Removed successfully";
                    response.StatusCode = 200;
                    response.Success = true;
                    return response;
                }

                response.Data = false;
                response.Message = "oops something went wrong, unable to process your request";
                response.StatusCode = 400;
                response.Success = false;
                return response;
            }

            throw new Exception("user or address does not exist");

        }
    }
}