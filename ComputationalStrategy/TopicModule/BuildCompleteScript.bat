@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set SolutionDir=%~3
set TargetFileName=%~4

echo ========== %ProjectName% (%ConfigurationName%) Build Complete ==========

if "%ConfigurationName%"=="Release" (

	if not exist "%SolutionDir%\Lib\StrategyModule" md "%SolutionDir%\Lib\StrategyModule"
	echo %TargetFileName%
	copy "%TargetFileName%" "%SolutionDir%\Lib\StrategyModule" /y
	copy "..\Config\*.json" "%SolutionDir%MathSheetsSettingApp\Config" /Y
	copy "..\Config\*.json" "%SolutionDir%MathSheetsSettingWeb\Config" /Y
	copy "..\Config\*.json" "%SolutionDir%MathSheetsSettingWebApi\Config" /Y
	copy "..\Config\*.json" "%SolutionDir%TestConsoleApp\Config" /Y

)