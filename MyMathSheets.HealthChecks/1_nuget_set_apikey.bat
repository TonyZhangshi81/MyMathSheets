@echo off

echo ("nuget setApiKey <apikey>")

set /p api_key=
if %api_key% neq "" goto END

nuget setApiKey %api_key%

:END

Pause
exit 

