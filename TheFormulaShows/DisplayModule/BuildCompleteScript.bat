@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set SolutionDir=%~3
set TargetFileName=%~4

echo ========== %ProjectName% (%ConfigurationName%) Build Complete ==========

if "%ConfigurationName%"=="Release" (

	if not exist "%SolutionDir%\Lib\DisplayModule" md "%SolutionDir%\Lib\DisplayModule"
	echo %TargetFileName%
	copy "%TargetFileName%" "%SolutionDir%\Lib\DisplayModule" /y
	copy "..\Scripts\Ext\*.js" "..\..\..\Scripts\Ext\" /Y
	xcopy "..\Content" "..\..\..\Content" /C /E /H /K /R /Y
	copy "..\Template\*.html" "%SolutionDir%TheFormulaShows\Template" /Y

)
