using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebProgrammingProject1.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using WebProgrammingProject1.CustomAttributes;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace WebProgrammingProject1
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
            services.AddSession();
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddRazorPages()
                                  .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                                  .AddDataAnnotationsLocalization();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });

            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var supportedCultures = new[]
            //    {
            //    new CultureInfo("tr"),
            //    new CultureInfo("en")
            //    };
            //    options.DefaultRequestCulture = new RequestCulture("tr");
            //    options.SupportedCultures = supportedCultures;
            //    options.SupportedUICultures = supportedCultures;
            //});
            services.AddDbContext<ShareItContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("ShareItContext")));
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ShareItAuthentication>();

        }

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
            

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();


            var supportedCultures = new List<CultureInfo>
            {
            new CultureInfo("en"),
            new CultureInfo("tr")
            };
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            app.UseRequestLocalization(options/* app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value*/);


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });


        }
    }
}
