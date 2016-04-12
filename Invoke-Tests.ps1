function Invoke-StandardTests($framework) {
  $testRunner = Join-Path $PsScriptRoot "Lib/Test/NUnit/nunit-console.exe"
  $directory = Join-Path $PsScriptRoot "tests/$framework"
  $testFiles = get-childitem 

  gci -Path $directory -Filter *NBuilder.Tests.dll | foreach {
    $testAssembly = $_.FullName
    $cmd = "$testRunner $testAssembly"
    write-host $cmd
    Invoke-Expression $cmd
  }
}

function Invoke-SilverlightTests($framework) {
  $testRunner = Join-Path $PsScriptRoot "Lib/Test/NUnit/nunit-console.exe"
  $directory = Join-Path $PsScriptRoot "tests/$framework"
  $testFiles = get-childitem 

  gci -Path $directory -Filter *.Tests.dll | foreach {
    $testAssembly = $_.FullName
    $cmd = "$testRunner $testAssembly"
    write-host $cmd
    Invoke-Expression $cmd
  }
}


Invoke-StandardTests "net35"
Invoke-StandardTests "net40"
Invoke-SilverlightTests "sl40"

