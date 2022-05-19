using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReservationWebAPI.EndpointTests
{
    public class ClientFixture : IDisposable
    {
        public IAppRepository AppRepository { get; private set; }
        public ITokenAuthorizationHandler AuthorizationHandler { get; private set; }
        public HttpClient Client { get; private set; }

        public ClientFixture()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.First(d =>
                        d.ServiceType == typeof(DbContextOptions<AppContext>));
                    services.Remove(dbContextDescriptor);
                    services.AddDbContext<AppContext>(options =>
                        options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()),
                        ServiceLifetime.Singleton, ServiceLifetime.Singleton);
                });
            });

            AuthorizationHandler = webHost.Services.CreateScope().ServiceProvider.GetService<ITokenAuthorizationHandler>();
            AppRepository = webHost.Services.CreateScope().ServiceProvider.GetService<AppContext>();
            Client = webHost.CreateClient();
        }

        //public async ValueTask DisposeAsync()
        //{
        //    await AppRepository.DisposeAsync();
        //}

        public void Dispose()
        {
            AppRepository.Dispose();
        }
    }
}
