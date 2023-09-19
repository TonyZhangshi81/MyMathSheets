@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set TargetDir=%~3
set ProjectDir=%~4

echo ========== %ProjectName% (%ConfigurationName%) Build PreComplete ==========

copy %ProjectDir%..\..\Lib\DisplayModule\*.dll %TargetDir% /y
copy %ProjectDir%..\..\Lib\StrategyModule\*.dll %TargetDir% /y
