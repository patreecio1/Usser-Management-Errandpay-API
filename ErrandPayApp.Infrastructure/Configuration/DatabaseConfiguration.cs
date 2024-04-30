using System;
using System.Linq;
using ErrandPayApp.Core.Enums;
using ErrandPayApp.Core.Interfaces.Repositories;
using ErrandPayApp.Core.Interfaces.Services;
using ErrandPayApp.Data;
using ErrandPayApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ErrandPayApp.Infrastructure.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

                 services.AddEntityFrameworkSqlServer()
                .AddDbContextPool<APPContext>((serviceProvider, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(connectionString);
                    optionsBuilder.UseInternalServiceProvider(serviceProvider);
                });
        }
      

    }
}
