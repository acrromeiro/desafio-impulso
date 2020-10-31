using System;
using System.Linq;
using System.Threading.Tasks;
using desafio_impulso_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_impulso_dotnet.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel, new()
    {
        protected readonly DataBaseContext DataBaseContext;

        public BaseRepository(DataBaseContext dataBaseContext)
        {
            DataBaseContext = dataBaseContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return DataBaseContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possivel retornar a entidades: {ex.Message}");
            }
        }
        
        public Task<TEntity> GetById(int id)
        {
            try
            {
                return DataBaseContext.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possivel retornar a entidade: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} a entidade não pode ser nula");
            }

            try
            {
                await DataBaseContext.AddAsync(entity);
                await DataBaseContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} falha ao tentar salvar: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} a entidade não pode ser nula");
            }

            try
            {
                DataBaseContext.Update(entity);
                await DataBaseContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)}  falha ao tentar salvar: {ex.Message}");
            }
        }
    }
}