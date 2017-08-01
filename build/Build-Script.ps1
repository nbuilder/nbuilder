
pushd Source

$version = ($env:APPVEYOR_BUILD_VERSION,"1.2.3.4" -ne $null)[0]

write-host "dotnet build -c Release /p:AssemblyVersion=$version /p:ProductVersion=$version" -ForegroundColor Yellow
dotnet build -c Release /p:AssemblyVersion=$version /p:ProductVersion=$version
popd           

$outputDirectory = $(pwd)

