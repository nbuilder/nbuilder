function Invoke-StandardTests($framework) {
  $testRunner = Join-Path $PsScriptRoot "NUnit.ConsoleRunner/tools/nunit3-console.exe"
  $directory = Join-Path $PsScriptRoot "artifacts/$framework"
  $testFiles = get-childitem 

  gci -Path $directory -Filter *Tests.dll | foreach {
    $testAssembly = $_.FullName
    $cmd = "$testRunner $testAssembly"
    write-host $cmd
    Invoke-Expression $cmd
    
    if ($LASTEXITCODE -ne 0) {
      write-error "Test Failure"
      exit $LASTEXITCODE
    }
  }
}

function Invoke-SilverlightTests($framework) {
  $testRunner = Join-Path $PsScriptRoot "NUnitLite.SL50/tools/sl50/nunitlite-runner.exe"
  $directory = Join-Path $PsScriptRoot "artifacts/$framework"
  $testFiles = get-childitem 

  gci -Path $directory -Filter *Tests.dll | foreach {
    $testAssembly = $_.FullName
    $cmd = "$testRunner $testAssembly"
    write-host $cmd
    Invoke-Expression $cmd
    
    if ($LASTEXITCODE -ne 0) {
      write-error "Test Failure"
      exit $LASTEXITCODE
    }
  }
}


nuget install "NUnit.Runners" -ExcludeVersion
nuget install "NUnitLite.SL50" -ExcludeVersion

Invoke-StandardTests "net35"
Invoke-StandardTests "net40"
Invoke-SilverlightTests "sl4"

