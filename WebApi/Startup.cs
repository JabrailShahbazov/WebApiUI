using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Versioning;
using NSwag.AspNetCore;
using NSwag.Generation.AspNetCore;

namespace WebApi
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

            services.AddControllers(options =>
                options.Filters.Add(new JsonExceptionFilterAttribute()));

            services.AddRouting(option =>
                option.LowercaseUrls = true);
            services.AddSwaggerDocument(o => o.Title = "My Awesome core API");


            //API Versions
            services.AddApiVersioning(apiVerConfig =>
            {
                apiVerConfig.AssumeDefaultVersionWhenUnspecified = true;
                apiVerConfig.DefaultApiVersion = new ApiVersion(1, 0);
                apiVerConfig.ReportApiVersions = true;
                apiVerConfig.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseExceptionHandler("/error-local-development");

            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
