write-host "Testing..." -ForegroundColor Yellow
pushd "./Source/FizzWare.NBuilder.Tests"
dotnet test -c Release
popd