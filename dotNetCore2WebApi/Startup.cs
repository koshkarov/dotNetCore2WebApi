using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetCore2WebApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotNetCore2WebApi
{
    public class Startup
    {
        public IConfiguration Configuraton { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuraton = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuraton.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("HelloWorld"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("MVS didn't run!");
            });
        }
    }
}
