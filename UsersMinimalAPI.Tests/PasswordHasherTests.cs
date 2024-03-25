using FluentAssertions;
using UsersMinimalAPI.Authentication;

namespace UsersMinimalAPI.Tests
{
    public class PasswordHasherTests
    {
        private const string TEST_PASSWORD = "P455W0RD";
        [Fact]
        public void HashingPasswordStringReturnsSuccessfullyHashedPassword()
        {
            //Arrange
            var sut = new PasswordHasher();
            
            //Act
            var result = sut.HashPassword(TEST_PASSWORD);
            //Assert
            result.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void HashingSamePasswordTwiceProducesDifferentResults()
        {
            //Arrange
            var sut = new PasswordHasher();            
            //Act
            var result = sut.HashPassword(TEST_PASSWORD);
            var result2 = sut.HashPassword(TEST_PASSWORD);
            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().NotBeEquivalentTo(result2);
        }
        [Fact]
        public void HashingPasswordAndThenVerifyingSamePasswordSucceeds()
        {
            //Arrange
            var sut = new PasswordHasher();            
            //Act
            var hashedPassword = sut.HashPassword(TEST_PASSWORD);
            var result = sut.VerifyPassword(TEST_PASSWORD, hashedPassword);
            //Assert
            result.Should().BeTrue();            
        }
    }
}