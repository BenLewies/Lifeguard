using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()  //This is to make the application run as a windows service
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            });
}

public class Worker : BackgroundService
{
    private readonly string[] _servicesToMonitor;

    public Worker(IHostEnvironment env)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        _servicesToMonitor = config.GetSection("ServicesToMonitor").Get<string[]>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
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

            await Task.Delay(1000 * 60, stoppingToken);  //check every minute
        }
    }
}
