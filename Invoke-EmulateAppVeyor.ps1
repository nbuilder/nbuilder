param(
    [switch] $SkipBuild,
    [switch] $SkipTests,
    [switch] $SkipNuget,
    [ValidateSet("NET3.5", "NET4.0", "NetStandard16")] [Array]$Frameworks
)

write-host "Emulating AppVeyor: Build: $(-not $SkipBuild), Test: $(-not $SkipTests), NuGet: $(-not $SkipNuget)" -ForegroundColor Green

# init 
Import-Module "$PsScriptRoot\DeployNBuilder\DeployNBuilder.psd1" -Force

# before_build
gci -Exclude *.nuspec -Path "nuget" -recurse | Remove-Item -recurse
$env:PackageVersion = Get-NuGetPackageVersion -beta
Invoke-DisplayBuildInfo

# build_script
$workingDirectory = $(pwd)
if (-not $SkipBuild) {
    $Frameworks | foreach {
        $Solution = "Source\NBuilder-$_.sln"
        nuget restore $Solution
        if ($_ -match "Standard") {
            dotnet restore $Solution
        }
    }
    $Frameworks | Invoke-Build -WorkingDirectory $workingDirectory
}

# before_test

# test
if (-not $SkipTests) {

    $assemblies = @()
    $Frameworks| %{
        $OutputPath = Get-BuildOutputPath -WorkingDirectory $workingDirectory -Framework $_
        $assemblies += Resolve-Path "$OutputPath\FizzWare.NBuilder.FunctionalTests.dll"
        $assmeblies += Resolve-Path "$OutputPath\FizzWare.NBuilder.Tests.dll"
    }

    $assemblies | Invoke-RunTests
}


# package

if(-not $SkipNuGet) {
    Invoke-NugetPack -Version $env:PackageVersion -NuSpec "nuget\NBuilder.nuspec" -Destination "."
}
