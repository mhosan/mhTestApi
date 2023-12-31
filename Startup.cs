
using mhTestApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;


namespace mhTestApi
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
            services.AddControllers();

            //db service !ojo, revisar si aqui va WebApiContext o DBmhtestContext.cs 
            services.AddDbContext<DBmhtestContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:SqlMhTest.somee.com"])
            );

            services.AddCors(options =>
            {
                //var origins = Configuration.GetSection("Cors:Origins").Get<string[]>();
                //options.AddPolicy(_MyCors,
                //    builder =>
                //        builder
                //        .WithOrigins(origins)
                //        .AllowAnyMethod()
                //        .AllowAnyHeader()
                //        .AllowCredentials()
                //        );
                options.AddPolicy("NewPolicy", app =>
                {
                    app.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });

            });

            // Configurar Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MH Services",
                    Version = "v1"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();

            //    // Habilitar Swagger 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nombre de la API v1");
                });
            //}
            app.UseCors("NewPolicy");

            app.Run(async r =>
            {
                await Task.Run(() =>
                {
                    r.Response.Redirect(r.Request.PathBase.Value + "/swagger");
                });
            });
        }
    }
}

