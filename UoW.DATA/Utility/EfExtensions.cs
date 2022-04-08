using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UoW.CORE.Interface;
using UoW.LOG;

namespace UoW.DATA.Utility
{
    public static class EfExtensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<UoWDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("UoW.DATA")));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        public static IServiceCollection AddEntityFrameworkLog(this IServiceCollection services, string connectionString2)
        {
            services.AddDbContext<UoWDbLogContext>(options => options.UseSqlServer(connectionString2, x => x.MigrationsAssembly("UoW.LOG")));
            services.AddScoped(typeof(IUnitOfWorkLog<>), typeof(UnitOfWorkLog<>));
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryLog<>));
            return services;
        }
    }
}
