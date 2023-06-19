
# Windows Service Monitor

This application monitors specified Windows services and restarts them if they have stopped. It's a .NET Core Worker Service that runs as a Windows service.

## Getting Started

1. First, clone this repository to your local machine.

2. Then navigate to the project directory.

3. Run `dotnet restore` to restore the project's dependencies.

4. Run `dotnet build` to build the project.

## Configuration

The service names to be monitored are specified in the `appsettings.json` file located in the same directory as the .NET Core application and .bat files. 

Here is an example of what the `appsettings.json` file should look like:

```json
{
  "ServicesToMonitor": [
    "Service1",
    "Service2"
  ],
  "ServiceName": "WindowsServiceMonitor"
}
```
Replace "Service1" and "Service2" with the names of the services you wish to monitor. The "ServiceName" is the name under which the monitoring service will run.

## Deployment

To deploy the application as a Windows service:

1. Navigate to the project directory.

2. Run `dotnet publish --runtime win-x64 --configuration Release --self-contained true` to publish the application.

3. Copy the published files to your desired directory.

4. Also, ensure the `appsettings.json`, `install.bat`, and `uninstall.bat` files are in the same directory.

5. Edit 'appsettings.json' to replace "Service1", "Service2", and "WindowsServiceMonitor" with the actual service names as per your requirements.  Run the `install.bat` file with administrator privileges to install and start the service.

6. If you need to uninstall the service, run the `uninstall.bat` file with administrator privileges.

## License

This project is licensed under the MIT License.