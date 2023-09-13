@echo off

echo ("input nupkg file name:")

set /p package-name=
if %package-name% neq "" goto END

nuget push %package-name% -Source https://api.nuget.org/v3/index.json

:END

Pause
exit 

