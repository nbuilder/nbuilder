pushd Source
write-host "Building NBuilder $($env:AssemblyVersion)" -ForegroundColor Yellow
dotnet clean
dotnet restore
popd
