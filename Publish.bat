@ECHO OFF

TITLE Publish

SET LOGFILE=%~n0.log

MSBuild.exe Publish.xml /fileLogger /fileLoggerParameters:LogFile=%LOGFILE%;Encoding=UTF-8 /maxcpucount /nodeReuse:false /consoleloggerparameters:ErrorsOnly;WarningsOnly;Summary;

PAUSE