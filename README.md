Sure, here's the updated `README.md`:

---

# Windows Service Monitor

This solution contains a .NET 6 console application `WindowsServiceMonitor` that monitors specified Windows services and restarts them if they stop unexpectedly. It also contains two additional console applications, `InstallApp` and `UninstallApp`, to install and uninstall Windows services.

## Project Structure

- **WindowsServiceMonitor** : This is the main application that monitors the specified Windows services and restarts them if they stop. The list of services to monitor is read from the `appsettings.json` file at startup.
- **InstallApp** : This application prompts the user for a service name and attempts to install and start a Windows service with that name.
- **UninstallApp** : This application prompts the user for a service name and attempts to uninstall a Windows service with that name.

## Setup and Run

1. **Update build.config**: Update the `build.config` file with the paths to your .NET installation, the output directory for the published applications, and the paths to your project files.

2. **Build and Publish**: Open a PowerShell window with administrator privileges, navigate to the directory containing the `build-publish.ps1` script, and run the script by typing `.\build-publish.ps1` and pressing Enter.  Note: PowerShell execution policy might prevent the script from running. If that's the case, you can change the execution policy by running Set-ExecutionPolicy Unrestricted in an elevated PowerShell prompt. Be sure to read about PowerShell execution policies and understand the implications before changing them.

3. **Run the WindowsServiceMonitor**: Navigate to the directory where the `WindowsServiceMonitor` was published, and run `WindowsServiceMonitor.exe`.

4. **Install a service**: Navigate to the directory where the `InstallApp` was published, and run `InstallApp.exe`. It will prompt for the service name. Enter the name of the Windows service to install and press enter.

5. **Uninstall a service**: Navigate to the directory where the `UninstallApp` was published, and run `UninstallApp.exe`. It will prompt for the service name. Enter the name of the Windows service to uninstall and press enter.

## appsettings.json

The `appsettings.json` file, used by the `WindowsServiceMonitor`, should be in the same directory as the `WindowsServiceMonitor.exe` file. It contains an array of strings with the names of the services to monitor. Here's an example:

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

## build.config

The `build.config` file should be in the same directory as the `build-publish.ps1` script. It contains the paths to your .NET installation, the output directory for the published applications, and the paths to your project files. Here's an example:

```json
{
    "DestinationPath": "C:\\published_apps",
    "DotnetPath": "C:\\Program Files\\dotnet",
    "Projects": [
        {
            "Name": "WindowsServiceMonitor",
            "ProjectFile": "WindowsServiceMonitor\\WindowsServiceMonitor.csproj"
        },
        {
            "Name": "InstallApp",
            "ProjectFile": "InstallApp\\InstallApp.csproj"
        },
        {
            "Name": "UninstallApp",
            "ProjectFile": "UninstallApp\\UninstallApp.csproj"
        }
    ]
}
```

Replace the paths and project files with your actual paths and project files.

## Contributions

Contributions, issues, and feature requests are welcome!

## License

This project is [MIT](LICENSE) licensed.

---
You can replace `"LICENSE"` in the last line with the path to your actual license file, if you have one.