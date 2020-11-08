using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using desafio_impulso_dotnet;
using desafio_impulso_dotnet.Repositories;
using desafio_impulso_dotnet.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace desafio_impulso_dotnet_test.Tests
{
    public class SchoolServiceTest
    {
        private TestServer _server;
        private DataBaseContext _dataBaseContext;
        private ISchoolService _schoolService;

        public SchoolServiceTest(){ }
        
        public void SetUp()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            
            Type t = new DataBaseContext().GetType();
            _dataBaseContext = (DataBaseContext) _server.Services.GetService(t);
            
            _schoolService = new SchoolService(new SchoolRepository(_dataBaseContext), new SchoolClassRepository(_dataBaseContext));
            
        }

        public void SetDown()
        {
            _dataBaseContext.Database.EnsureDeleted();
            _dataBaseContext.Dispose();
            
            _server.Dispose();
        }


        [Fact]
        public async Task CreateSchool()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);
            
            SetDown();
        }
        
        
        [Fact]
        public async Task CreateSchoolClass()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            var schoolClass = _schoolService.CreateSchoolClassInSchool("Turma 64", "6º ano", 30, school.Id);
            
            Assert.Equal("Turma 64", schoolClass.Result.Name);
            Assert.Equal("6º ano", schoolClass.Result.Grade);
            Assert.Equal(30, schoolClass.Result.QtdStudents);
            Assert.Equal(school.Id, schoolClass.Result.SchoolId);
            
            SetDown();
        }
        
    }
}