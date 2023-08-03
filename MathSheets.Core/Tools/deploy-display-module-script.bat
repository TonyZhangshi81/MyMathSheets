@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set TargetFileName=%~3
set ProjectDir=%~4
set TargetPath=%~5

echo ========== %ProjectName% (%ConfigurationName%) Build Complete ==========

if not exist %ProjectDir%..\..\..\..\Lib\DisplayModule md %ProjectDir%..\..\..\..\Lib\DisplayModule
echo %TargetFileName%
copy %TargetPath% %ProjectDir%..\..\..\..\Lib\DisplayModule /y
copy %ProjectDir%\Scripts\Ext\*.js %ProjectDir%..\..\..\..\MathSheets.Business\TheFormulaShows\Scripts\Ext\ /Y
xcopy %ProjectDir%\Content %ProjectDir%..\..\..\..\MathSheets.Business\TheFormulaShows\Content /C /E /H /K /R /Y
copy %ProjectDir%\Template\*.html %ProjectDir%..\..\..\..\MathSheets.Business\TheFormulaShows\Template /Y
