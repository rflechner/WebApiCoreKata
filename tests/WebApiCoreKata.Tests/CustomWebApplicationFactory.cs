using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApiCoreKata.Persistence.SqlServer.Database;
using WebApiCoreKata.Persistence.SqlServer.Dto;

namespace WebApiCoreKata.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public CustomerDto[] fakeCustomers = new CustomerDto[0];
        public Action<Action<KataContext>> AccessDatabase;
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<KataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                AccessDatabase = f =>
                {
                    var sp = services.BuildServiceProvider();
               
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<KataContext>();
                        var logger = scopedServices
                            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                        
                        db.Database.EnsureCreated();
    
                        try
                        {
                            f(db);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"An error occurred seeding the " +
                                                "database with test messages. Error: {ex.Message}");
                        }
                    }
                };

                AccessDatabase(db =>
                {
                    db.Customers.AddRange(fakeCustomers);
                    db.SaveChanges();
                });
                
            });
        }
    }
}