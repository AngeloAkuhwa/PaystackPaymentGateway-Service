using EcommerceApi_dotNetFramework.Contracts.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EcommerceApi_dotNetFramework.ServicesImplementation
{
    public class RequestHandler : IRequestHandler
    {
        private readonly HttpClient _client;

        public RequestHandler(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
        }

        #region paystack payment api initialization call

        public async Task<TRes> SendAsync<TReq, TRes>(TReq requestData, string url, string secretKey = null)
        {
            if (!string.IsNullOrWhiteSpace(secretKey))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + secretKey);

            var serializedModel = JsonConvert.SerializeObject(requestData);

            var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(new Uri(url), content);

            string responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TRes>(responseContent);

            return result;
        }
        #endregion 


        #region paystack payment api verification call

        public async Task<TRes> GetAsync<TRes>(string referenceId, string varficationUri, string secretKey)
        {
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + secretKey);

            HttpResponseMessage response = await _client.GetAsync(varficationUri + referenceId);

            string resut = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TRes>(resut);

        }

        #endregion
    }
}