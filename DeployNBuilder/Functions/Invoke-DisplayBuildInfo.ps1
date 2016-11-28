function Invoke-DisplayBuildInfo() {
    $isPr = "No";
    if ($env:APPVEYOR_PULL_REQUEST_NUMBER)
    {
        $isPr = "Yes = PR #: " + $env:APPVEYOR_PULL_REQUEST_NUMBER
    }

    Write-Host "  *** AppVeyor configuration information ***" -ForegroundColor Yellow
    write-host "      Account Name: " + $env:APPVEYOR_ACCOUNT_NAME
    Write-Host "      Version: $env:APPVEYOR_BUILD_VERSION" -ForegroundColor Green
    write-host "      Git:"
    write-host @"
                        - Name:          $($env:APPVEYOR_REPO_NAME)
                        - Branch:        $($env:APPVEYOR_REPO_BRANCH)
                        - Commit:        $($env:APPVEYOR_REPO_COMMIT)
                        - Message:       $($env:APPVEYOR_REPO_COMMIT_MESSAGE)
                        - Author:        $($env:APPVEYOR_REPO_COMMIT_AUTHOR)
                        - TimeStamp:     $($env:APPVEYOR_REPO_COMMIT_TIMESTAMP)
                        - Pull Request?: $($IsPr)
"@ -ForegroundColor Gray
    write-host "      Platform: " + $env:PLATFORM
    write-host "      Configuration: " + $env:CONFIGURATION
    write-host "      Nuget:"
    write-host "            - Package Version: $($env:PackageVersion)" -ForegroundColor Gray
    write-host "  --------------------------------------------------------------------------"
    write-host ""
}
