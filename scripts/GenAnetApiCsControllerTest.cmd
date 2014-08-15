@ECHO OFF

@ECHO Starting %DATE%-%TIME%
SET CYGWIN=NODOSFILEWARNING 
SET CDIR=%CD%
SET SRCDIR=.
SET GENFOLDER=Authorize.NET\Api\Contracts\V1
SET CONTROLLERFOLDER=AuthorizeNETTest\Api\Controllers\Test

IF NOT EXIST "%SRCDIR%" (
    @ECHO Unable to find "%SRCDIR%"
    EXIT /b 1
)
@ECHO Identifying Requests/Responses to process from "%SRCDIR%"
DIR /s %SRCDIR%\%GENFOLDER%\AnetApiSchema.generated.cs > %TEMP%\Sources0.log
pushd %SRCDIR%\%GENFOLDER%\
grep -i -e "request" -e "response" *.cs | grep -i anetapi | grep -i class > %TEMP%\Sources0.log 
popd
DIR /s %SRCDIR%\%CONTROLLERFOLDER%\*Test.cs > %TEMP%\Controllers0.log

@ECHO Cleaning up paths in Sources and Controllers
cut -c53- %TEMP%\Sources0.log      | cut -d: -f1 | sort -u  > %TEMP%\Sources1.log 
cut -c40- %TEMP%\Controllers0.log  | sort -u | grep -i "\.cs" | cut -d. -f1 | sort -u > %TEMP%\Controllers.log 

@ECHO Getting Unique Request/Responses
grep -i -e "request *$" -e "response *$" %TEMP%\Sources1.log > %TEMP%\Sources2.log 

@ECHO Identifying Object names
perl -pi -w -e 's/Request *$//g;'  %TEMP%\Sources2.log  
perl -pi -w -e 's/Response *$//g;' %TEMP%\Sources2.log  
sort -u %TEMP%\Sources2.log      > %TEMP%\Sources3.log  

@ECHO Fixing Controllers
perl -pi -w -e 's/ControllerTest *$/Controller/g;' %TEMP%\Controllers.log 

@ECHO Removing ExistingControllers From Request/Response List
@ECHO From File
FOR /F %%X IN (%TEMP%\Controllers.log) DO (
    @ECHO Processing "%%X"
    perl -pi -w -e 's/%%X//g;' %TEMP%\Sources3.log  
)
@ECHO From BlackList
FOR %%X IN ( authenticateTest isAlive logout ANetApi XXDoNotUseDummy) DO (
    @ECHO Processing "%%X"
    perl -pi -w -e 's/%%X//g;' %TEMP%\Sources3.log      
)

@ECHO Creating Final List of Request/Response to generate code
sort -u %TEMP%\Sources3.log   > %TEMP%\Sources.log  

FOR /F %%x IN (%TEMP%\Sources.log ) DO (
    IF EXIST "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs" (
        @ECHO "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs" exists, Creating New 
        COPY AuthorizeNETtest\Api\ControllerTemplateTest.cst  "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.new" 1>NUL
        perl -pi -w -e 's/APICONTROLLERNAME/%%x/g;'            %SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.new 
    ) ELSE (
        @ECHO Generating Code for "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs"
        COPY AuthorizeNETtest\Api\ControllerTemplateTest.cst  "%SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs" 1>NUL
        perl -pi -w -e 's/APICONTROLLERNAME/%%x/g;'            %SRCDIR%\%CONTROLLERFOLDER%\%%xControllerTest.cs 
    )
)
DEL %SRCDIR%\%CONTROLLERFOLDER%\*.bak
     
@ECHO FINISHED %DATE%-%TIME%
