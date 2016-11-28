function Invoke-NugetPack(
    [Parameter(Mandatory)] [string]$version,
    [Parameter(Mandatory)] [string]$nuspec,
    [Parameter(Mandatory)] [string]$destination,
    [switch]$clean
) {

    function CleanUp()
    {
        $nupkgFiles = @(Get-ChildItem $destination -Filter *.nupkg)

        if ($nupkgFiles.Count -gt 0)
        {
            "Found " + $nupkgFiles.Count + " *.nupkg files. Lets delete these first..."

            foreach($nupkgFile in $nupkgFiles)
            {
                $combined = Join-Path $destination $nupkgFile
                "... Removing $combined."
                Remove-Item $combined
            }
            
            "... Done!"
        }
    }

    if ($clean) {
        CleanUp
    }

    nuget pack $nuspec -Version $version -OutputDirectory $destination
}