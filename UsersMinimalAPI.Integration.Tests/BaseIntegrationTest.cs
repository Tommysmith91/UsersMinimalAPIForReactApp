using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersMinimalAPI.Authentication;
using UsersMinimalAPI.Repositaries;

namespace UsersMinimalAPI.Integeration.Tests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
    {
        private readonly IServiceScope _scope;
        protected UsersDbContext _dbContext { get; }
        protected IPasswordHasher _passwordHasher { get; }
        protected IJWTTokenGenerator _jwtTokenGenerator { get; }
        protected IUsersCommandRepositary _usersCommandRepositary { get; }
        protected IUsersQueryRepositary _usersQueryRepositary { get; }

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<UsersDbContext>();
            _passwordHasher = _scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
            _jwtTokenGenerator = _scope.ServiceProvider.GetRequiredService<IJWTTokenGenerator>();
            _usersCommandRepositary = _scope.ServiceProvider.GetRequiredService<IUsersCommandRepositary>();
            _usersQueryRepositary = _scope.ServiceProvider.GetRequiredService<IUsersQueryRepositary>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
