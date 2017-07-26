param(
    [ValidateSet("NET3.5", "NET4.0", "NetStandard16")] [Array] $Frameworks,
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
    $Frameworks | Invoke-Build -WorkingDirectory $workingDirectory
}

# before_test

# test
if ($Test) {

    $assemblies = @()
    $Frameworks| %{
        $OutputPath = Get-BuildOutputPath -WorkingDirectory $workingDirectory -Framework $_
        $assemblies += Resolve-Path "$OutputPath\FizzWare.NBuilder.FunctionalTests.dll"
        $assmeblies += Resolve-Path "$OutputPath\FizzWare.NBuilder.Tests.dll"
    }

    $assemblies | Invoke-RunTests
}

# package

if($Pack) {
    Invoke-NugetPack -Version $env:PackageVersion -NuSpec "nuget\NBuilder.nuspec" -Destination "."
}
