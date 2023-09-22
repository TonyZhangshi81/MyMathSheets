@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set TargetFileName=%~3
set ProjectDir=%~4
set TargetPath=%~5

echo ========== %ProjectName% (%ConfigurationName%) Build Complete ==========

if not exist %ProjectDir%..\..\..\..\Lib\StrategyModule md %ProjectDir%..\..\..\..\Lib\StrategyModule
echo %TargetFileName%
copy %TargetPath% %ProjectDir%..\..\..\..\Lib\StrategyModule /y
copy %ProjectDir%\Config\*.json %ProjectDir%..\..\..\..\MathSheets.Application\MathSheetsApp\Config /Y
copy %ProjectDir%\Config\*.json %ProjectDir%..\..\..\..\MathSheets.Application\MathSheetsService\Config /Y
copy %ProjectDir%\Config\*.json %ProjectDir%..\..\..\..\MathSheets.Presentation\MathSheets\Config /Y
copy %ProjectDir%\Config\*.json %ProjectDir%..\..\..\..\MathSheets.Presentation\TestConsoleApp\Config /Y
