// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using EFToAnyDB.SQLServer;
using EFToAnyDB.SQLite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EFToAnyDB;

Console.WriteLine("Hello, Database!");

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        Enum.TryParse(configuration.GetValue("Provider", DBProvider.Sqlite.ToString()), out DBProvider dbProvider);
        var connectionString = configuration.GetConnectionString(dbProvider.ToString());

        if (connectionString == null) throw new ArgumentException("The connection string was missing");

        switch (dbProvider)
        {
            case DBProvider.SqlServer:
                services.AddSQLServer(connectionString);
                break;
            default:
                services.AddSQLite(connectionString);
                break;
        }

        services.AddHostedService<ConsoleHostedService>();

    }).RunConsoleAsync();




