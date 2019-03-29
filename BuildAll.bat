@ECHO OFF
TITLE BuildALL

SET LOGFILE=%~n0.log

REM CALL "%VS150COMNTOOLS%\VsDevCmd.bat"
REM PATH "VS150COMNTOOLS" = "C:\Program Files\Microsoft Visual Studio\2017\Professional\Common7\Tools"
MSBuild.exe Build.xml /fileLogger /fileLoggerParameters:LogFile=%LOGFILE%;Encoding=UTF-8 /maxcpucount /nodeReuse:false /consoleloggerparameters:ErrorsOnly;WarningsOnly;Summary;

PAUSE