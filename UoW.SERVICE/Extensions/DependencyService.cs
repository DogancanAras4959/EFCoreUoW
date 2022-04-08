using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.CORE.Interface;
using UoW.DATA;
using UoW.DOMAIN.Models;
using UoW.SERVICE.Builders;
using UoW.SERVICE.Builders.Interfaces;
using UoW.SERVICE.Builders.Services;
using UoW.SERVICE.Contracts;
using UoW.SERVICE.Services;

namespace UoW.SERVICE.Extensions
{
    public static class DependencyService 
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICargoService, CargoService>();
            services.AddTransient<ICargoServiceBuilder, CargoServiceBuilder>();
            services.AddTransient<IYurticiCargoService, YurticiCargoService>();
            services.AddTransient<ISuratCargoService, SuratCargoService>();
        }
    }
}
