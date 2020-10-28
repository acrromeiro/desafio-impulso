using System;
using System.Collections.Generic;
using System.Linq;
using desafio_impulso_dotnet.Models;
using desafio_impulso_dotnet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
            else
            {
                return StatusCodes.Status400BadRequest;
            }
        }
        
        [HttpGet("{id}")]
        public IEnumerable<SchoolClass> GetShowSchoolClasses(string id)
        {
            var schoolClasses = _schoolService.GetAllSchoolClass(id);
            return schoolClasses != null ? schoolClasses.ToArray() : null;
        }
        
        [HttpPost("{id}")]
        public IEnumerable<SchoolClass> PostShowSchoolClasses(string id)
        {
            var schoolClasses = _schoolService.GetAllSchoolClass(id);
            return schoolClasses != null ? schoolClasses.ToArray() : null;
        }
    }

    public class RequestPostCreateSchool
    {
        public string name { get; set; }
    }
}