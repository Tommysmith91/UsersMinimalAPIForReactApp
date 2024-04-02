using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Testcontainers.MsSql;

namespace UsersMinimalAPI.Integeration.Tests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        public HttpClient? ApiClient { get; private set; }
        private readonly MsSqlContainer _msSQLContainer = new MsSqlBuilder().Build();


        public Task InitializeAsync()
        {
            return _msSQLContainer.StartAsync();
        }

        Task IAsyncLifetime.DisposeAsync()
        {
            return _msSQLContainer.DisposeAsync().AsTask();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //Swap MyDB for Test Container DB.
                services.RemoveAll(typeof(DbContextOptions<UsersDbContext>));
                services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(_msSQLContainer.GetConnectionString()));
                
            });
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            //Create Test Client For Communication with Minimal API.
            var host = base.CreateHost(builder);
            ApiClient = host.GetTestClient();
            return host;
        }
    }
}
