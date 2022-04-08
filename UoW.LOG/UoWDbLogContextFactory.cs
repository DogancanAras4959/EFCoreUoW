using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UoW.LOG
{
    public class UoWDbLogContextFactory : IDesignTimeDbContextFactory<UoWDbLogContext>
    {
        public UoWDbLogContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();

            var builder = new DbContextOptionsBuilder<UoWDbLogContext>();

            // Build connection string. This requires that you have a connectionstring in the appsettings.json
            var connectionString = configuration.GetConnectionString("Loglar");
            builder.UseSqlServer(connectionString);
            // Create our DbContext.
            return new UoWDbLogContext(builder.Options);
        }
    }
}
