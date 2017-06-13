function Get-BuildOutputPath(
    [Parameter(Mandatory)][string] $WorkingDirectory,
    [Parameter(Mandatory)][string] $Framework,
    [string] $OutputDirectory = (Join-Path $WorkingDirectory "nuget" -Resolve)    
    ) {
    Join-Path $OutputDirectory ($Framework -replace "\.", "")    
}