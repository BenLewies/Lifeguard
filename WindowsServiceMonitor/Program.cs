using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Diagnostics; // For managing processes

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService() // This makes the application run as a Windows service
            .ConfigureServices((hostContext, services) => { services.AddHostedService<Worker>(); });
}

public class Worker : BackgroundService
{
    private readonly string[] _servicesToMonitor;
    private readonly string[] _processesToMonitor;
    private int _refreshIntervalInSeconds;

    public Worker(IHostEnvironment env)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        _servicesToMonitor = config.GetSection("ServicesToMonitor").Get<string[]>();
        _processesToMonitor = config.GetSection("ProcessesToMonitor").Get<string[]>();
        _refreshIntervalInSeconds = config.GetSection("RefreshIntervalInSeconds").Get<int>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            EnsureMonitoredServices();
            EnsureMonitoredProcesses(); // Monitor additional processes

            await Task.Delay(1000 * _refreshIntervalInSeconds,
                stoppingToken); // e.g., check every _refreshIntervalInSeconds seconds
        }
    }

    private void EnsureMonitoredServices()
    {
        foreach (var serviceName in _servicesToMonitor)
        {
            using var service = new ServiceController(serviceName);
            try
            {
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running);
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Failed to manage service: {serviceName}, {ex.Message}");
            }
        }
    }

    private void EnsureMonitoredProcesses()
    {
        // For each process identifier (either executable name or full path), determine if it is running.
        foreach (var processIdentifier in _processesToMonitor)
        {
            // Determine the executable's base name (without extension) for process checking.
            var processName = System.IO.Path.GetFileNameWithoutExtension(processIdentifier);
            var runningProcesses = Process.GetProcessesByName(processName);

            // If no process is found, attempt to start it.
            if (runningProcesses.Length == 0)
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = processIdentifier,
                        UseShellExecute = true // Use the shell to start the process (helpful with non-console apps)
                    };

                    Process.Start(startInfo);
                    Console.WriteLine($"Started process: {processIdentifier}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to start process: {processIdentifier}. Exception: {ex.Message}");
                }
            }
        }
    }
}