@echo off

set project-name=MyMathSheets.WebHealthChecks.csproj
nuget pack %project-name% -Prop Configuration=Debug -IncludeReferencedProjects

pause
