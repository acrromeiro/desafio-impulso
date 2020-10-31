using System;
using System.Linq;
using desafio_impulso_dotnet.Models;

namespace desafio_impulso_dotnet.Repositories
{
    public class SchoolClassRepository : BaseRepository<SchoolClass>, ISchoolClassRepository
    {
        public SchoolClassRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
        
        public IQueryable<SchoolClass> GetAllBySchoolId(int schoolId)
        {
            try
            {
                return GetAll().Where(c=>c.SchoolId == schoolId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possivel retornar a entidades: {ex.Message}");
            }
        }
    }
}