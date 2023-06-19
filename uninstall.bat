@echo off
for /f "delims=" %%x in ('powershell -Command "Get-Content '%~dp0\appsettings.json' | ConvertFrom-Json | % ServiceName"') do set "SERVICENAME=%%x"

sc query "%SERVICENAME%" > nul
if ERRORLEVEL 1 (
    echo The service does not exist.
) else (
    echo The service exists, uninstalling...
    sc stop "%SERVICENAME%"
    sc delete "%SERVICENAME%"
    echo Uninstallation complete.
    timeout /t 5
)
