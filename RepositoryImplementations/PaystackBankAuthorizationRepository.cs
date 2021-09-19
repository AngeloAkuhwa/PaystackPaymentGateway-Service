using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Data;
using EcommerceApi_dotNetFramework.Models.PaystackModels;
using System.Data.Entity;

namespace EcommerceApi_dotNetFramework.RepositoryImplementations
{
    public class PaystackBankAuthorizationRepository : GenericRepository<PaystackBankAuthorization>, IPaystackBankAuthorizationRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<PaystackBankAuthorization> _dbSet;

        public PaystackBankAuthorizationRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PaystackBankAuthorization>();
        }
    }
}