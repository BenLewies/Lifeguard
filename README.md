# Lifeguard

This solution contains a .NET 6 console application `Lifeguard` that monitors specified Windows services and processes and restarts them if they stop unexpectedly. It also contains two additional console applications, `InstallApp` and `UninstallApp`, to install and uninstall Windows services.

## Project Structure

- **Lifeguard** : This is the main application that monitors the specified Windows services and restarts them if they stop. The list of services to monitor is read from the `appsettings.json` file at startup.
- **InstallApp** : This application prompts the user for a service name and attempts to install and start a Windows service with that name.
- **UninstallApp** : This application prompts the user for a service name and attempts to uninstall a Windows service with that name.

All projects are built into a single output directory, `Net6Publish`.

## Setup and Run

Before beginning, please ensure that the .Net 6 runtime is installed on the target machine. You can download it from [here](https://dotnet.microsoft.com/download/dotnet/6.0).

1. **Build the solution**: Open the solution in Visual Studio, and build it by clicking `Build -> Build Solution`. All projects are built into the `Net6Publish` directory at the solution root.

2. **Run the Lifeguard**: Open a command prompt or terminal, navigate to the `Net6Publish` directory, and run `dotnet Lifeguard.dll`.

3. **Install a service**: Run the `InstallApp` by executing `dotnet Install.exe` in your command prompt or terminal. It will prompt for the service name. Enter the name of the Windows service to install and press enter (you can give this any name you like).

4. **Uninstall a service**: Run the `UninstallApp` by executing `dotnet Uninstall.exe` in your command prompt or terminal. It will prompt for the service name. Enter the name of the Windows service to uninstall and press enter.

Remember that to install and uninstall services, you need administrator privileges.

Hint: To run multiple instances of the `Lifeguard`, you can copy the `Net6Publish` directory to another location and run the `Lifeguard` from there.  Remember to give the service a unique name when running `dotnet Install.exe`.

## appsettings.json

The `appsettings.json` file, used by the `Lifeguard`, should be in the same directory as the `Lifeguard.dll` file. It contains an array of strings with the names of the services and processes to monitor. Here's an example:

```json
{
  "ServicesToMonitor": ["Service1","Service2","Service3"],
  "ProcessesToMonitor": ["C:\\Program Files\\Docker\\Docker\\Docker Desktop.exe"],
  "RefreshIntervalInSeconds": 30
}

```

## Contributions

Contributions, issues, and feature requests are welcome!

## License

This project is [MIT](LICENSE) licensed.

