using System.Linq;
using System.Threading.Tasks;

namespace desafio_impulso_dotnet.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}