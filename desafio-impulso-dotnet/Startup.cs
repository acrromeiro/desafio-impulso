using System;
using System.Reflection;
using desafio_impulso_dotnet.Repositories;
using desafio_impulso_dotnet.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace desafio_impulso_dotnet
{
    public class Startup
    {
        public String enviromentConfig;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["ConnectionStrings:DefaultConnection"];
            enviromentConfig = Configuration["env"];
            if (connection == null && enviromentConfig == null)
            {
                connection = "Data Source=test.db";
                enviromentConfig = "Tests";
            }

            services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlite(connection, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                })
            );
            
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISchoolClassRepository, SchoolClassRepository>();

            if (enviromentConfig != "Tests")
            {
                services.AddControllersWithViews();
                // In production, the Angular files will be served from this directory
                services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            }
            else
            {
                services.AddControllers();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>();
                context.Database.EnsureCreated();
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment() && enviromentConfig != "Tests")
            {
                app.UseSpaStaticFiles();
            }
            
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            if (enviromentConfig != "Tests")
            {
                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";


                    spa.UseAngularCliServer(npmScript: "start");
                });
            }
        }
    }
}