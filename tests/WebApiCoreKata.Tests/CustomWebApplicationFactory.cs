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
        
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<KataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                    
                    
                });

                
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
//                        Utilities.InitializeDbForTests(db);
                        
//                        db.Customers.Add(new CustomerDto
//                        {
//                            Id = Guid.NewGuid(),
//                            Birth = new DateTime(1988, 05, 02),
//                            FirstName = "Leti",
//                            LastName = "Coco"
//                        });

                        db.Customers.AddRange(fakeCustomers);
                        db.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}