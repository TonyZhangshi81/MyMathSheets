@echo off

set package-name=MyMathSheets.HealthChecks.1.0.0.nupkg
nuget push %package-name% -Source https://api.nuget.org/v3/index.json

pause
