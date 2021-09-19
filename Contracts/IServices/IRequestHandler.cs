using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.Contracts.IServices
{
    public interface IRequestHandler
    {
        Task<TRes> SendAsync<TReq, TRes>(TReq requestData, string url, string authorization = null);

        Task<TRes> GetAsync<TRes>(string referenceId, string uri, string secretKey);
    }
}
