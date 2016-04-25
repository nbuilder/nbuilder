params(
    [Parameter(Mandatory=$true)] $version
)

pushd nuget
nuget pack NBuilder.nuspec -version $version
popd