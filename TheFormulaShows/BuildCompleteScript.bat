@echo off

set ConfigurationName=%~1
set SolutionDir=%~2

echo ========== MyMathSheets.TheFormulaShows (%ConfigurationName%) Build Complete ==========

if "%ConfigurationName%"=="Debug" (

	echo ========== MathSheetsSettingApp Setting ... ==========

	echo ..\Template\HTMLPage
	copy "..\Template\HTMLPage" "%SolutionDir%MathSheetsSettingApp\HtmlTemplate" /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\Ext" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\Ext"
	xcopy "..\Scripts\Ext" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\Ext" /C /E /H /K /R /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Plugin" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Plugin"
	xcopy "..\Plugin" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Plugin" /C /E /H /K /R /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\fonts" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\fonts"
	xcopy "..\Content\fonts" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\fonts" /C /E /H /K /R /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\css" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\css"
	xcopy "..\Content\css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\css" /C /E /H /K /R /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\icons" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\icons"
	xcopy "..\Content\icons" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\icons" /C /E /H /K /R /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Page" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Page"

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts"
	echo ..\Scripts\bootstrap.min.js
	copy "..\Scripts\bootstrap.min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\bootstrap.min.js.map
	copy "..\Scripts\bootstrap.min.js.map" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery-ui.js
	copy "..\Scripts\jquery-ui.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery-3.3.1.min.js
	copy "..\Scripts\jquery-3.3.1.min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery.base64.js
	copy "..\Scripts\jquery.base64.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery.PrintArea.js
	copy "..\Scripts\jquery.PrintArea.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\json2.min.js
	copy "..\Scripts\json2.min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery.easing.1.3.js
	copy "..\Scripts\jquery.easing.1.3.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\store.legacy.min.js
	copy "..\Scripts\store.legacy.min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery.linq.min.js
	copy "..\Scripts\jquery.linq.min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y
	echo ..\Scripts\jquery.linq.min.js
	copy "..\Scripts\jquery.linq.min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts" /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\umd" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\umd"
	echo ..\Scripts\umd\popper.js
	copy "..\Scripts\umd\popper.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\umd" /Y
	echo ..\Scripts\umd\popper.js.map
	copy "..\Scripts\umd\popper.js.map" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\umd" /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\snap" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\snap"
	echo ..\Scripts\snap\snap.svg-min.js
	copy "..\Scripts\snap\snap.svg-min.js" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Scripts\snap" /Y
	echo ..\Content\bootstrap.min.css
	copy "..\Content\bootstrap.min.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\bootstrap.min.css.map
	copy "..\Content\bootstrap.min.css.map" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\font-awesome.min.css
	copy "..\Content\font-awesome.min.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\default.css
	copy "..\Content\default.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\Shake.css
	copy "..\Content\Shake.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\jquery-ui.css
	copy "..\Content\jquery-ui.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\icon.css
	copy "..\Content\icon.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\bootstrap-grid.min.css
	copy "..\Content\bootstrap-grid.min.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y
	echo ..\Content\bootstrap-reboot.min.css
	copy "..\Content\bootstrap-reboot.min.css" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content" /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\image" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\image"
	xcopy "..\Content\image" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\image" /C /E /H /K /R /Y

	if not exist "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\switcher" md "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\switcher"
	xcopy "..\Content\switcher" "%SolutionDir%MathSheetsSettingApp\App_Data\Work\Content\switcher" /C /E /H /K /R /Y

	echo +++++++++++++++++++++++++++++++++++++++++++++++++++++

	echo ========== MathSheetsSettingWeb Setting... ==========
	if not exist "%SolutionDir%MathSheetsSettingWeb\Scripts" md "%SolutionDir%MathSheetsSettingWeb\Scripts"
	echo ..\Scripts\bootstrap.min.js
	copy "..\Scripts\bootstrap.min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\bootstrap.min.js.map
	copy "..\Scripts\bootstrap.min.js.map" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery-ui.js
	copy "..\Scripts\jquery-ui.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery-3.3.1.min.js
	copy "..\Scripts\jquery-3.3.1.min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery.base64.js
	copy "..\Scripts\jquery.base64.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery.PrintArea.js
	copy "..\Scripts\jquery.PrintArea.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\json2.min.js
	copy "..\Scripts\json2.min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery.easing.1.3.js
	copy "..\Scripts\jquery.easing.1.3.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\store.legacy.min.js
	copy "..\Scripts\store.legacy.min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery.linq.min.js
	copy "..\Scripts\jquery.linq.min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y
	echo ..\Scripts\jquery.rating-stars.min.js
	copy "..\Scripts\jquery.rating-stars.min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts" /Y

	if not exist "%SolutionDir%MathSheetsSettingWeb\Scripts\umd" md "%SolutionDir%MathSheetsSettingWeb\Scripts\umd"
	echo ..\Scripts\umd\popper.js
	copy "..\Scripts\umd\popper.js" "%SolutionDir%MathSheetsSettingWeb\Scripts\umd" /Y
	echo ..\Scripts\umd\popper.js.map
	copy "..\Scripts\umd\popper.js.map" "%SolutionDir%MathSheetsSettingWeb\Scripts\umd" /Y

	if not exist "%SolutionDir%MathSheetsSettingWeb\Scripts\snap" md "%SolutionDir%MathSheetsSettingWeb\Scripts\snap"
	echo ..\Scripts\snap\snap.svg-min.js
	copy "..\Scripts\snap\snap.svg-min.js" "%SolutionDir%MathSheetsSettingWeb\Scripts\snap" /Y
	echo ..\Content\bootstrap.min.css
	copy "..\Content\bootstrap.min.css" "%SolutionDir%MathSheetsSettingWeb\Content" /Y
	echo ..\Content\bootstrap.min.css.map
	copy "..\Content\bootstrap.min.css.map" "%SolutionDir%MathSheetsSettingWeb\Content" /Y
	echo ..\Content\font-awesome.min.css
	copy "..\Content\font-awesome.min.css" "%SolutionDir%MathSheetsSettingWeb\Content" /Y
	echo ..\Content\jquery-ui.css
	copy "..\Content\jquery-ui.css" "%SolutionDir%MathSheetsSettingWeb\Content" /Y
	echo ..\Content\icon.css
	copy "..\Content\icon.css" "%SolutionDir%MathSheetsSettingWeb\Content" /Y
	echo ..\Content\bootstrap-grid.min.css
	copy "..\Content\bootstrap-grid.min.css" "%SolutionDir%MathSheetsSettingWeb\Content" /Y
	echo ..\Content\bootstrap-reboot.min.css
	copy "..\Content\bootstrap-reboot.min.css" "%SolutionDir%MathSheetsSettingWeb\Content" /Y

)