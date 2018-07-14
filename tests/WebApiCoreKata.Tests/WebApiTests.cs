using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WebApiCoreKata.Persistence.SqlServer.Database;
using WebApiCoreKata.Persistence.SqlServer.Dto;
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

    public void AccessDatabase(Action<KataContext> action) => CustomFactory.AccessDatabase(action);

    protected virtual void ConfigureWebHostBuilder(IWebHostBuilder builder)
    {
      // for things like Razor, etc ...
      //builder.UseSolutionRelativeContentRoot("<SOLUTION-RELATIVE-PATH>");
    }

    public async Task<HttpResponseMessage> PostJson<T>(HttpClient client, string path, T model)
    {
      HttpContent content = new StringContent(JsonConvert.SerializeObject(model));
      content.Headers.ContentType.MediaType = "application/json";
      return await client.PostAsync(path, content);
    }
  }
}