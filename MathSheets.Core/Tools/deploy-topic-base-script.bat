@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set TargetFileName=%~3
set TargetPath=%~4
set ProjectDir=%~5

echo ========== %ProjectName% (%ConfigurationName%) Build Complete ==========

echo %TargetFileName%
copy %TargetPath% %ProjectDir%..\..\Lib /y
