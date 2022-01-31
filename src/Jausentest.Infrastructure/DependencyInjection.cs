using Jausentest.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;


namespace Jausentest.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<JausentestContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        configuration.GetConnectionString("JausentestDbConnection"),
                        new MySqlServerVersion(new Version(8, 0, 23)))
                // Everything from this point on is optional but helps with debugging.
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()

            );

            return services;
        }


    }
}
