$version = new-object System.Version $env:APPVEYOR_BUILD_VERSION
$env:PackageVersion = "{0}.{1}.{2}-beta-{3}" -f $version.Major, $version.Minor, $version.Build, $version.Revision
