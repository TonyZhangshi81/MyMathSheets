@echo off

set ProjectName=%~1
set ConfigurationName=%~2
set ProjectDir=%~3

echo ========== %ProjectName% (%ConfigurationName%) Build Complete ==========

echo ========== MathSheetsSettingApp Setting ... ==========

copy %ProjectDir%Template\HTMLPage %ProjectDir%..\..\MathSheets.Application\MathSheetsApp\HtmlTemplate /Y

echo +++++++++++++++++++++++++++++++++++++++++++++++++++++

echo ========== MathSheetsSettingWeb Setting... ==========

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\Ext md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\Ext
xcopy %ProjectDir%Scripts\Ext %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\Ext /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Plugin md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Plugin
xcopy %ProjectDir%Plugin %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Plugin /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Games md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Games
xcopy %ProjectDir%Games %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Games /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\fonts md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\fonts
xcopy %ProjectDir%Content\fonts %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\fonts /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\css md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\css
xcopy %ProjectDir%Content\css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\css /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\icons md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\icons
xcopy %ProjectDir%Content\icons %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\icons /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Page md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Page

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts
copy %ProjectDir%Scripts\bootstrap.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\bootstrap.min.js.map %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery-ui.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery-3.3.1.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.base64.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.PrintArea.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\json2.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.easing.1.3.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\store.legacy.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.linq.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.linq.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.rating-stars.min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\jquery.dropdown.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\popper.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y
copy %ProjectDir%Scripts\popper.js.map %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\umd md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\umd
copy %ProjectDir%Scripts\umd\popper.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\umd /Y
copy %ProjectDir%Scripts\umd\popper.js.map %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\umd /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\snap md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\snap
copy %ProjectDir%Scripts\snap\snap.svg-min.js %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Scripts\snap /Y
copy %ProjectDir%Content\bootstrap.min.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\bootstrap.min.css.map %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\normalize.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\font-awesome.min.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\default.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\Shake.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\jquery-ui.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\icon.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\bootstrap-grid.min.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y
copy %ProjectDir%Content\bootstrap-reboot.min.css %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\image md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\image
xcopy %ProjectDir%Content\image %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\image /C /E /H /K /R /Y

if not exist %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\switcher md %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\switcher
xcopy %ProjectDir%Content\switcher %ProjectDir%..\..\MathSheets.Presentation\MathSheets\AppData\Work\Content\switcher /C /E /H /K /R /Y

echo +++++++++++++++++++++++++++++++++++++++++++++++++++++

echo ========== WebApi Setting... ==========

copy %ProjectDir%Template\HTMLPage %ProjectDir%..\..\MathSheets.Application\MathSheetsService\HtmlTemplate /Y
