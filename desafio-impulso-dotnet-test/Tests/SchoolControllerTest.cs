using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using desafio_impulso_dotnet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace desafio_impulso_dotnet_test.Tests
{
    public class SchoolControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SchoolControllerTest()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task GetSchool()
        {
            // Act
            var jsonInString = "{\"name\":\"Escola1\"}";
            var response = await _client.PostAsync("/school",new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Contains("Escola1", responseString);
        }
        
        [Fact]
        public async Task GetSchoolClasses()
        {
            // Save one school
            var jsonInStringSchool = "{\"name\":\"Escola1\"}";
            var response = await _client.PostAsync("/school",new StringContent(jsonInStringSchool, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school");
            response.EnsureSuccessStatusCode();
            var responseString1 = await response.Content.ReadAsStringAsync();
            
            // Save one school class
            var jsonInStringSchoolClass = "{\"name\":\"Turam 63\",\"grade\":\"6\",\"qtdStudents\":\"63\",\"schoolId\":\"1\"}";
            response = await _client.PostAsync("/school/1",new StringContent(jsonInStringSchoolClass, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            
            response = await _client.GetAsync("/school/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            
            // Assert
            Assert.Contains("Turam 63", responseString);
            
        }
    }
}