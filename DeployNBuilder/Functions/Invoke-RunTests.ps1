function Invoke-RunTests(
    [Parameter(Mandatory, ValueFromPipeline)] [string] $assembly
) {
    Begin {
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
