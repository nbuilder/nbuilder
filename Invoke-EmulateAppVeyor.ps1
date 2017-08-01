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

# build_script
$workingDirectory = $(pwd)
if ($Build) {
    . ./build/Write-BuildInfo.ps1
    . ./build/Before-Build.ps1
    . ./build/Build-Script.ps1
}

# before_test

# test
if ($Test) {
    . ./build/Test-Script.ps1
}

# package
if($Pack) {
    . ./build/New-NugetPackage.ps1 -Beta
}
