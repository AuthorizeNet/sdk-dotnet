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

SET LOCALXSD="%~dp0\AnetApiSchema.xsd"
@ECHO Local XSD path:
@ECHO %LOCALXSD%
REM SET LOCALWSDL=%TEMP%\AnetApiSchema.wsdl
REM SET selection=N
REM CHOICE /C YN  /T 10 /D N /M "Fetch and update Schema/WSDL file from remote server?"
REM IF "%ERRORLEVEL%"=="1" (
    REM @ECHO Fetching Schema/WSDL files 
    REM SET %ERRORLEVEL%=    
    REM CALL "%~dp0\getXsdWsdl.cmd" %LOCALXSD% %LOCALWSDL%
    REM SET ERRORCODE=%ERRORLEVEL%
    REM @ECHO GetXsdWsdl Call Exit Code:%ERRORCODE%
    REM IF NOT "%ERRORLEVEL%"=="0" (
       REM @ECHO Error fetching source files
       REM @ECHO ##### ***** $$$$$ CHECK FOR ERROR $$$$$ ***** #####
       REM REM EXIT /b 1
    REM )
REM ) ELSE (
    REM @ECHO Schema/WSDL files have not been updated!
REM )

SET XSDSRCDIR=Authorize.NET\Api\Contracts\V1
SET XSDPACKAGE=AuthorizeNet.API.FrontEnd.v1
SET XSDPACKAGE=AuthorizeNet.Api.Contracts.V1

SET WSDLSRCDIR=wsdl
SET WSDLPACKAGE=ANetApiFE.ANetApiWS

IF NOT EXIST "%LOCALXSD%" (
    @ECHO Unable to find "%LOCALXSD%"
    EXIT /b 1
)
REM IF NOT EXIST "%LOCALWSDL%" (
    REM @ECHO Unable to find "%LOCALWSDL%"
    REM @REM EXIT /b 1
REM )
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

REM @ECHO Generating source from WSDL: %WSDL% in folder "%WSDLSRCDIR%"
REM wsdl.exe    /f /l:cs      /out:"%WSDLSRCDIR%" /n:"%WSDLPACKAGE%" "%LOCALWSDL%"
REM wsdl.exe /nologo /order /l:cs /out:"%WSDLSRCDIR%" /n:"%WSDLPACKAGE%" "%LOCALWSDL%"
@REM           wsdl.exe /f /nologo /order /l:cs /out:"%WSDLSRCDIR%" /n:"%WSDLPACKAGE%" "%LOCALWSDL%"

ENDLOCAL
@ECHO FINISHED %DATE%-%TIME%
