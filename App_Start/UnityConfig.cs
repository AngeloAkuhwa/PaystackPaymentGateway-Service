using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Contracts.IServices;
using EcommerceApi_dotNetFramework.Controllers;
using EcommerceApi_dotNetFramework.ReposImplementations;
using EcommerceApi_dotNetFramework.RepositoryImplementations;
using EcommerceApi_dotNetFramework.ServicesImplementation;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace EcommerceApi_dotNetFramework
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //container.Resolve<ApplicationDbContext>();

            container.RegisterType<IAddressRepository, AddressRepository>();


            container.RegisterType<IAddressService, AddressService>();


            container.RegisterType<IUserService, UserService>();

            container.RegisterType<IPaystackservice, Paystackservice>();

            container.RegisterType<IRequestHandler, RequestHandler>();

            container.RegisterType<IPaystackRepository, PaystackRepository>();

            container.RegisterType<IPaystackBankAuthorizationRepository, PaystackBankAuthorizationRepository>();


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}