@ECHO OFF

CALL "%~dp0\validateCygwinBinaries.cmd"
IF "1"=="%ERRORLEVEL%" (
    @ECHO Invalid or incomplete Cygwin installation. Install cygwin and its components viz.
    @ECHO grep sed perl cut touch wget sort
    EXIT /b 1
)
SET CYGWIN_EXE=%CYGWIN_HOME%\bin

where xsd.exe > NUL 2>&1
IF "1"=="%ERRORLEVEL%" (
    @ECHO Unable to find xsd.exe in the path. Locate it and add it directory to the path
    @ECHO Usually it is found under Microsoft SDK viz. "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\"
    EXIT /b 1
)

SETLOCAL
@ECHO Starting %DATE%-%TIME%

SET LOCALXSD=%TEMP%\AnetApiSchema.xsd
SET LOCALWSDL=%TEMP%\AnetApiSchema.wsdl
SET selection=N
CHOICE /C YN  /T 10 /D N /M "Fetch and update Schema/WSDL file from remote server?"
IF "%ERRORLEVEL%"=="1" (
    @ECHO Fetching Schema/WSDL files 
    SET %ERRORLEVEL%=    
    CALL "%~dp0\getXsdWsdl.cmd" %LOCALXSD% %LOCALWSDL%
    SET ERRORCODE=%ERRORLEVEL%
    @ECHO GetXsdWsdl Call Exit Code:%ERRORCODE%
    IF NOT "%ERRORLEVEL%"=="0" (
       @ECHO Error fetching source files
       @ECHO ##### ***** $$$$$ CHECK FOR ERROR $$$$$ ***** #####
       REM EXIT /b 1
    )
) ELSE (
    @ECHO Schema/WSDL files have not been updated!
)

SET XSDSRCDIR=Authorize.NET\Api\Contracts\V1
SET XSDPACKAGE=AuthorizeNet.API.FrontEnd.v1
SET XSDPACKAGE=AuthorizeNet.Api.Contracts.V1

SET WSDLSRCDIR=wsdl
SET WSDLPACKAGE=ANetApiFE.ANetApiWS

IF NOT EXIST "%LOCALXSD%" (
    @ECHO Unable to find "%LOCALXSD%"
    EXIT /b 1
)
IF NOT EXIST "%LOCALWSDL%" (
    @ECHO Unable to find "%LOCALWSDL%"
    @REM EXIT /b 1
)
@ECHO Validating target folder "%XSDSRCDIR%"
IF NOT EXIST %XSDSRCDIR% (
    MD "%XSDSRCDIR%"
)
@ECHO Validating target folder "%WSDLSRCDIR%"
IF NOT EXIST %WSDLSRCDIR% (
    MD "%WSDLSRCDIR%"
)

@ECHO Generating sources from Schema: %XSD% in folder "%XSDSRCDIR%"
@ECHO: Command Line: xsd.exe  /c /f /l:cs /edb /eld /out:"%XSDSRCDIR%" /n:"%XSDPACKAGE%" "%LOCALXSD%"
xsd.exe  /c /f /l:cs /edb /eld /out:"%XSDSRCDIR%" /n:"%XSDPACKAGE%" "%LOCALXSD%"
@ECHO Renaming the generated file
IF EXIST "%XSDSRCDIR%\AnetApiSchema.generated.cs" (DEL /q "%XSDSRCDIR%\AnetApiSchema.generated.cs")
REN %XSDSRCDIR%\AnetApiSchema.cs AnetApiSchema.generated.cs

@ECHO Generating the proxy for old code
xsd.exe  /c /f /l:cs /out:"Authorize.NET\Utility" /n:"AuthorizeNet.APICore" "%LOCALXSD%"
@ECHO Renaming the generated file
IF EXIST "Authorize.NET\Utility\AnetApiSchema.generated.cs" (DEL /q "Authorize.NET\Utility\AnetApiSchema.generated.cs")
REN Authorize.NET\Utility\AnetApiSchema.cs AnetApiSchema.generated.cs

REM xsd.exe  /c /f /l:cs /edb /out:"%XSDSRCDIR%" /n:"%XSDPACKAGE%" "%LOCALXSD%"
REM DATASET 
REM xsd.exe  /f /l:cs /d /edb /eld /out:"%XSDSRCDIR%" /n:"%XSDPACKAGE%" "%LOCALXSD%"

@ECHO Generating source from WSDL: %WSDL% in folder "%WSDLSRCDIR%"
REM wsdl.exe    /f /l:cs      /out:"%WSDLSRCDIR%" /n:"%WSDLPACKAGE%" "%LOCALWSDL%"
REM wsdl.exe /nologo /order /l:cs /out:"%WSDLSRCDIR%" /n:"%WSDLPACKAGE%" "%LOCALWSDL%"
@REM           wsdl.exe /f /nologo /order /l:cs /out:"%WSDLSRCDIR%" /n:"%WSDLPACKAGE%" "%LOCALWSDL%"

ENDLOCAL
@ECHO FINISHED %DATE%-%TIME%
