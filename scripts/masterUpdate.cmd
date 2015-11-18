@ECHO OFF

CALL "%~dp0\validateCygwinBinaries.cmd"
IF "1"=="%ERRORLEVEL%" (
    @ECHO Invalid or incomplete Cygwin installation. Install cygwin and its components viz.
    @ECHO grep sed perl cut touch wget sort
    EXIT /b 1
)
SET CYGWIN_EXE=%CYGWIN_HOME%\bin

@ECHO. Started at %date%-%time%
@ECHO This script will update all the generated code
@ECHO. 

FOR %%x IN ( generateObjectsFromXsd.cmd generateControllersFromTemplate.cmd generateTestControllersFromTemplate.cmd generateTestForEnums.cmd generateRequestFactorySpecified.cmd) DO (
    @ECHO Executing script "%%x"
    CALL "%~dp0%%x"
    IF "1"=="%ERRORLEVEL%" (
        @ECHO. ########################################################################
        @ECHO Encountered error during execution of "%~dp0%%x"
        @ECHO See logs or output above. 
        @ECHO Exiting, Update ***NOT*** complete.
        EXIT /b 1
    )
)
@ECHO. 
@ECHO Exiting, Update completed succesfully.
@ECHO Compile, run tests and commit in git-hub.
@ECHO. Completed at %date%-%time%