@ECHO OFF
CALL "%~dp0\validateCygwinBinaries.cmd"
IF "1"=="%ERRORLEVEL%" (
    @ECHO Invalid or incomplete Cygwin installation. Install cygwin and its components viz.
    @ECHO grep sed perl cut touch wget sort
    EXIT /b 1
)
SET CYGWIN_EXE=%CYGWIN_HOME%\bin

SETLOCAL
@ECHO Starting %DATE%-%TIME%

IF "%1"=="" (
    @ECHO Invalid Local XSD "%1"
    EXIT /b 1
)
IF "%2"=="" (
    @ECHO Invalid Local WSDL "%2"
    EXIT /b 1
)

SET LOCALXSD=%1
SET LOCALWSDL=%2

SET PROXY=%https.proxyHost%:%https.proxyPort%
SET HOST=apitest.authorize.net
SET PROTOCOL=https

@REM SET PROXY=%http.proxyHost%:%http.proxyPort%
@REM SET HOST=ww730vSmBu114.%USERDNSDOMAIN%
@REM SET PROTOCOL=http

SET XSD=%PROTOCOL%://%HOST%/xml/v1/schema/AnetApiSchema.xsd
SET WSDL=%PROTOCOL%://%HOST%/ANetApiWS/ANetApiWS.asmx?wsdl

@ECHO Fetching  XSD from:%XSD%
@ECHO Fetching WSDL from:%WSDL%
@ECHO Press Enter to continue
pause
DEL /Q %LOCALXSD%
DEL /Q %LOCALWSDL%

@ECHO Fetching Schema: %XSD% 
bitsadmin.exe /transfer "XSD Download" /DOWNLOAD %XSD%  %LOCALXSD%
IF NOT "%ERRORLEVEL%"=="0" (
    @ECHO Unable to fetch "%XSD%"
    EXIT /b 1
)

@ECHO Fetching WSDL:   %WSDL% 
bitsadmin.exe /transfer "WSDL Download" /DOWNLOAD %WSDL% %LOCALWSDL%
IF NOT "%ERRORLEVEL%"=="0" (
    SET ERRORLEVEL=
    IF EXIST "ANetApiWS.asmx@wsdl" (
        DEL /Q "ANetApiWS.asmx@wsdl"
    )
    @ECHO Unable to fetch "%WSDL%" via bitsadmin, trying wget
@rem    "%CYGWIN_EXE%\wget.exe" %WSDL% 
REM     IF "%ERRORLEVEL%"=="1" (
REM         @ECHO Unable to fetch "%WSDL%" via wget
REM         EXIT /b 1
REM     )
     IF EXIST "ANetApiWS.asmx@wsdl" (
        COPY "ANetApiWS.asmx@wsdl" "%LOCALWSDL%"
        DEL /Q "ANetApiWS.asmx@wsdl"
    ) ELSE (
@rem        @ECHO Unable to fetch "%WSDL%" via wget
		@ECHO Escape fetching %WSDL%
        @REM EXIT /b 1    
    )
)
IF NOT EXIST "%LOCALXSD%" (
    @ECHO Unable to find "%LOCALXSD%"
    EXIT /b 1
)
@rem IF NOT EXIST "%LOCALWSDL%" (
@rem     @ECHO Unable to find "%LOCALWSDL%"
@rem     @REM EXIT /b 1
@rem )
@ECHO %0 Exit Code:'%ERRORLEVEL%'
ENDLOCAL

@ECHO FINISHED %DATE%-%TIME%
