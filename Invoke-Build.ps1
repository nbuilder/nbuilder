function Assert-DirectoryExists($path){
  if (!(Test-Path $path)){
    New-Item -ItemType directory -Path $path
  }  
}

Assert-DirectoryExists "nuget\net35"
Assert-DirectoryExists "tests\net35"
msbuild Source/NBuilder-NET3.5.sln /target:clean
msbuild Source/NBuilder-NET3.5.sln /target:build
copy Source/FizzWare.NBuilder\bin\Debug\FizzWare.NBuilder.dll nuget/net35/Fizzware.NBuilder.dll
copy Source/FizzWare.NBuilder.Tests\bin\Debug\*.* tests\net35



Assert-DirectoryExists "nuget\net40"
Assert-DirectoryExists "tests\net40"
msbuild NBuilder-NET4.0.sln /target:clean
msbuild NBuilder-NET4.0.sln /target:build
copy Source/FizzWare.NBuilder\bin\Debug\FizzWare.NBuilder.dll nuget/net40/Fizzware.NBuilder.dll
copy Source/FizzWare.NBuilder.Tests\bin\Debug\*.* tests\net40

Assert-DirectoryExists "nuget\sl40"
Assert-DirectoryExists "tests\sl40"

msbuild Source/NBuilder-SL4.sln /target:clean
msbuild Source/NBuilder-SL4.sln /target:build
copy-item Source/FizzWare.NBuilder\bin\Debug\FizzWare.NBuilder.dll nuget/sl40/Fizzware.NBuilder.dll
copy Source/FizzWare.NBuilder.Tests\bin\Debug\*.* tests\sl40
