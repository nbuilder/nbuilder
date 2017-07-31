pushd Source
write-host "Building NBuilder $($env:AssemblyVersion)" -ForegroundColor Yellow
dotnet build -c Release /p:AssemblyVersion=$env:AssemblyVersion /p:ProductVersion=$env:AssemblyVersion
popd           

$outputDirectory = $(pwd)

write-host "Creating nuget package version $($env:PackageVersion)" -ForegroundColor Yellow 
pushd ".\Source\FizzWare.NBuilder"
dotnet pack -c Release /p:PackageVersion=$env:PackageVersion FizzWare.NBuilder.csproj -o "$outputDirectory"
popd
