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
$env:PackageVersion = Get-NuGetPackageVersion -beta
Invoke-DisplayBuildInfo

# build_script
$workingDirectory = $(pwd)
if ($Build) {
    pushd "Source"
    dotnet build
    popd
}

# before_test

# test
if ($Test) {

    # $assemblies = @()
    # $Frameworks| %{
    #     $OutputPath = Get-BuildOutputPath -WorkingDirectory $workingDirectory -Framework $_
    #     write-host "Searching $OutputPath for Test assembly..." -ForegroundColor Cyan
    #     $assmeblies += Resolve-Path "$OutputPath\FizzWare.NBuilder.Tests.dll"
    # }

    # $assemblies | %{
    #     write-host "Running tests for $_"
    #     return $_;
    # } | Invoke-RunTests
}

# package

if($Pack) {
    Invoke-NugetPack -Version $env:PackageVersion -NuSpec "nuget\NBuilder.nuspec" -Destination "."
}
