using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Data;
using EcommerceApi_dotNetFramework.Models.PaystackModels;
using EcommerceApi_dotNetFramework.RepositoryImplementations;
using System.Data.Entity;

namespace EcommerceApi_dotNetFramework.ReposImplementations
{
    public class PaystackRepository : GenericRepository<PaystackTransaction>, IPaystackRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<PaystackTransaction> _dbSet;

        public PaystackRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<PaystackTransaction>();
        }
    }
}