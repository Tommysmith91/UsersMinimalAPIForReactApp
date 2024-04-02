using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UsersMinimalAPI.Authentication;

namespace UsersMinimalAPI.Unit.Tests
{
    public class JWTTokenGeneratorTests
    {
        private const string TEST_USERNAME = "testuser";
        [Fact]
        public void GenerateToken_ReturnsValidToken_WhenProvidedWithTokenFromConfig()
        {
            // Arrange
            var configuration = SetupConfigurationWithTokenValue();
            

            var generator = new JWTTokenGenerator(configuration);          

            // Act
            var token = generator.GenerateToken(TEST_USERNAME);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void GenerateTokenThrowsArgumentNullExceptionWhenTokenIsMissingFromConfig()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();            

            var generator = new JWTTokenGenerator(configuration);           

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => generator.GenerateToken(TEST_USERNAME));
        }
        private string GenerateRandomKey()
        {
            using(var hmac = new HMACSHA512())
            {
                return BitConverter.ToString(hmac.Key).Replace("-","");
            }
        }
        private IConfiguration SetupConfigurationWithTokenValue()
        {
            var inMemorySettings = new Dictionary<string, string> {
            {"Token", GenerateRandomKey()},           
            };
            return new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }
    }
}
