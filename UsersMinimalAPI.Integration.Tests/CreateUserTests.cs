using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace UsersMinimalAPI.Integeration.Tests
{
    public class CreateUserTests : BaseIntegrationTest
    {
        private readonly IntegrationTestWebAppFactory _factory;
        public CreateUserTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturn201CreatedStatusCodeOnSuccessfulCreation()
        {
            // Arrange
            var request = new UsersDTO
            {
                Email = "test@example.com",
                Password = "Password12",
                RegistrationDate = DateTime.UtcNow,
                CompanyName = "Test Company"
            };

            // Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/api/users", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        [Fact]
        public async Task ShouldReturn400BadRequestStatusCodeOnFailedCreation()
        {
            //Arrange
            var request = new UsersDTO
            {
                Email = "",
                Password = "pass",
                CompanyName = ""
            };
            //Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/api/users", request);
            var jsonContent = await response.Content.ReadAsStringAsync();
            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
            jsonContent.Should().NotBeEmpty();            
        }
        [Fact]
        public async Task ShouldReturn404NotFoundWhenHittingInvalidEndpoint()
        {
            // Arrange
            var request = new UsersDTO
            {
                Email = "test@example.com",
                Password = "Password12",
                RegistrationDate = DateTime.UtcNow,
                CompanyName = "Test Company"
            };
            // Act
            var response = await _factory.ApiClient.PostAsJsonAsync("/users", request);
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}