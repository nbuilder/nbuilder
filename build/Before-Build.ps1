
$version = ($env:APPVEYOR_BUILD_VERSION,"1.2.3" -ne $null)[0]

write-host "Building NBuilder $($version)" -ForegroundColor Yellow
dotnet clean
if ($LASTEXITCODE -ne 0 ) {
    throw "'dotnet clean' exited with code $LastExitCode."
}

dotnet restore
if ($LASTEXITCODE -ne 0 ) {
    throw "'dotnet restore' exited with code $LastExitCode."
}

