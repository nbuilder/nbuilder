pushd "./tests/FizzWare.NBuilder.Tests"
try {
    write-host "dotnet test -c Release" -ForegroundColor Yellow
    dotnet test -c Release
    if ($LastExitCode -ne 0) {
        throw "'dotnet test' exited with code $LastExitCode"
    }
} finally {
    popd
}
