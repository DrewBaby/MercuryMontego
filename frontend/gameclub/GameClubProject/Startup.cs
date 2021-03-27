using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameClubProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using GameClubProject.Data;
namespace GameClubProject
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
            services.AddControllersWithViews();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<GameclubDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("GameclubDBContextProd"))
                );
            else
                services.AddDbContext<GameclubDBContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("GameclubDBContext"))
                );

            services.AddScoped <IGameClubData, GameclubRepository>();

            
            //Automatically Perform Database Migration
            //services.BuildServiceProvider().GetService<GameclubDBContext>().Database.Migrate();           

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
            options.LoginPath = "/google-login"; // Must be lowercase
            options.ExpireTimeSpan = TimeSpan.FromMinutes(2);  //Set maximum timeout for Google sign in page until you have to reauth.             
            })
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =Configuration.GetSection("Authentication:Google");
                options.ClientId = "220439306478-9126hhbrkg3phc3envdv4lo7qhjvf4kp.apps.googleusercontent.com";
                options.ClientSecret = "qs_hmB7ZBvzZEkbMszCKsz8i";
            });

            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }   
            else if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseSession();
            //app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=GameQuery}/{action=Home}");
                endpoints.MapControllerRoute(
                    name: "about",
                    pattern: "{controller=GameQuery}/{action=About}");
                endpoints.MapControllerRoute(
                    name: "details",
                    pattern: "{controller=GameQuery}/{action=Details}/{id?}");


            });
        }
    }
}
