using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.Models.Abstract;

namespace WebApplication1
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IPeople, NonSqlPeopleRepository > ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOption = new DeveloperExceptionPageOptions()
                {
                    SourceCodeLineCount = 3
                };


                app.UseDeveloperExceptionPage();
            }



            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("deafault.html");
            //app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("default.html");
            //app.UseFileServer(fileServerOptions);

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes=> { routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            //app.UseMvc();

            /*

           app.Use(async (context, next) =>
           {
               await context.Response.WriteAsync("Ist MW ");
               await next();
               logger.LogInformation("Response Ist MW ");
           });

           app.Use(async (context, next) =>
           {
               await context.Response.WriteAsync("IInd MW ");
               await next();
           });

           */

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Nth MW "+env.EnvironmentName);

            });




            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!"
                        + System.Diagnostics.Process.GetCurrentProcess().ProcessName
                        + _config["Test"]);
                });
                endpoints.Map("/Test", async context =>
                {
                    await context.Response.WriteAsync("Hello Test!"
                        + System.Diagnostics.Process.GetCurrentProcess().ProcessName
                        + _config["Test"]);
                });
            });




        }
    }
}
