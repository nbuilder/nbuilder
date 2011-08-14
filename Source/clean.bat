FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"

FOR /F "tokens=*" %%G IN ('DIR /B /S *.xap') DO del /S /Q "%%G"

RMDIR /S /Q DeploymentScripts\working