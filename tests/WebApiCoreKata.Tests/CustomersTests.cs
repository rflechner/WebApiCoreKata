using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebApiCoreKata.Models;
using WebApiCoreKata.Persistence.SqlServer.Dto;
using Xunit;

namespace WebApiCoreKata.Tests
{
  public class CustomersTests : WebApiTests
  {
    public CustomersTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
    {
    }

    [Theory]
    [InlineData(1)]
    public async Task Post_CustomersSaveIt(int count)
    {
      var client = Factory.CreateClient(
        new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false
        });

      AccessDatabase(db =>
      {
        Assert.Equal(0, db.Customers.ToList().Count);
      });
      
      // Act
      var response = await PostJson(client, "/api/customers", 
        new CustomerDto
          {
            Birth = new DateTime(1999, 04, 12),
            FirstName = "Jean",
            LastName = "Paul"
          });

      // Assert
      response.EnsureSuccessStatusCode(); // Status Code 200-299

      Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
      Assert.Equal("utf-8", response.Content.Headers.ContentType.CharSet);
      
      AccessDatabase(db =>
      {
        Assert.Equal(count, db.Customers.ToList().Count);
      });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public async Task Get_AllCustomersReturnsCorrectModels(int count)
    {
      // Arrange
      CustomFactory.fakeCustomers =
        Enumerable.Range(0, count)
          .Select(_ =>
            new CustomerDto
            {
              Id = Guid.NewGuid(),
              Birth = new DateTime(1988, 05, 02),
              FirstName = "Leti",
              LastName = "Coco"
            }).ToArray();
      
      var client = Factory.CreateClient(
        new WebApplicationFactoryClientOptions
        {
          AllowAutoRedirect = false
        });
      
      // Act
      var response = await client.GetAsync("/api/customers/all");

      // Assert
      response.EnsureSuccessStatusCode(); // Status Code 200-299

      Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
      Assert.Equal("utf-8", response.Content.Headers.ContentType.CharSet);

      var json = await response.Content.ReadAsStringAsync();
      var customerPresenters = JsonConvert.DeserializeObject<CustomerPresenter[]>(json);

      Assert.Equal(count, customerPresenters.Length);
    }

    
  }
}