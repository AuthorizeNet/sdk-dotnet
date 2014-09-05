@ECHO OFF

@ECHO Starting %DATE%-%TIME%

SET CYGWIN=NODOSFILEWARNING

SET CDIR=%CD%
SET SRCDIR=Authorize.NET
SET GENFOLDER=Api\Contracts\v1
SET CONTROLLERFOLDER=Api\Controllers

IF NOT EXIST "%SRCDIR%" (
	@ECHO Unable to find "%SRCDIR%"
	EXIT /b 1
)
@ECHO Identifying Requests/Responses to process from "%SRCDIR%"
DIR /s %SRCDIR%\%GENFOLDER%\*.cs > %TEMP%\Sources0.log
pushd %SRCDIR%\%GENFOLDER%\
grep -i -e "request" -e "response" *.cs | grep -i anetapiresponse | grep -i class > %TEMP%\Sources0.log
popd
DIR /s %SRCDIR%\%CONTROLLERFOLDER%\*Controller.cs > %TEMP%\Controllers0.log

@ECHO Cleaning up paths in Sources and Controllers
cut -f2- -d: %TEMP%\Sources0.log   | cut -c26- | cut -d: -f1    | sort -u  > %TEMP%\Sources1.log
cut -c40- %TEMP%\Controllers0.log  | sort -u   | grep -i "\.cs" | cut -d. -f1 | sort -u > %TEMP%\Controllers.log

@ECHO Getting Unique Request/Responses
grep -i -e "request *$" -e "response *$" %TEMP%\Sources1.log > %TEMP%\Sources2.log

@ECHO Identifying Object names
perl -pi -w -e 's/Request *$//g;'  %TEMP%\Sources2.log 
perl -pi -w -e 's/Response *$//g;' %TEMP%\Sources2.log  
sort -u %TEMP%\Sources2.log      > %TEMP%\Sources3.log 

@ECHO Fixing Controllers
perl -pi -w -e 's/Controller *$//g;' %TEMP%\Controllers.log

@ECHO Removing ExistingControllers From Request/Response List
@ECHO From File
FOR /F %%X IN (%TEMP%\Controllers.log) DO (
	@ECHO Processing "%%X"
	perl -pi -w -e 's/%%X *$//g;' %TEMP%\Sources3.log  	
)
@ECHO From BlackList
FOR %%X IN (ANetApi authenticateTest Error Ids isAlive logout XXDoNotUseDummy) DO (
	@ECHO Processing BlackList "%%X"
	perl -pi -w -e 's/%%X *$//g;' %TEMP%\Sources3.log  	
)

@ECHO Creating Final List of Request/Response to generate code
sort -u %TEMP%\Sources3.log   > %TEMP%\Sources.log 

FOR /F %%x IN (%TEMP%\Sources.log ) DO (
	IF EXIST "%SRCDIR%\%CONTROLLERFOLDER%\%%xController.cs" (
		@ECHO "%SRCDIR%\%CONTROLLERFOLDER%\%%xController.cs" exists, Creating New 
		COPY %SRCDIR%\Api\ControllerTemplate.cst   "%SRCDIR%\%CONTROLLERFOLDER%\%%xController.new"
		perl -pi -w -e 's/APICONTROLLERNAME/%%x/g;' %SRCDIR%\%CONTROLLERFOLDER%\%%xController.new
	) ELSE (
		@ECHO Generating Code for "%SRCDIR%\%CONTROLLERFOLDER%\%%xController.cs"
		COPY %SRCDIR%\Api\ControllerTemplate.cst   "%SRCDIR%\%CONTROLLERFOLDER%\%%xController.cs"
		perl -pi -w -e 's/APICONTROLLERNAME/%%x/g;' %SRCDIR%\%CONTROLLERFOLDER%\%%xController.cs
	)
)
DEL %SRCDIR%\%CONTROLLERFOLDER%\*.bak 1>NUL 2>&1

ENDLOCAL
 	
@ECHO FINISHED %DATE%-%TIME%
