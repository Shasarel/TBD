using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TBD.Interfaces;
using TBD.Services;
using System.Linq;
using TBD.Core;
using TBD.Core.Authorization;
using TBD.Core.Validation;

namespace TBD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<TBDDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IValueConverterService, ValueConverterService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IMeasurementFetcher, MeasurementFetcher>();
            services.AddTransient<IEnergyService, EnergyService>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            typeof(IValidator)
                .Assembly.GetTypes()
                .Where(t => typeof(IValidator).IsAssignableFrom(t) && !t.IsAbstract).ToList()
                .ForEach(x => services.AddTransient(x));

            services.AddTransient<IValidationProvider>(provider => new ValidationProvider(type =>
                (IValidator) provider
                    .GetService(typeof(IValidator)
                    .Assembly.GetTypes()
                    .FirstOrDefault(x => x.IsSubclassOf(typeof(Validator<>).MakeGenericType(type))))));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<AuthorizationMiddleware>();

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
