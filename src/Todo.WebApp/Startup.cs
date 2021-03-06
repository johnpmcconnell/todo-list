using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.WebApp.Controllers;
using static Todo.WebApp.Controllers.ControllerExtensions;

namespace Todo.WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;
                opt.ReturnHttpNotAcceptable = true;
            });
            services.AddDbContext<TodoListDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // TODO: Fix mishandling of invalid HTTP methods
                endpoints.MapFallbackToController(
                    "api/{**slug}",
                    action: nameof(FallbackController.ApiNotFoundFallback),
                    controller: RoutingName<FallbackController>()
                );
                endpoints.MapFallbackToController(
                    "{**slug}",
                    action: nameof(FallbackController.HtmlNotFoundFallback),
                    controller: RoutingName<FallbackController>()
                );
            });
        }
    }
}
