using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiCoreKata.Persistence;
using WebApiCoreKata.Persistence.SqlServer;
using WebApiCoreKata.Persistence.SqlServer.Database;

namespace WebApiCoreKata
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            services.AddDbContext<KataContext>(o =>
            {
                var connectionString = Configuration["SqlConnectionString"];
                o.UseSqlServer(connectionString);
            });
            
            services.AddScoped<ICustomersRepository, CustomersRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use((context, next) =>
            {
                context.Response.Headers.Add("lala", "toto");
                
                return next();
            });

            app.Use((ctx, next) =>
            {
                if (!ctx.Request.Path.HasValue || ctx.Request.Path.Value != "/ping")
                    return next();
                
                using (var writer = new StreamWriter(ctx.Response.Body))
                {
                    writer.WriteLine("PONG");
                        
                    writer.Flush();
                    ctx.Response.Body.Flush();
                }
                    
                return Task.CompletedTask;
            });
            
            app.UseMvc();

        }
    }
}
