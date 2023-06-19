echo "Building and publishing projects..."

# Read the config file
$config = Get-Content -Path "build.json" | ConvertFrom-Json

# Clean the projects
& "$($config.DotnetPath)\dotnet.exe" clean
if ($LASTEXITCODE -ne 0) {
    Write-Error "An error occurred while cleaning the projects."
    exit
}

# Build the projects
& "$($config.DotnetPath)\dotnet.exe" build --configuration Release
if ($LASTEXITCODE -ne 0) {
    Write-Error "An error occurred while building the projects."
    exit
}

# Publish the projects
foreach ($project in $config.Projects) {
    & "$($config.DotnetPath)\dotnet.exe" publish --configuration Release --output "$($config.DestinationPath)\$($project.Name)" --runtime win-x64 --self-contained true $project.ProjectFile
    if ($LASTEXITCODE -ne 0) {
        Write-Error "An error occurred while publishing the $($project.Name) project."
        exit
    }
}

echo "All projects built and published successfully."
pause
