pushd Source

$version = ($env:APPVEYOR_BUILD_VERSION,"1.2.3" -ne $null)[0]

write-host "Building NBuilder $($version)" -ForegroundColor Yellow
dotnet clean
dotnet restore
popd
