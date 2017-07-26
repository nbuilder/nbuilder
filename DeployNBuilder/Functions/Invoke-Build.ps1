function Invoke-Build(
    [Parameter(Mandatory, ValueFromPipeline)] [string] $SolutionTag,
    [Parameter(Mandatory)] [string] $WorkingDirectory,
    [string] $SourceDirectory = (Join-Path $WorkingDirectory "Source" -Resolve)
){
    Begin {
        $logger=""
        if ($env:AppVeyor) {
            $logger="/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
        } 
    }

    Process {

        $OutputPath = Get-BuildOutputPath -WorkingDirectory $WorkingDirectory -Framework $SolutionTag
        write-host "Invoke-Build -SolutionTag $SolutionTag -WorkingDirectory $WorkingDirectory -OutputDirectory $OutputDirectory -SourceDirectory $SourceDirectory" -ForegroundColor Blue

        $Solution = Join-Path $SourceDirectory "NBuilder-$solutionTag.sln"
        nuget restore $Solution
        if ($_ -match "Standard") {
            dotnet restore $Solution
        }

        $args = @($solution, "/t:ReBuild", $logger, "/verbosity:minimal", "/p:OutputPath=$OutputPath")
        write-host "Executing: msbuild $args" -ForegroundColor Yellow
        msbuild $args | Out-Host    
        if ($LASTEXITCODE -ne 0){
            write-error "Building $Solution failed: exit code $LASTEXITCODE"
        }

        # Give the system time to let go of file locks
        Start-Sleep -Seconds 5

        return $OutputPath
    }

    End {
    }    
}

