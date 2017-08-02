function Get-NuGetPackageVersion(
    [switch] $beta
) {
    if ($env:APPVEYOR_BUILD_VERSION) {
        $version = new-object System.Version $env:APPVEYOR_BUILD_VERSION
    } else {
        $version = new-object System.Version "1.2.3.4"
    }

    if (-not $beta) {
        return "{0}.{1}.{2}" -f $version.Major, $version.Minor, $version.Build
    } else {
        return "{0}.{1}.{2}-beta-{3}" -f $version.Major, $version.Minor, $version.Build, $version.Revision
    }
}

