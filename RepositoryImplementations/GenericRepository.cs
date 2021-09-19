using EcommerceApi_dotNetFramework.Contracts.IRepositories;
using EcommerceApi_dotNetFramework.Data;
using EcommerceApi_dotNetFramework.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.RepositoryImplementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> Delete(T entity)
        {

            _dbSet.Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Get(string id)
        {
            T Item = await _dbSet.FindAsync(id);
            return Item;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> Insert(T entity)
        {
             _dbSet.Add(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}