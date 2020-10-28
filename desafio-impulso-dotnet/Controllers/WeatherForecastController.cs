using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_impulso_dotnet.Repositories;
using desafio_impulso_dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace desafio_impulso_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISchoolService _schoolService;
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ISchoolService schoolService)
        {
            _logger = logger;
            _schoolService = schoolService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var school = _schoolService.Create("IGD");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}