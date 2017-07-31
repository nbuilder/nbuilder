function Get-BuildOutputPath(
    [Parameter(Mandatory)][string] $WorkingDirectory,
    [Parameter(Mandatory)][string] $Framework,
    [string] $OutputDirectory = (Join-Path $WorkingDirectory "nuget")    
    ) {

    if (-not (Test-Path $OutputDirectory)) {
        New-Item -ItemType Directory -Path $WorkingDirectory -Value (Split-Path $OutputDirectory -Leaf) -Force | Out-Null
    }

    Join-Path $OutputDirectory ($Framework -replace "\.", "")    
}