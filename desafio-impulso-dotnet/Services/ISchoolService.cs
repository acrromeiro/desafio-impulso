using System.Threading.Tasks;
using desafio_impulso_dotnet.Models;

namespace desafio_impulso_dotnet.Services
{
    public interface ISchoolService
    {
        Task<School> create(string Name);
    }
}