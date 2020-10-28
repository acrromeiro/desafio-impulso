using System.Linq;
using System.Threading.Tasks;
using desafio_impulso_dotnet.Models;

namespace desafio_impulso_dotnet.Services
{
    public interface ISchoolService
    {
        Task<School> Create(string Name);
        
        IQueryable<School> GetAll();

        IQueryable<SchoolClass> GetAllSchoolClass(string id);

        Task<SchoolClass> CreateSchoolClassInSchool(string Name, string Grade, string QtdStudents, string SchoolId);
    }
}