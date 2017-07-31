param(
    [switch] $Build,
    [switch] $Test,
    [switch] $Pack
)

write-host "Emulating AppVeyor: Build: $Build, Test: $Test, Pack: $Pack" -ForegroundColor Green

# init 
Import-Module "$PsScriptRoot\DeployNBuilder\DeployNBuilder.psd1" -Force

# before_build
gci -Exclude *.nuspec -Path "nuget" -recurse | Remove-Item -recurse
$env:AssemblyVersion = Get-NuGetPackageVersion
$env:PackageVersion = Get-NuGetPackageVersion -beta
Invoke-DisplayBuildInfo

# build_script
$workingDirectory = $(pwd)
if ($Build) {
    pushd "Source"
    write-host "Building NBuilder $($env:AssemblyVersion)" -ForegroundColor Yellow
    dotnet clean
    dotnet restore
    dotnet build -c Release /p:AssemblyVersion=$env:AssemblyVersion /p:ProductVersion=$env:AssemblyVersion
    popd
}

# before_test

# test
if ($Test) {
    write-host "Testing..." -ForegroundColor Yellow
    pushd "./Source/FizzWare.NBuilder.Tests"
    dotnet test -c Release
    popd
}

# package
if($Pack) {
    write-host "Packaging..." -ForegroundColor Yellow
    pushd "./Source\FizzWare.NBuilder"
    dotnet pack -c Release /p:PackageVersion=$env:PackageVersion FizzWare.NBuilder.csproj -o "$PsScriptRoot"
    popd
}
