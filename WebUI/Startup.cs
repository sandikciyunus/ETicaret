using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Business.ValidationRules.FluentValidations;
using Core.CrossCuttingConcerns.Validation;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using FormHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.Identity;
using WebUI.Middlewares;
using WebUI.SessionService;

namespace WebUI
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
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseSqlServer(@"Server=DESKTOP-EBEN970;Database=ShopApp;integrated security=true"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;//Mutlaka sayýsal deðerler ister
                options.Password.RequireLowercase = true;//Mutlaka küçük karakter ister
                options.Password.RequiredLength = 6;//Minimum 6 karakter ister
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;//Mutlaka büyük karakterler ister

                options.Lockout.MaxFailedAccessAttempts = 5;//5 kere parolayý yanlýþ girme hakký var
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//5 dakika sonra girer
                options.Lockout.AllowedForNewUsers = true;

                //options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accesdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);//Cookinin süresi
                options.SlidingExpiration = true;//Token kullaným süresi ile ilgili
                options.Cookie = new CookieBuilder
                {
                    HttpOnly=true,
                    Name=".ShopApp.Security.Cookie",
                    SameSite=SameSiteMode.Strict
                };
            });
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddMvc();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
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
            app.CustomStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=GetAll}/{id?}");
            });

          
        }
    }
}
