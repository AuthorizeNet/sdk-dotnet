@ECHO OFF
@ECHO Script to prepare dlls for VeraCode scan

IF NOT EXIST "Authorize.NET\bin\USELOCAL\AuthorizeNet.dll" (
    @ECHO AuthorizeNet.dll not found, build and make sure yo are in correct working directory
    EXIT /b 1
)
SET target=veracode
IF NOT EXIST "%TARGET%" (MD "%TARGET%")

COPY /y Authorize.NET\bin\USELOCAL\AuthorizeNet.* "%TARGET%"\.
COPY /y AuthorizeNETtest\bin\USELOCAL\AuthorizeNet_Accessor.* "%TARGET%"\.
COPY /y AuthorizeNETtest\bin\USELOCAL\AuthorizeNETtest.* "%TARGET%"\.
COPY /y CoffeeShopWebApp\bin\CoffeeShopWebApp.* "%TARGET%"\.
 
@ECHO Looking for Microsoft.VisualStudio.QualityTools.UnitTestFramework 
SET MSVSTEST=C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\PublicAssemblies\Microsoft.VisualStudio.QualityTools.UnitTestFramework.dll
IF NOT EXIST "%MSVSTEST%" (
    @ECHO "%MSVSTEST%" not found
    EXIT /b 1
)
COPY /y "%MSVSTEST%" "%TARGET%"\.
