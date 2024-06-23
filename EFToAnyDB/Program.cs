// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using EFToAnyDB.SQLServer;
using EFToAnyDB.SQLite;
using EFToAnyDB.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFToAnyDB;

Console.WriteLine("Hello, Database!");

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var provider = configuration.GetValue("Provider", "SqlServer");

        if (provider == "SqlServer")
        {
            services.AddSQLServer(configuration.GetConnectionString("SqlServerConnection"));
        }
        else
        {
            services.AddSQLite(configuration.GetConnectionString("SqliteConnection"));
        }

        services.AddHostedService<ConsoleHostedService>();

    }).RunConsoleAsync();




