using desafio_impulso_dotnet.Models;

namespace desafio_impulso_dotnet.Repositories
{
    public class SchoolRepository : BaseRepository<School>, ISchoolRepository
    {
        public SchoolRepository(DataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
    }
}