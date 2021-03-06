using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EComm.Data;
using EComm.Data.EF;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EComm.Web
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
            services.AddMemoryCache();
            services.AddSession();

            services.AddAuthentication(
              CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options => { options.LoginPath = "/login"; });
            
            services.AddAuthorization(options => {
                options.AddPolicy("AdminsOnly", policy =>
                  policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });

            services.AddDbContext<ECommDataContext>(
                options => options.UseSqlServer(
                Configuration.GetConnectionString("ECommConnection")));

            services.AddScoped<IRepository, ECommDataContext>(
                sp => sp.GetService<ECommDataContext>());

            services.AddHttpContextAccessor();
            services.AddScoped<ISession>(
                sp => sp.GetService<IHttpContextAccessor>().HttpContext.Session);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/error");
            }
            app.UseStatusCodePagesWithReExecute("/clienterror", "?statuscode={0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
