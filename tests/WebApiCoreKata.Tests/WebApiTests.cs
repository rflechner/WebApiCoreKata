using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebApiCoreKata.Tests
{
  public abstract class WebApiTests: IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    protected readonly WebApplicationFactory<Startup> Factory;
    protected readonly CustomWebApplicationFactory<Startup> CustomFactory;

    protected WebApiTests(CustomWebApplicationFactory<Startup> factory)
    {
      CustomFactory = factory;
      Factory = factory.WithWebHostBuilder(ConfigureWebHostBuilder);
    }

    protected virtual void ConfigureWebHostBuilder(IWebHostBuilder builder)
    {
      // for things like Razor, etc ...
      //builder.UseSolutionRelativeContentRoot("<SOLUTION-RELATIVE-PATH>");
    }
  }
}