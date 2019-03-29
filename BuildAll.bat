@ECHO OFF
TITLE BuildALL

SET LOGFILE=%~n0.log

MSBuild.exe Build.xml /fileLogger /fileLoggerParameters:LogFile=%LOGFILE%;Encoding=UTF-8 /maxcpucount /nodeReuse:false /consoleloggerparameters:ErrorsOnly;WarningsOnly;Summary;

PAUSE