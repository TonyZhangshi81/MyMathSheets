@echo off

set ConfigurationName=%~1
set SolutionDir=%~2
set TargetFileName=%~3

echo ========== MyMathSheets.CommonLib (%ConfigurationName%) Build Complete ==========

if "%ConfigurationName%"=="Release" (

	echo "%TargetFileName%"
	copy "%TargetFileName%" "%SolutionDir%Lib" /y

)