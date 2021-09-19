using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Data_Transfer_Objects;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EcommerceApi_dotNetFramework.Controllers
{
    public class PayStackController : ApiController
    {
        public PayStackController()
        {

        }
        private readonly IPaystackservice _paystackservice;

        public PayStackController(IPaystackservice paystackservice)
        {
            _paystackservice = paystackservice ?? throw new ArgumentNullException(nameof(paystackservice));
        }

        [HttpPost]
        [Route("paymentby-paystack")]
        public async Task<HttpResponseMessage> CreatePaystackUserPaymentIntent([FromBody] RecievePaymentDto model)
        {
            var result = await _paystackservice.CreatePaystackPaymentIntent(model);
            if (!result.Success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("verfiy-paystack-payment/{referenceId}")]
        public async Task<HttpResponseMessage> VerifyPaystackUserPayment(string referenceId)
        {
            var result = await _paystackservice.VerifyPaystackPayment(referenceId);
            if (!result.Success)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
