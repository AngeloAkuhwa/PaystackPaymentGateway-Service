using EcommerceApi_dotNetFramework.Commons;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects.PaystackVerificationDTO;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.Contracts.IServices
{
    public interface IPaystackservice
    {
        Task<Response<PaystackPaymentReturnDto>> CreatePaystackPaymentIntent(RecievePaymentDto model);

        Task<Response<VerifyReturnData>> VerifyPaystackPayment(string referenceId);
    }
}
