using EFToAnyDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace EFToAnyDB.SQLServer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSQLServer(
            this IServiceCollection services,
            string connectionString,
            Action<SqlServerDbContextOptionsBuilder>? configureSqlServerOptions = null)
        {
            services.AddDbContext<EFCoreContext>(_ => _.UseSqlServer(connectionString, sqlServerOptionsBuilder =>
            {
                sqlServerOptionsBuilder.MigrationsAssembly("EFToAnyDb.SQLServer");
                configureSqlServerOptions?.Invoke(sqlServerOptionsBuilder);
            }));

            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<EFCoreContext>();

            return services;
        }
    }
}
