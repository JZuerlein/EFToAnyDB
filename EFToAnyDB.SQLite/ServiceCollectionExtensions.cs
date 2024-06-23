using EFToAnyDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EFToAnyDB.SQLite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSQLite(
            this IServiceCollection services,
            string connectionString,
            Action<DbContextOptionsBuilder>? configureOptions = null,
            Action<SqliteDbContextOptionsBuilder>? configureSqliteOptions = null)
        {
            services.AddDbContext<EFCoreContext>(optionsBuilder =>
            {
                configureOptions?.Invoke(optionsBuilder);
                optionsBuilder.UseSqlite(connectionString, sqliteOptionsBuilder =>
                {
                    sqliteOptionsBuilder.MigrationsAssembly("EFToAnyDB.SQLite");
                    configureSqliteOptions?.Invoke(sqliteOptionsBuilder);
                });
            });
           

            var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<EFCoreContext>();

            return services;
        }
    }
}
