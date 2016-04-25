New-Item -ItemType directory -Path nuget/net35
New-Item -ItemType directory -Path nuget/net40
New-Item -ItemType directory -Path nuget/sl40

msbuild Source/NBuilder-NET3.5.sln /target:clean
msbuild Source/NBuilder-NET3.5.sln /target:build
copy Source/FizzWare.NBuilder\bin\Debug\FizzWare.NBuilder.dll nuget/net35/Fizzware.NBuilder.dll

msbuild NBuilder-NET4.0.sln /target:clean
msbuild NBuilder-NET4.0.sln /target:build
copy Source/FizzWare.NBuilder\bin\Debug\FizzWare.NBuilder.dll nuget/net40/Fizzware.NBuilder.dll

msbuild Source/NBuilder-SL4.sln /target:clean
msbuild Source/NBuilder-SL4.sln /target:build
copy-item Source/FizzWare.NBuilder\bin\Debug\FizzWare.NBuilder.dll nuget/sl40/Fizzware.NBuilder.dll