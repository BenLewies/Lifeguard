using System;
using System.Diagnostics;
using System.IO;

namespace InstallApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Please enter the name of the service to install: ");
                string serviceName = Console.ReadLine();

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string serviceExe = Path.Combine(path, "Lifeguard.exe");

                // Wrap executable path and serviceName in quotes to handle spaces.
                string quotedServiceExe = $"\"{serviceExe}\"";
                string quotedServiceName = $"\"{serviceName}\"";

                Console.WriteLine("Attempting to install the service...");
                Console.WriteLine($@"... Resolved monitoring service exe to {serviceExe}");

                using var process = new Process();
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = $"create {quotedServiceName} binPath= {quotedServiceExe} start= auto";
                process.StartInfo.UseShellExecute = false;
                process.Start();

                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Failed to install the service. Please make sure you have the necessary permissions and try again.");
                    Environment.Exit(1);
                }

                Console.WriteLine("Service installed successfully. Starting the service...");

                process.StartInfo.Arguments = $"start {quotedServiceName}";
                process.Start();

                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Failed to start the service. Please check the service configuration and try again.");
                    Environment.Exit(1);
                }

                Console.WriteLine("Service started successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
