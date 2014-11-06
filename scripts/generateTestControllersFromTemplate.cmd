@ECHO OFF

@ECHO Starting %DATE%-%TIME%

SET CYGWIN=NODOSFILEWARNING 

SET CDIR=%CD%
SET SRCDIR=.
SET GENFOLDER=Authorize.NET\Api\Contracts\V1
SET TESTDIR=.
SET CONTROLLERFOLDER=AuthorizeNETTest\Api\Controllers\MockTest

SET SRCLOG=%CD%\log\TestSources
SET CNTLOG=%CD%\log\TestControllers
IF EXIST "%CD%\log" (
    DEL /q/s "%CD%\log\*.*" > NUL
) ELSE (
    MD "%CD%\log\"
)

IF NOT EXIST "%SRCDIR%" (
    @ECHO Unable to find "%SRCDIR%"
    EXIT /b 1
)
@ECHO Identifying Requests/Responses to process from "%SRCDIR%"
DIR /s %SRCDIR%\%GENFOLDER%\AnetApiSchema.generated.cs > %SRCLOG%0.log
pushd %SRCDIR%\%GENFOLDER%\
grep -i -e "request *:" -e "response *:" *.cs | grep -i class > %SRCLOG%0.log 
popd
DIR /s %TESTDIR%\%CONTROLLERFOLDER%\*ControllerTest.cs > %CNTLOG%0.log

@ECHO Cleaning up paths in Sources and Controllers
cut -c53- %SRCLOG%0.log  | cut -d: -f1 | sort -u  > %SRCLOG%1.log 
cut -c40- %CNTLOG%0.log  | sort -u | grep -i "\.cs" | cut -d. -f1 | sort -u > %CNTLOG%.log 

@ECHO Getting Unique Request/Responses
grep -i -e "request *$" -e "response *$" %SRCLOG%1.log > %SRCLOG%2.log 

@ECHO Identifying Object names
perl -pi -w -e 's/Request *$//g;'  %SRCLOG%2.log  
perl -pi -w -e 's/Response *$//g;' %SRCLOG%2.log  
sort -u %SRCLOG%2.log      > %SRCLOG%3.log  

@ECHO Fixing Controllers
perl -pi -w -e 's/ControllerTest *$//g;' %CNTLOG%.log 
@REM Create backup for later comparison
COPY %SRCLOG%3.log %SRCLOG%4.log >NUL
COPY %CNTLOG%.log  %CNTLOG%9.log >NUL

@ECHO Removing ExistingControllers From Request/Response List
@ECHO From File
FOR /F %%X IN (%CNTLOG%.log) DO (
    @ECHO Processing "%%X"
    perl -pi -w -e 's/^\b%%X\b *$//g;' %SRCLOG%3.log
)

@ECHO From BlackList
FOR %%X IN (ANetApi Error Ids XXDoNotUseDummy) DO (
    @ECHO Processing BlackList "%%X"
    perl -pi -w -e 's/^\b%%X\b *$//g;' %SRCLOG%3.log
)

@ECHO Creating Final List of Request/Response to generate code
sort -u %SRCLOG%3.log   > %SRCLOG%.log  

FOR /F %%x IN (%SRCLOG%.log ) DO (
    IF EXIST "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs" (
        @ECHO "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs" exists, Creating New 
        COPY AuthorizeNETtest\Api\ControllerTemplateTest.cst  "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.new"
        perl -pi -w -e 's/APICONTROLLERNAME/%%x/g;'            %SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.new 
    ) ELSE (
        @ECHO Generating Code for "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs"
        COPY AuthorizeNETtest\Api\ControllerTemplateTest.cst  "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs"
        perl -pi -w -e 's/APICONTROLLERNAME/%%x/g;'            %SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs 
    )
)
@REM Identify Obsolete Controllers
@ECHO From Request/ResponseList
FOR /F %%X IN (%SRCLOG%4.log) DO (
	@ECHO Processing "%%X"
	perl -pi -w -e 's/%%X *$//g;' %CNTLOG%9.log  	
)
@ECHO Following are Obsolete Controllers
sort -u %CNTLOG%9.log
DEL /s *.bak 1>NUL 2>&1
     
ENDLOCAL
 	
@ECHO FINISHED %DATE%-%TIME%
