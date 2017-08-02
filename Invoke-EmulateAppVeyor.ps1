param(
    [switch] $Build,
    [switch] $Test,
    [switch] $Pack
)

write-host "Emulating AppVeyor: Build: $Build, Test: $Test, Pack: $Pack" -ForegroundColor Green
    try {
    # build_script
    if ($Build) {
        . ./build/Write-BuildInfo.ps1
        . ./build/Before-Build.ps1
        . ./build/Build-Script.ps1
    }

    # test
    if ($Test) {
        . ./build/Test-Script.ps1
    }

    # package
    if($Pack) {
        . ./build/New-NugetPackage.ps1 -Beta
    }
} finally {
    #do nothing
}
