@echo off

set ConfigurationName=%~1
set SolutionDir=%~2
set TargetFileName=%~3

echo ========== MyMathSheets.BasicOperationsLib (%ConfigurationName%) Build Complete ==========

if "%ConfigurationName%"=="Debug" (

	echo "%TargetFileName%"
	copy "%TargetFileName%" "%SolutionDir%Lib" /y

)