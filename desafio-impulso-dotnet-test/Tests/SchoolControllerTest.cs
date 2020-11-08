using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using desafio_impulso_dotnet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace desafio_impulso_dotnet_test.Tests
{
    public class SchoolControllerTest
    {
        private TestServer _server;
        private HttpClient _client;
        private DataBaseContext _dataBaseContext;

        public SchoolControllerTest(){ }
        
        public void SetUp()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            
            Type t = new DataBaseContext().GetType();
            _dataBaseContext = (DataBaseContext) _server.Services.GetService(t);
        }

        public void SetDown()
        {
            _dataBaseContext.Database.EnsureDeleted();
            _dataBaseContext.Dispose();
            
            _client.Dispose();
            _server.Dispose();
        }


        [Fact]
        public async Task GetSchool()
        {
            SetUp();
            
            var jsonInString = "{\"name\":\"Escola1\"}";
            var response = await _client.PostAsync("/school",new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Escola1", responseString);
            
            SetDown();
        }
        
        [Fact]
        public async Task GetEmptySchool()
        {
            SetUp();
            
            var response = await _client.GetAsync("/school");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            
            Assert.Contains("[]", responseString);
            
            SetDown();
        }
        
        [Fact]
        public async Task GetSchoolClasses()
        {
            SetUp();
            
            var jsonInStringSchool = "{\"name\":\"Escola1\"}";
            var response = await _client.PostAsync("/school",new StringContent(jsonInStringSchool, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school");
            response.EnsureSuccessStatusCode();
            var responseString1 = await response.Content.ReadAsStringAsync();
            
            var jsonInStringSchoolClass = "{\"name\":\"Turam 63\",\"grade\":\"6\",\"qtdStudents\":\"63\",\"schoolId\":\"1\"}";
            response = await _client.PostAsync("/school/1",new StringContent(jsonInStringSchoolClass, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            
            // Assert
            Assert.Contains("Turam 63", responseString);
            SetDown();
        }
        
        [Fact]
        public async Task GetEmptySchoolClasses()
        {
            SetUp();
            
            var jsonInStringSchool = "{\"name\":\"Escola1\"}";
            var response = await _client.PostAsync("/school",new StringContent(jsonInStringSchool, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            
            Assert.Contains("[]", responseString);
            SetDown();
        }
    }
    
    
}