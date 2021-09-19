using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects.PaystackVerificationDTO;
using EcommerceApi_dotNetFramework.Mappings;
using EcommerceApi_dotNetFramework.Models.PaystackModels;
using EcommerceApi_dotNetFramework.Utils;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.ServicesImplementation
{
    public class Paystackservice : IPaystackservice
    {
        private readonly IPaystackRepository _paystackRepository;
        private readonly IRequestHandler _requestHandler;
        private readonly IPaystackBankAuthorizationRepository _bankAuthorizationrepo;

        public Paystackservice(
            IPaystackRepository paystackRepository, 
            IRequestHandler requestHandler,
            IPaystackBankAuthorizationRepository bankAuthorizationrepo)
        {
            _bankAuthorizationrepo = bankAuthorizationrepo ?? throw new ArgumentNullException(nameof(bankAuthorizationrepo));
            _paystackRepository = paystackRepository ?? throw new ArgumentNullException(nameof(paystackRepository));
            _requestHandler = requestHandler ?? throw new ArgumentNullException(nameof(requestHandler));
        }

        public async Task<Response<PaystackPaymentReturnDto>> CreatePaystackPaymentIntent(RecievePaymentDto model)
        {
            Response<PaystackPaymentReturnDto> response = new Response<PaystackPaymentReturnDto>();

            var payStackMap = AutoMap.Mapper.Map<PaystackRequestDto>(model);

            var url = Path.Combine(ConfigurationManager.AppSettings["Paystack:Url"], "initialize");

            var result = await _requestHandler.SendAsync<PaystackRequestDto, PaystackPaymentReturnDto>(payStackMap, url,
                ConfigurationManager.AppSettings["Paystack:secretKey"]);

            if (result.Data is null || string.IsNullOrWhiteSpace(result.Data.AuthorizationUrl))
            {
                response.Message = result.Message;
                return response;
            }


            response.Success = true;
            response.Message = result.Message;
            response.Data = AutoMap.Mapper.Map<PaystackPaymentReturnDto>(result);

            return response;
        }

        public async Task<Response<VerifyReturnData>> VerifyPaystackPayment(string referenceId)
        {
            Response<VerifyReturnData> response = new Response<VerifyReturnData>();

            if (!string.IsNullOrWhiteSpace(referenceId))
            {
                string verficationUrl = ConfigurationManager.AppSettings["Paystack-verify:url"];
                string secretKey = ConfigurationManager.AppSettings["Paystack:secretKey"];

                var verifcationRequestResult = await _requestHandler.GetAsync<VerifyReturnData>(referenceId, verficationUrl, secretKey);

                if (verifcationRequestResult.ReturnData.GatewayResponse != PaymentStatus.Successful.ToString())
                {
                    var verificationFailedData = AutoMap.Mapper.Map<VerifyReturnData>(verifcationRequestResult);
                    response.Data = verificationFailedData;
                    response.Message = verificationFailedData.Message;
                    response.StatusCode = 400;
                    response.Success = false;
                }

                var transacetions = new PaystackTransaction();
                transacetions.FirstName = verifcationRequestResult.ReturnData.Customer.FirstName ?? "test";
                transacetions.LastName = verifcationRequestResult.ReturnData.Customer.LastName ?? "test";
                transacetions.Email = verifcationRequestResult.ReturnData.Customer.Email?? "email";
                transacetions.Currency = verifcationRequestResult.ReturnData.Currency?? "currency" ;
                transacetions.ReferenceId= referenceId;

                var saveTransactionDetails = await SaveTransactionDetails(transacetions);

                //var authorizationDetails = AutoMap.Mapper.Map<PaystackBankAuthorization>(verifcationRequestResult.ReturnData.Authorization);
                //authorizationDetails.ReferenceId = referenceId;
                //var saveAuthorizationDetails = await SaveAuthorizationDetails(authorizationDetails);

                if (saveTransactionDetails)
                {
                    var verificationReturnedData = AutoMap.Mapper.Map<VerifyReturnData>(verifcationRequestResult);
                    response.Data = verificationReturnedData;
                    response.Message = verificationReturnedData.ReturnData.GatewayResponse;
                    response.StatusCode = 200;
                    response.Success = true;
                    return response;
                }

                throw new ApplicationException("oops something went wrong");

            }
            throw new ApplicationException("reference value can not be null");
        }

        private async Task<bool> SaveTransactionDetails(PaystackTransaction model)
        {
            return await _paystackRepository.Insert(model);

            
        }

        //private async Task<bool> SaveAuthorizationDetails(PaystackBankAuthorization authorizationDEtails)
        //{
        //    return await _bankAuthorizationrepo.Insert(authorizationDEtails);
        //}
    }




}