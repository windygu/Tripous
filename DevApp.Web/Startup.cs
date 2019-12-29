using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Tripous;
using Tripous.Web;

/* new types
 Microsoft.Extensions.Hosting.IHostEnvironment
Microsoft.AspNetCore.Hosting.IWebHostEnvironment : IHostEnvironment
Microsoft.Extensions.Hosting.IHostApplicationLifetime
Microsoft.Extensions.Hosting.Environments
     */

namespace DevApp.Web
{
    public class Startup
    {
        IConfiguration Configuration;
        IWebHostEnvironment HostingEnvironment;

        /// <summary>
        /// Constructor
        /// </summary>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            HostingEnvironment = environment;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // see: https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;  // Make the session cookie essential
                //options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson()
            /*
                // the default case for serializing output to JSON is camelCase in Asp.Net Core, so we turn it off here.
                // https://stackoverflow.com/questions/38728200/how-to-turn-off-or-handle-camelcasing-in-json-response-asp-net-core
                // https://github.com/aspnet/Announcements/issues/194
                .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver());
            */
                .AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; });



            WApp.ConfigureServices(services);
        }
        //IApplicationBuilder app, IApplicationLifetime appLifetime
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
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
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=App}/{action=Index}/{id?}");
            });

            WApp.Configure(app, env, appLifetime);

        }
    }
}
