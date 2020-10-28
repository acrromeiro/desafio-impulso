using System;
using System.Linq;
using System.Threading.Tasks;
using desafio_impulso_dotnet.Models;
using desafio_impulso_dotnet.Repositories;

namespace desafio_impulso_dotnet.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;

        public SchoolService(ISchoolRepository schoolRepository, ISchoolClassRepository schoolClassRepository)
        {
            _schoolRepository = schoolRepository;
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<School> Create(string Name)
        {
            School school = new School();
            school.Name = Name;
            return await _schoolRepository.AddAsync(school);
        }
        
        public IQueryable<School> GetAll()
        {
            return  _schoolRepository.GetAll();
        }
        
        public IQueryable<SchoolClass> GetAllSchoolClass(string id)
        {
           return _schoolClassRepository.GetAllBySchoolId(Int32.Parse(id));
        }
        
        public async Task<SchoolClass> CreateSchoolClassInSchool(string Name,string Grade,string QtdStudents,string SchoolId)
        {
            SchoolClass schoolClass = new SchoolClass();
            schoolClass.Name = Name;
            schoolClass.Grade = Grade;
            schoolClass.QtdStudents = Int32.Parse(QtdStudents);
            schoolClass.SchoolId = Int32.Parse(SchoolId);
            return await _schoolClassRepository.AddAsync(schoolClass);
        }
    }
}