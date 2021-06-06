using CouponsLtd.Data;
using CouponsLtd.Helpers;
using CouponsLtd.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace CouponsLtd
{
    public class Startup
    {
        private const string ApiHeaderName = "x-api-key";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<ApplicationDbContext>(options =>
                               options.UseSqlServer(
                                   Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddCors();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<CouponService, CouponService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CouponsLtd Api", Version = "v1" }
            );

                // Bearer token authentication
                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };
                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token specified
                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
{
    {securityScheme, new string[] { }},
};
                c.AddSecurityRequirement(securityRequirements);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouponsLtd Api V1");
            });

            app.UseRouting();

            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
