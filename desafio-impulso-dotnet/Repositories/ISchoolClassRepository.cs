using System.Linq;
using desafio_impulso_dotnet.Models;

namespace desafio_impulso_dotnet.Repositories
{
    public interface ISchoolClassRepository : IRepository<SchoolClass>
    {
        IQueryable<SchoolClass> GetAllBySchoolId(int schoolId);
    }
}