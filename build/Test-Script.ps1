pushd "./Source/FizzWare.NBuilder.Tests"
write-host "dotnet test -c Release" -ForegroundColor Yellow
dotnet test -c Release
popd