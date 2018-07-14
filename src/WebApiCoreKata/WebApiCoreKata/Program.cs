using System.Runtime.CompilerServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

[assembly: InternalsVisibleTo("WebApiCoreKata.Tests")]

namespace WebApiCoreKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            CreateWebHostBuilder(args) .Build();

        /// <summary>
        /// Create webhost builder.
        /// Integration tests scann assemblies and search this method.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
            => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
