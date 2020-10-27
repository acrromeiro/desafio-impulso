using System.Threading.Tasks;
using desafio_impulso_dotnet.Models;
using desafio_impulso_dotnet.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace desafio_impulso_dotnet.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<School> create(string Name)
        {
            School school = new School();
            school.Name = Name;
            return await this._schoolRepository.AddAsync(school);
        }
    }
}