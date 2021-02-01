using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Company.Services;
using Company.WebApi.Filters;

namespace Company.WebApi
{
    public class Startup
    {
        private const string connectionStringKey = "DbContext:ConnectionString";
        private const string angularAllowOrigins = "AngularAllowOrigins";
        private const string clientUrl = "ApplicationSettings:ClientURL";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration[connectionStringKey];
            services.AddCompanyServices(connectionString);
            services.AddControllers(opt => opt.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddCors(options => options.AddPolicy(name: angularAllowOrigins,
                builder => builder.WithOrigins(Configuration[clientUrl].ToString()).AllowAnyHeader().AllowAnyMethod()));

            services.AddSwaggerGen();
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Company API");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(angularAllowOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
