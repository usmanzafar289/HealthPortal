using CarePortal.Web.Controllers;
using CarePortal.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace CarePortal.Web
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
            // Requires using Microsoft.AspNetCore.Mvc;
            services.Configure<MvcOptions>(options =>
            {
                //options.Filters.Add(new RequireHttpsAttribute());
            });

            //In-Memory
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//Session Timeout.
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // send back a ISO date
                    var settings = options.SerializerSettings;
                    //settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                    settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    settings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
                    settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
                    // dont mess with case of properties
                    //var resolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;
                    //resolver.NamingStrategy = null;
                })
                .AddFormatterMappings(options =>
                {
                    options.SetMediaTypeMappingForFormat("json", "application/json");
                })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=dashboard}/{action=index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=account}/{action=login}/{id?}");
            });

            APICallerExtensions._hostingEnvironment = env;
            APICallerExtensions._configuration = Configuration;

            AccountController._hostingEnvironment = env;
        }

    }
}
