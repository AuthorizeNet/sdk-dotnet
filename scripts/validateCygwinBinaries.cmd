@ECHO OFF

@ECHO Validating Cygwin installation and its required components

IF ""=="%CYGWIN_HOME%" (
    @ECHO CYGWIN_HOME not set. Pl. make sure cygwin is installed and set CYGWIN_HOME to point to it. 
    EXIT /b 1
)
IF NOT EXIST "%CYGWIN_HOME%" (
    @ECHO %CYGWIN_HOME% does not exist.
    EXIT /b 1
)
IF NOT EXIST "%CYGWIN_HOME%\bin" (
    @ECHO %CYGWIN_HOME%\bin does not exist.
    EXIT /b 1
)
SET CYGWIN_EXE=%CYGWIN_HOME%\bin
FOR %%x IN (grep sed perl cut touch wget sort) DO (
    IF NOT EXIST "%CYGWIN_EXE%\%%x.exe" (
        @ECHO "%CYGWIN_EXE%\%%x.exe" does not exist. Pl. make sure to install cygwin utility "%%x"
        EXIT /b 1
    )
)
