@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set SolutionDir=%~3
set TargetDir=%~4

echo ========== %ProjectName% (%ConfigurationName%) Build PreComplete ==========

if "%ConfigurationName%"=="Release" (

	copy "%SolutionDir%Lib\DisplayModule\*.dll" "%TargetDir%" /y
	copy "%SolutionDir%Lib\StrategyModule\*.dll" "%TargetDir%" /y
)
