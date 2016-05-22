$logger=""
$outputPath = Join-Path (new-object System.IO.FileInfo $PSScriptRoot).Directory.FullName "artifacts"

if ($env:AppVeyor) {
    $logger="/logger:C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll"
    $outputPath = $env:APPVEYOR_BUILD_FOLDER + "\nbuilder_output"
}

function Invoke-Build($solutionTag){
    $build_dir = ($solutionTag -replace "\.", "")
    $cmd = "msbuild Source\NBuilder-$solutionTag.sln $logger /m /verbosity:minimal /p:OutputPath=$outputPath\$build_dir"
    write-host $cmd
    iex $cmd
}

Invoke-Build "NET3.5"
invoke-build "NET4.0"
Invoke-Build "SL4"