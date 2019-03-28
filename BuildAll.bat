@ECHO OFF
TITLE BuildALL

SET LOGFILE=%~n0.log

REM CALL "%VS150COMNTOOLS%\VsDevCmd.bat"
MSBuild.exe Build.xml /fileLogger /fileLoggerParameters:LogFile=%LOGFILE%;Encoding=UTF-8 /maxcpucount /nr:False /consoleloggerparameters:ErrorsOnly;WarningsOnly

PAUSE