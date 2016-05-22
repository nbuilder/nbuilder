if ($env:APPVEYOR_PULL_REQUEST_NUMBER) { 
    Write-Host "** This is a Pull Request, so we will not do any NuGet Package/Publishing."; 
} 
else { 
    & "AppVeyor/nuget-package-and-publish.ps1"  -Version $env:appveyor_build_version -NuGet "C:\Tools\NuGet\nuget.exe" -feedSource $env:feedSource -apiKey $env:apiKey -source '.\NuGet\' -destination '.\NuGet\'
}