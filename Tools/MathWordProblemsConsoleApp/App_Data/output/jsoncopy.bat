@echo off

@echo ***********************************************
@echo 0: All
@echo 1: GapFillingProblemsLibrary
@echo 2: MathWordProblemsLibrary
@echo 3: TopicManagement
@echo ***********************************************

@set /p inout=INOUT(0...3):

if "%inout%"=="0" (
	copy GapFillingProblemsLibrary.json ..\..\..\..\ComputationalStrategy\TopicModule\GapFillingProblems\Config /-Y
	copy MathWordProblemsLibrary.json ..\..\..\..\ComputationalStrategy\TopicModule\MathWordProblems\Config /-Y
	copy TopicManagement.json ..\..\..\..\MathSheetsSettingApp\Config /-Y
	copy TopicManagement.json ..\..\..\..\MathSheetsSettingWebApi\Config /-Y)
if "%inout%"=="1" (
	copy GapFillingProblemsLibrary.json ..\..\..\..\ComputationalStrategy\TopicModule\GapFillingProblems\Config /-Y
)
if "%inout%"=="2" (
	copy MathWordProblemsLibrary.json ..\..\..\..\ComputationalStrategy\TopicModule\MathWordProblems\Config /-Y
)
if "%inout%"=="3" (
	copy TopicManagement.json ..\..\..\..\MathSheetsSettingApp\Config /-Y
	copy TopicManagement.json ..\..\..\..\MathSheetsSettingWebApi\Config /-Y)
)

