## taken from here: https://raw.githubusercontent.com/PureKrome/PushIt/master/NuGet%20Package%20and%20Publish.ps1

############################################################################
###                                                                      ###
###                    NUGET  PACKAGE and PUBLISH                        ###
###                                                                      ###
############################################################################



param (
  [string]$version,
  [string]$apiKey,
  [string]$source = $PSScriptRoot,
  [string]$destination = $PSScriptRoot,
  [string]$feedSource = "https://nuget.org",
  [string]$nuget,
  [switch]$clean,
  [switch]$help
)

function DisplayHelp()
{
    "Available command line arguments:"
    "    =>     version: Please use SemVer."
    "    =>      source: Directory location of the nuspec file(s). Default: current directory."
    "    => destination: Directory location where the nupkg files will be saved. Default: current directory."
    "    =>       nuget: Full path of where NuGet.exe is located (if not in PATH, etc). Default: none."
    "    =>  feedSource: Url of the feed to publish the package(s) to. Default: https://nuget.org"
    "    =>     api key: Feed secret api key. Default: none."
    "    =>       clean: Removes all nupkg files from the destination before running this script. Default: 0 (no cleaning)."
    ""
    ""
    "eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha"
    "eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha -destination C:\temp\TempNuGetPackages"
    "eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha -source ../nugetspecs/ -destination C:\temp\TempNuGetPackages"
    "eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha -nuget c:\temp\nuget.exe"
    "eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha -nuget c:\temp\nuget.exe -apiKey ABCD-EFG..."
    "eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha -nuget c:\temp\nuget.exe -feedSource https://www.myget.org/F/pushit/api/v2 -apiKey ABCD-EFG..."
    ""
}

function CleanUpInputArgs()
{
    $apiKey = $apiKey.Trim()
    $feedSource = $feedSource.Trim()
}

function DisplayCommandLineArgs()
{
    
    if ($apiKey)
    {
        if ($apiKey.length -gt 6)
        {
            $truncatedApiKey = "......" + $apiKey.substring($apiKey.length - 6)
        }
        else
        {
            for($i = 0; $i -lt $apiKey.length; $i++)
            {
                $truncatedApiKey += "*"
            }
        }
    }
    else
    {
        $truncatedApiKey = "<none provided>"
    }
     
    "Options provided:"
    "    =>     version: $version"
    "    =>      source: $source"
    "    => destination: $destination"
    "    =>       nuget: $nuget"
    "    =>  feedSource: $feedSource"
    "    =>     api key: $truncatedApiKey"
    "    =>       clean: $clean"
    ""

    if (-Not $version)
    {
        ""
        "**** The version of this NuGet package is required."
        "**** Eg. & '.\NuGet Package and Package.ps1' -version 0.1-alpha"
        ""
        ""
        throw;
    }

    if ($source -eq "")
    {
        ""
        "**** A source parameter provided cannot be an empty string."
        ""
        ""
        throw;
    }

    if ($destination -eq "")
    {
        ""
        "**** A destination parameter provided cannot be an empty string."
        ""
        ""
        throw;
    }

    if ($feedSource -eq "")
    {
        ""
        "**** The NuGet push source parameter provided cannot be an empty string."
        ""
        ""
        throw;
    }

    # Setup the nuget path.
    if (-Not $nuget -eq "")
    {
        $global:nugetExe = $nuget
    }
    else
    {
        # Assumption, nuget.exe is the current folder where this file is.
        $global:nugetExe = Join-Path $source "nuget" 
    }

    if (!(Test-Path $global:nugetExe -PathType leaf))
    {
        ""
        "**** Nuget file was not found. Please provide the -nuget parameter with the nuget.exe path -or- copy the nuget.exe to the current folder, side-by-side to this powershell file."
        ""
        ""
        throw;
    }
}


function CleanUp()
{
    if ($clean -eq $false)
    {
        return;
    }

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


function PackageTheSpecifications()
{
    ""
    "Getting all *.nuspec files to package in directory: $source"

    $files = Get-ChildItem $source -Filter *.nuspec

    if ($files.Count -eq 0)
    {
        ""
        "**** No nuspec files found in the directory: $source"
        "Terminating process."
        throw;
    }

    "Found: " + $files.Count + " files :)"

    foreach($file in $files)
    {
        &$nugetExe pack ($file.FullName) -Version $version -OutputDirectory $destination

        ""
    }
}


function PushThePackagesToNuGet()
{
    if ($apiKey -eq "")
    {
        "@@ No NuGet server api key provided - so not pushing anything up."
        return;
    }


    ""
    "Getting all *.nupkg's files to push to : $feedSource"

    $files = Get-ChildItem $destination -Filter *.nupkg

    if ($files.Count -eq 0)
    {
        ""
        "**** No nupkg files found in the directory: $destination"
        "Terminating process."
        throw;
    }

    "Found: " + $files.Count + " files :)"

    foreach($file in $files)
    {
        &$nugetExe push ($file.FullName) -Source $feedSource -apiKey $apiKey

        ""
    }
}

##############################################################################
##############################################################################

$ErrorActionPreference = "Stop"
$global:nugetExe = ""


""
" ---------------------- start script ----------------------"
""
""
"  Starting NuGet packing/publishing script"
""
"  This script will look for -all- *.nuspec files in a source directory,"
"  then paackage them up to *.nupack files. Finally, it can publish"
"  them to a NuGet server, if an api key was provided."
""
"  *** NEED HELP? use the -help argument."
"      eg.  & '.\NuGet Package and Package.ps1' -help"
""
""

if ($help)
{
    DisplayHelp;
}
else
{
    CleanUpInputArgs

    DisplayCommandLineArgs

    CleanUp

    PackageTheSpecifications

    PushThePackagesToNuGet
}

""
""
" ---------------------- end of script ----------------------"
""
""