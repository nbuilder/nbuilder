$logger=""
$solutionRoot = (new-object System.IO.FileInfo $PSScriptRoot).Directory.FullName
$outputPath   = Join-Path $solutionRoot "artifacts"

if ($env:AppVeyor) {
    $logger="/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
    $outputPath = $env:APPVEYOR_BUILD_FOLDER + "\nbuilder_output"
}

function Invoke-Build($solutionTag){
    $build_dir = ($solutionTag -replace "\.", "")

    $solution = Join-Path $solutionRoot "Source\NBuilder-$solutionTag.sln"
    $args = @($solution /t:ReBuild $logger "/m" "/verbosity:minimal" "/p:OutputPath=$outputPath\$build_dir")
    & msbuild $args | Out-Host    
    if ($LASTEXITCODE -ne 0){
        write-error "Building $solutionTag failed."
        exit $LASTEXITCODE
    }
    
    return $LASTEXITCODE
}

Invoke-Build "NET3.5"
invoke-build "NET4.0"
Invoke-Build "SL4"