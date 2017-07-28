function Invoke-RunTests(
    [Parameter(Mandatory, ValueFromPipeline)] 
    [ValidateNotNullOrEmpty()]
    [string] $Assembly
) {
    Begin {
        write-host "Invoke-RunTests -Assembly $Assembly" -ForegroundColor Blue

        $file = "nunit3-console.exe"
        $found = gci -filter "nunit3-console.exe" -Path "tools" -Recurse
        if(-not $found) {
            write-host "Executing: nuget install `"nunit.ConsoleRunner`" -o `"tools`" -ExcludeVersion" -ForegroundColor Yellow
            nuget install "nunit.ConsoleRunner" -o "tools" -ExcludeVersion
        }
        $found = gci -filter "nunit3-console.exe" -Path "tools" -Recurse

        $command = $found.FullName
    }

    Process {
        write-host "Executing: $($command) $assembly" -ForegroundColor Yellow
        & $command $assembly
    }
}
