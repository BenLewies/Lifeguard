using System;
using System.Diagnostics;

namespace UninstallApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Please enter the name of the service to uninstall: ");
                string serviceName = Console.ReadLine();

                Console.WriteLine("Attempting to uninstall the service...");

                using var process = new Process();
                process.StartInfo.FileName = "sc";
                process.StartInfo.Arguments = $"delete {serviceName}";
                process.StartInfo.UseShellExecute = false;
                process.Start();

                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Failed to uninstall the service. Please make sure the service exists and you have the necessary permissions, then try again.");
                    Environment.Exit(1);
                }

                Console.WriteLine("Service uninstalled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
