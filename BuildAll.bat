@ECHO OFF

TITLE BuildALL

SET LOGFILE=%~n0.log
SET CONFIGURATION=Debug

msbuild MyMathSheets.All.sln -t:Clean;Rebuild -p:Configuration=%CONFIGURATION% -m -nr:false -fileLoggerParameters:LogFile=%LOGFILE%;Encoding=UTF-8 -consoleloggerparameters:ErrorsOnly;WarningsOnly;Summary;

PAUSE