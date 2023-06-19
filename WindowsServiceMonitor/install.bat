@echo off
for /f "delims=" %%x in ('powershell -Command "Get-Content '%~dp0\appsettings.json' | ConvertFrom-Json | % ServiceName"') do set "SERVICENAME=%%x"
SET SERVICEEXE=%~dp0\WindowsServiceMonitor.exe

sc query "%SERVICENAME%" > nul
if ERRORLEVEL 1 (
    echo The service does not exist, installing...
) else (
    echo The service exists, uninstalling first...
    sc stop "%SERVICENAME%"
    sc delete "%SERVICENAME%"
    echo Uninstallation complete.
    timeout /t 5
)

echo Installing service...
sc create "%SERVICENAME%" binPath= "%SERVICEEXE%" start= auto
if ERRORLEVEL 1 (
    echo Failed to install service.
    exit /b 1
)
echo Installation complete.

echo Starting service...
sc start "%SERVICENAME%"
if ERRORLEVEL 1 (
    echo Failed to start service.
    exit /b 1
)
echo Service started successfully.
