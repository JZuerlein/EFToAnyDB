using EFToAnyDB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EFToAnyDB
{
    internal sealed class ConsoleHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IServiceProvider _serviceProvider;

        private Task? _applicationTask;
        private int? _exitCode;

        public ConsoleHostedService(
            IServiceProvider serviceProvider,
            ILogger<ConsoleHostedService> logger,
            IHostApplicationLifetime appLifetime)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _appLifetime = appLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

            CancellationTokenSource? _cancellationTokenSource = null;

            _appLifetime.ApplicationStarted.Register(() =>
            {
                _logger.LogDebug("Application has started");
                _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                _applicationTask = Task.Run(async () =>
                {
                    try
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var services = scope.ServiceProvider;

                        var context = services.GetService<EFCoreContext>();

                        if (await ShouldApplyMigrations(context))
                        {
                            context.Database.Migrate();
                        }

                        context.People.Add(new Domain.Person("Ceaser"));
                        context.SaveChanges();

                        _exitCode = 0;
                    }
                    catch (TaskCanceledException)
                    {
                        // This means the application is shutting down, so just swallow this exception
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Unhandled exception!");
                        _exitCode = 1;
                    }
                    finally
                    {
                        // Stop the application once the work is done
                        _appLifetime.StopApplication();
                    }
                });
            });

            _appLifetime.ApplicationStopping.Register(() =>
            {
                _logger.LogDebug("Application is stopping");
                _cancellationTokenSource?.Cancel();
            });

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_applicationTask != null)
            {
                await _applicationTask;
            }

            _logger.LogDebug($"Exiting with return code: {_exitCode}");

            // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
            Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
        }

        private async Task<bool> ShouldApplyMigrations(EFCoreContext context)
        {
            return (await context.Database.GetPendingMigrationsAsync().ConfigureAwait(false)).Any();
        }
    }
}
