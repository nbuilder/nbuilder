gci -Path "$($PsScriptRoot)\Functions" -Filter "*.ps1" | %{
    . $_.FullName
}

Export-ModuleMember -Function Get-BuildOutputPath
Export-ModuleMember -Function Get-NuGetPackageVersion
Export-ModuleMember -Function Invoke-DisplayBuildInfo
Export-ModuleMember -Function Invoke-Build
Export-ModuleMember -Function Invoke-NuGetPack
Export-ModuleMember -Function Invoke-RunTests
