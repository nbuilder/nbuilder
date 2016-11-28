$DeployNBuilderModuleDir = $PsScriptRoot
write-host "PsModulePath should contain $DeployNBuilderModuleDir"
[string[]] $modulePaths =   [Environment]::GetEnvironmentVariable("PsModulePath", "User") -split ";"

if (-not ($modulePaths -contains $DeployNBuilderModuleDir)) {
    $modulePaths += $DeployNBuilderModuleDir
    $newPath = $modulePaths -join ";"
    write-host "Setting PsModulePath to $newPath for future sessions"
    [Environment]::SetEnvironmentVariable("PsModulePath", $newPath, "User")
}

[string[]] $modulePaths = $env:PSModulePath -split ';'
if (-not ($modulePaths -contains $DeployNBuilderModuleDir)) {
    $modulePaths += $DeployNBuilderModuleDir
    $newPath = $modulePaths -join ";"
    write-host "Setting PsModulePath to $newPath for current session"
    $env:PSModulePath = $newPath
}


