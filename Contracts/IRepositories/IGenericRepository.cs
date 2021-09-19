using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.Contracts.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(string id);

        IQueryable<T> GetAll();

        Task<bool> Insert(T entity);

        Task<bool> Update(T enity);

        Task<bool> Delete(T entity);
    }
}
