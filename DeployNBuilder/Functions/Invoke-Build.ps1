function Invoke-Build(
    [Parameter(Mandatory, ValueFromPipeline)] [string] $SolutionTag,
    [Parameter(Mandatory)] [string] $WorkingDirectory,
    [string] $OutputDirectory = (Join-Path $WorkingDirectory "nuget" -Resolve),
    [string] $SourceDirectory = (Join-Path $WorkingDirectory "Source" -Resolve)
){
    Begin {
        $logger=""
        if ($env:AppVeyor) {
            $logger="/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
        } 
    }

    Process {
        $OutputPath = Join-Path $OutputDirectory ($SolutionTag -replace "\.", "")
        write-host "Invoke-Build -SolutionTag $SolutionTag -WorkingDirectory $WorkingDirectory -OutputDirectory $OutputDirectory -SourceDirectory $SourceDirectory" -ForegroundColor Blue

        $solution = Join-Path $SourceDirectory "NBuilder-$solutionTag.sln"
        $args = @($solution, "/t:ReBuild", $logger, "/m", "/verbosity:minimal", "/p:OutputPath=$OutputPath")
        write-host "Executing: msbuild $args" -ForegroundColor Yellow
        msbuild $args | Out-Host    
        if ($LASTEXITCODE -ne 0){
            write-error "Building $solutionTag failed: exit code $LASTEXITCODE"
        }

        # Give the system time to let go of file locks
        Start-Sleep -Seconds 2

    }

    End {
    }    
}

