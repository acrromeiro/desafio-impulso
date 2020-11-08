using System;
using System.Collections.Generic;
using System.Linq;
using desafio_impulso_dotnet.Models;
using desafio_impulso_dotnet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace desafio_impulso_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public IEnumerable<School> Get()
        {
            var schools = _schoolService.GetAll();
            return schools.ToArray();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public int Post(RequestPostCreateSchool form)
        {
            var school = _schoolService.Create(form.name);
            if (school.Result != null)
            {
                return StatusCodes.Status201Created;
            }
            
            return StatusCodes.Status400BadRequest;
        }
        
        [HttpGet("{id}")]
        public IEnumerable<SchoolClass> GetShowSchoolClasses(string id)
        {
            var schoolClasses = _schoolService.GetAllSchoolClass(Int32.Parse(id));
            return schoolClasses?.ToArray();
        }
        
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public int PostShowSchoolClasses(RequestPostCreateSchoolClass form)
        {
            var school = _schoolService.CreateSchoolClassInSchool(form.name,form.grade,Int32.Parse(form.qtdStudents),Int32.Parse(form.schoolId));
            if (school.Result != null)
            {
                return StatusCodes.Status201Created;
            }

            return StatusCodes.Status400BadRequest;
        }
    }

    public class RequestPostCreateSchool
    {
        public string name { get; set; }
    }
    
    public class RequestPostCreateSchoolClass
    {
        public string name { get; set; }
        public string grade { get; set; }
        public string qtdStudents { get; set; }
        public string schoolId { get; set; }
    }
}