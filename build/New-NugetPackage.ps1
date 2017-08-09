param(
    [switch] $Beta
)

if ($env:APPVEYOR_BUILD_VERSION) {
    $version = new-object System.Version $env:APPVEYOR_BUILD_VERSION
} else {
    $version = new-object System.Version "1.2.3.4"
}


if (-not $beta) {
    $packageVersion = "{0}.{1}.{2}" -f $version.Major, $version.Minor, $version.Build
} else {
    $packageVersion = "{0}.{1}.{2}-beta-{3}" -f $version.Major, $version.Minor, $version.Build, $version.Revision
}
$outputDirectory = $(pwd)
try {
    pushd "./src/FizzWare.NBuilder"
    write-host "dotnet pack -c Release /p:PackageVersion=$($packageVersion) FizzWare.NBuilder.csproj -o '$outputDirectory' " -ForegroundColor Yellow
    dotnet pack -c Release /p:PackageVersion=$($packageVersion) FizzWare.NBuilder.csproj -o "$outputDirectory"
    if ($LASTEXITCODE -ne 0) {
        throw "'dotnet pack' exited with code $LastExitCode"
    }
} finally {
    popd
}
