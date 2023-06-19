Here is the updated `README.md`:

---

# Windows Service Monitor

This solution contains a .NET 6 console application `WindowsServiceMonitor` that monitors specified Windows services and restarts them if they stop unexpectedly. It also contains two additional console applications, `InstallApp` and `UninstallApp`, to install and uninstall Windows services.

## Project Structure

- **WindowsServiceMonitor** : This is the main application that monitors the specified Windows services and restarts them if they stop. The list of services to monitor is read from the `appsettings.json` file at startup.
- **InstallApp** : This application prompts the user for a service name and attempts to install and start a Windows service with that name.
- **UninstallApp** : This application prompts the user for a service name and attempts to uninstall a Windows service with that name.

All projects are built into a single output directory, `out`.

## Setup and Run

1. **Build the solution**: Open the solution in Visual Studio, and build it by clicking `Build -> Build Solution`. All projects are built into the `out` directory at the solution root.

2. **Run the WindowsServiceMonitor**: Open a command prompt or terminal, navigate to the `out` directory, and run `dotnet WindowsServiceMonitor.dll`.

3. **Install a service**: Run the `InstallApp` by executing `dotnet Install.exe` in your command prompt or terminal. It will prompt for the service name. Enter the name of the Windows service to install and press enter.

4. **Uninstall a service**: Run the `UninstallApp` by executing `dotnet Uninstall.exe` in your command prompt or terminal. It will prompt for the service name. Enter the name of the Windows service to uninstall and press enter.

Remember that to install and uninstall services, you need administrator privileges.

## appsettings.json

The `appsettings.json` file, used by the `WindowsServiceMonitor`, should be in the same directory as the `WindowsServiceMonitor.dll` file. It contains an array of strings with the names of the services to monitor. Here's an example:

```json
{
    "Services": [
        "Service1",
        "Service2",
        "Service3"
    ]
}
```

Replace `"Service1"`, `"Service2"`, and `"Service3"` with the names of the services you want to monitor.

## Contributions

Contributions, issues, and feature requests are welcome!

## License

This project is [MIT](LICENSE) licensed.

---

Please replace `"LICENSE"` in the last line with the path to your actual license file, if you have one.
