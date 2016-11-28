# init 
Import-Module "$PsScriptRoot\DeployNBuilder\DeployNBuilder.psd1" -Force

# before_build
gci -Exclude *.nuspec -Path "nuget" -recurse | Remove-Item -recurse
$env:PackageVersion = Get-NuGetPackageVersion -beta
Invoke-DisplayBuildInfo
nuget restore Source\NBuilder-NET4.0.sln
nuget restore Source\NBuilder-NET3.5.sln

# build_script
$workingDirectory = $(pwd)
"NET3.5", "NET4.0" | Invoke-Build -WorkingDirectory $workingDirectory

# before_test

# test
"nuget\NET35\FizzWare.NBuilder.FunctionalTests.dll", `
"nuget\NET35\FizzWare.NBuilder.Tests.dll", `
"nuget\NET40\FizzWare.NBuilder.FunctionalTests.dll", `
"nuget\NET40\FizzWare.NBuilder.Tests.dll" | Invoke-RunTests


# package

Invoke-NugetPack -Version $env:PackageVersion -NuSpec "nuget\NBuilder.nuspec" -Destination "."
