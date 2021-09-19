using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Data;
using EcommerceApi_dotNetFramework.Models;
using System.Data.Entity;

namespace EcommerceApi_dotNetFramework.RepositoryImplementations
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<Address> _dbSet;

        public AddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Address>();
        }
    }
}