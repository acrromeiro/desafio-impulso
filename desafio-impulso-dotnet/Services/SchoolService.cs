using System;
using System.Collections.Generic;
using System.Linq;
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
            return await this._schoolRepository.AddAsync(school);
        }
        
        public IQueryable<School> GetAll()
        {
            return  this._schoolRepository.GetAll();
        }
        
        public IQueryable<SchoolClass> GetAllSchoolClass(string id)
        {
           return this._schoolClassRepository.GetAllBySchoolId(Int32.Parse(id));
        }
    }
}