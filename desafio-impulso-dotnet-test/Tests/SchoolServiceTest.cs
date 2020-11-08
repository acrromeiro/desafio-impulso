using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using desafio_impulso_dotnet;
using desafio_impulso_dotnet.Exceptions;
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
        public async Task CreateInvalidSchool()
        {
            SetUp();

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.Create(""));

            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidNullSchool()
        {
            SetUp();

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.Create(null));

            SetDown();
        }
        
        
        [Fact]
        public async Task CreateSchoolClass()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            var schoolClass = _schoolService.CreateSchoolClassInSchool("Turma 64", "6º ano", 30, school.Result.Id);
            
            Assert.Equal("Turma 64", schoolClass.Result.Name);
            Assert.Equal("6º ano", schoolClass.Result.Grade);
            Assert.Equal(30, schoolClass.Result.QtdStudents);
            Assert.Equal(school.Result.Id, schoolClass.Result.SchoolId);
            
            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidNameSchoolClass()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.CreateSchoolClassInSchool("", "6º ano", 30, school.Id));

            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidNameNullSchool()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.CreateSchoolClassInSchool(null, "6º ano", 30, school.Id));

            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidGradeSchoolClass()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.CreateSchoolClassInSchool("Turma 64","", 30, school.Id));

            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidGradeNullSchool()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.CreateSchoolClassInSchool("Turma 64",null, 30, school.Id));

            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidQydStudentsSchoolClass()
        {
            SetUp();
            
            var school = _schoolService.Create("Escola1");
            
            Assert.Equal("Escola1", school.Result.Name);

            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.CreateSchoolClassInSchool("Turma 64","6º ano", -30, school.Id));

            SetDown();
        }
        
        [Fact]
        public async Task CreateInvalidSchoolSchoolClass()
        {
            SetUp();
            
            await Assert.ThrowsAsync<BusinessException>(() => _schoolService.CreateSchoolClassInSchool("Turma 64","6º ano", 30, 1));

            SetDown();
        }
        
    }
    
    
}