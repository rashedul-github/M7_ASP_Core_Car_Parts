using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project_Parts.DAL;
using Project_Parts.Models;

namespace Project_Parts
{
    public class Startup
    {
        public Startup(IConfiguration config) { this._config = config; }
        private IConfiguration _config { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarDbContext>(op => op.UseSqlServer(this._config.GetConnectionString("DbConnection")));
            services.AddScoped<ICarRepositories, CarRepositories>();
            services.AddScoped<IPartsRepositories, PartsRepositories>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
