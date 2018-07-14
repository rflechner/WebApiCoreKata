using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApiCoreKata.Tests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                
                //services.AddDbContext<ApplicationDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("InMemoryDbForTesting");
                //    options.UseInternalServiceProvider(serviceProvider);
                //});

                var sp = services.BuildServiceProvider();

                
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    //var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    
                    //db.Database.EnsureCreated();

                    //try
                    //{
                    //    Utilities.InitializeDbForTests(db);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.LogError(ex, $"An error occurred seeding the " +
                    //                        "database with test messages. Error: {ex.Message}");
                    //}
                }
            });
        }
    }
}