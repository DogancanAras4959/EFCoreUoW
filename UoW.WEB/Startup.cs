using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using UoW.DATA.Utility;
using UoW.SERVICE.Builders.CargoSettings;
using UoW.SERVICE.Extensions;
using UoW.SERVICE.Mapper;

namespace UoW.WEB
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
            var connectionStrings = Configuration.GetConnectionString("Default");
            var connectionStrings2 = Configuration.GetConnectionString("Loglar");
            services.AddEntityFramework(connectionStrings);
            services.AddEntityFrameworkLog(connectionStrings2);
            services.AddControllersWithViews();
            services.AddDependencies();
            services.AddSession(x => x.IdleTimeout = TimeSpan.FromDays(5));
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeneralMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            var config = new SuratCargoSettings();
            Configuration.Bind("SuratKargo", config);      //  <--- This
            services.AddSingleton(config);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Bayi}/{action=SellerLogin}/{id?}");
            });
          
        }
    }
}
