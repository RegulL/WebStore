﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Clients.Services;
using WebStore.DAL;
using WebStore.DomainNew.Entities;
using WebStore.Interfaces;
using WebStore.Services;
using WebStore.Services.InMemory;
using WebStore.Services.SQL;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrdersService, SqlOrdersService>();
            //services.AddSingleton<IProductService, InMemoryProductService>();
            services.AddScoped<IProductService, SQLProductService>();
            services.AddTransient<IValueService, ValuesClient>();

            services.AddDbContext<WebStoreContext>(optionsAction: options => options.UseSqlServer
            (Configuration.GetConnectionString(name: "DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequiredLength = 4;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;

                option.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(o =>
            {
               o.Cookie.Expiration = TimeSpan.FromDays(100);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CookieCartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseWelcomePage("/welcome");

            app.UseMvc( routes => 
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");                
            });

            var hellomsg = Configuration["CustomHelloWorld"];

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(hellomsg);
            });
        }
    }
}