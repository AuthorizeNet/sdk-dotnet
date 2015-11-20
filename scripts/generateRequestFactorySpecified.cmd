@rem Creating RequestFactoryWithSpecified file which handles the elements having minoccurs=0 in XSD
@rem It generates specified property for such element and sets them true if the required condition is met
@ECHO OFF
CALL "%~dp0\validateCygwinBinaries.cmd"
IF "1"=="%ERRORLEVEL%" (
    @ECHO Invalid or incomplete Cygwin installation. Install cygwin and its components viz.
    @ECHO grep sed perl cut touch wget sort
    EXIT /b 1
)
SET CYGWIN_EXE=%CYGWIN_HOME%\bin
SET OUTDIR=%Temp%
SET SRCDIR=Authorize.NET\Api\Contracts\V1
SET OUTFILE=%SRCDIR%\RequestFactoryWithSpecified.generated.org
SET INFILE=%SRCDIR%\AnetApiSchema.generated.cs
SET BACKUPFILE=%OUTDIR%\RequestFactoryWithSpecified_Backup.generated.org
SET SPECIFIEDFILE=%OUTDIR%\splst.txt 

IF NOT EXIST "%CYGWIN_EXE%" (
	@ECHO "%CYGWIN_EXE%" DOES NOT EXIST
	EXIT /B 1
)
IF NOT EXIST "%SRCDIR%" (
	@ECHO "%SRCDIR%" Does Not exist
	EXIT /B 1
)

IF EXIST "%OUTFILE%" (
	"%CYGWIN_EXE%\rm.exe" %OUTFILE%
)

@ECHO ### Generating request factory specified
@ECHO namespace AuthorizeNet.Api.Contracts.V1 > %OUTFILE%
@ECHO { >> %OUTFILE%
@rem replacing using System; with HTHT### because it is causing problem for one the regular expression
@ECHO   HTHT### >> %OUTFILE%

@ECHO #pragma warning disable 169 >> %OUTFILE%
@ECHO #pragma warning disable 1591 >> %OUTFILE%
@ECHO // ReSharper disable InconsistentNaming >> %OUTFILE%
@ECHO   /// ^<summary^> >> %OUTFILE%
@ECHO   /// Special case handlers >> %OUTFILE%
@ECHO   /// >> %OUTFILE%
@ECHO   /// validated on ????/??/?? for objects listed at the end >> %OUTFILE%
@ECHO   /// should be validated after each update of AnetApiSchema.cs >> %OUTFILE%
@ECHO   /// for fields/properties that are minOccurs="0" since xsd.exe >> %OUTFILE%
@rem replacing the word specified with WWWW because further in script we are greping all the parameters having specified word - this was creating a problem
@ECHO   /// generates "WWWW" property for such fields and requires >> %OUTFILE%
@ECHO   /// special handling to set them seamlessly >> %OUTFILE%
@ECHO   /// Make sure to update the respective controllers to call the respective request hand >> %OUTFILE%
@ECHO   ///  >> %OUTFILE%
@ECHO   /// ^</summary^> >> %OUTFILE%
@ECHO     public static class RequestFactoryWithSpecified >> %OUTFILE%
@ECHO     {>> %OUTFILE%

"%CYGWIN_EXE%\grep.exe" -i "class\|specified\|typeof\|type\|public" %INFILE%  | "%CYGWIN_EXE%\grep.exe" -i -v "string\|event" >> %OUTFILE% 

rem creating a back up file
"%CYGWIN_EXE%\cp.exe" -f %OUTFILE% %BACKUPFILE%

@rem replacing public partial class with public static void
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/public partial class/        }\n        }\n        public static void/g' %OUTFILE%

@ECHO ### Deleting unwanted lines from file - may take sometime 
@rem remove everything after colon public partial class merchantAuthenticationType : object, System.ComponentModel.INotifyPropertyChanged {
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/:.*/\n        {\n            if(null != argument) \n            {\n/g if /: object/' %OUTFILE%

@rem remove everything after colon  public partial class authenticateTestRequest : ANetApiRequest  
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/:.*/\n        {\n            if(null != argument) \n            {\n/g if /: ANetApi/' %OUTFILE%

@ rem remove "public enum bankAccountTypeEnum"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/public enum.*//g' %OUTFILE%

@ rem remove ["System.Xml.Serialization.XmlTypeAttribute(Namespace="AnetApi/xml/v1/schema/AnetApiSchema.xsd")]"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^\[System.Xml.Serialization.XmlTypeAttribute.*$//g' %OUTFILE%

@ rem remove "[System.Xml.Serialization.XmlIncludeAttribute(typeof(creditCardType))]"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^\[System.Xml.Serialization.XmlIncludeAttribute.*$//g' %OUTFILE%

@rem remove before 'typeof' word "[System.Xml.Serialization.XmlElementAttribute("bankAccount", typeof(bankAccountType))]"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^\[System.Xml.Serialization.XmlElementAttribute.*typeof\(/                /g' %OUTFILE%

@rem remove replacing the last three characters ))] in above expression with "(argument)"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/\)\)\]/ABCargumentXXX;/g' %OUTFILE%

@ rem remove  "[System.Xml.Serialization.XmlElementAttribute(DataType="date")]"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^\[System.Xml.Serialization.XmlElementAttribute.*$//g' %OUTFILE%

@ rem remove  "[System.Xml.Serialization.XmlAttributeAttribute(DataType"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^\[System.Xml.Serialization.XmlAttributeAttribute.*$//g' %OUTFILE%

@rem remove the line containing enum at the end of the word accountTypeEnum
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/([A-Za-z0-9]*.Enum.*)$//g if !/public static/' %OUTFILE%

@rem remove the line containing enum at the end of the word ItemChoiceType
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/(ItemChoiceType.*)$//g' %OUTFILE%
@rem replace the curly braces "public partial class emailSettingsType : ArrayOfSetting {" with "public partial class emailSettingsType : ArrayOfSetting ABCargumentXXX"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/{/ABCargumentXXX;/g if /:/' %OUTFILE%

@rem replacing { in "public static void ABCargumentXX {" if (null != argument)
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/:/\n        {\n            if(null != argument) \n            {\n        /g' %OUTFILE%


@rem removing public object from "public object Item;"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public object.*//g' %OUTFILE%

@rem removing all datatypes
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public bool//g' %OUTFILE%
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public int.*;//g' %OUTFILE%
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public decimal.*;//g' %OUTFILE%
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public short.*;//g' %OUTFILE%
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public System.DateTime.*;//g' %OUTFILE%

@rem removing public keyword anywhere in file except the lines containing "public static ..."
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ *public //g if ! /public static /' %OUTFILE%

@rem trying to tokenize "public messagesTypeMessage[] message;"
"%CYGWIN_EXE%\perl.exe" -pi -w -e 's/^ *([A-Za-z0-9]*)\[\] *([A-Za-z0-9]*);/                if(null != argument.$2){ foreach( var value in argument.$2) { $1(value);} } /g' "%OUTFILE%"

@rem grep all the lines having specified word
"%CYGWIN_EXE%\grep.exe" -i "Specified" %OUTFILE% | "%CYGWIN_EXE%\grep.exe" -v "class" | "%CYGWIN_EXE%\sort.exe" -u > %SPECIFIEDFILE% 
@rem removing specified word
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/Specified;*//g' %SPECIFIEDFILE%

@ECHO ### Processing function name - Creating functions
@rem replacing "public static void ARBGetSubscriptionListRequest" with "XYZ ARBGetSubscriptionListRequest ABC ARBGetSubscriptionListRequest argument XXX"
"%CYGWIN_EXE%\perl.exe" -pi -w -e 's/^ *public *static *void *([A-Za-z0-9]*)/XYZ $1ABC$1 argumentXXX/g' %OUTFILE% 

@ECHO ### Processing Specified List - Adding if block also may take time
FOR /f %%i IN ( %SPECIFIEDFILE%) DO (
	@rem @ECHO %%i
	"%CYGWIN_EXE%\perl.exe" -p -i -e 's/ ^ *%%i;//g if ! /specified/' %OUTFILE%
	"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^ %%iSpecified;/                ifABCargument.%%iXXX { argument.%%iSpecified123=true;}/g if /%%iSpecified/' %OUTFILE%
)
 
@rem replace xyz in "XYZ paymentMaskedType ABCpaymentMaskedType argumentXXX" with public static void
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/XYZ/        public static void/g' %OUTFILE%
@rem replace ABC with (
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/ABC/(/g' %OUTFILE%
@rem replace XXX with )
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/XXX/)/g' %OUTFILE%
@rem remove 123 from "argument.taxExemptSpecified123=true; " 
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/123//g if /Specified/' %OUTFILE%

@rem replacing the string of type "customerPaymentProfileType paymentProfile;" to "customerPaymentProfileType(argument.paymentProfile);"
"%CYGWIN_EXE%\perl.exe" -pi -w -e 's/^([A-Za-z0-9]*) ([A-Za-z0-9]*);/                $1(argument.$2);/g' "%OUTFILE%"

@rem removing string like this "validationModeEnum"
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/^[a-zA-Z]*$//g' %OUTFILE%


@ECHO             }>> %OUTFILE%
@ECHO         }>> %OUTFILE%
@ECHO     } >> %OUTFILE%

@ECHO // ReSharper restore InconsistentNaming >> %OUTFILE%
@ECHO #pragma warning restore 1591 >> %OUTFILE%
@ECHO #pragma warning restore 169 >> %OUTFILE%
@ECHO } >> %OUTFILE%
@ECHO /* >> %OUTFILE%
@ECHO Requests >> %OUTFILE%

@ECHO ARBCreateSubscriptionRequest  >> %OUTFILE%
@ECHO ARBUpdateSubscriptionRequest  >> %OUTFILE%
@ECHO createCustomerPaymentProfileRequest  >> %OUTFILE%
@ECHO createCustomerProfileRequest  >> %OUTFILE%
@ECHO createCustomerProfileTransactionRequest  >> %OUTFILE%
@ECHO createTransactionRequest  >> %OUTFILE%
@ECHO getSettledBatchListRequest  >> %OUTFILE%
@ECHO mobileDeviceRegistrationRequest  >> %OUTFILE%
@ECHO updateCustomerPaymentProfileRequest  >> %OUTFILE%
@ECHO XXDoNotUseDummyRequest  >> %OUTFILE%
@ECHO  >> %OUTFILE%
@ECHO  */ >> %OUTFILE%
@ECHO /* >> %OUTFILE%
@ECHO Objects >> %OUTFILE%
@ECHO   >> %OUTFILE%
@ECHO ARBSubscriptionType  >> %OUTFILE%
@ECHO bankAccountMaskedType  >> %OUTFILE%
@ECHO bankAccountType  >> %OUTFILE%
@ECHO batchDetailsType  >> %OUTFILE%
@ECHO batchStatisticType  >> %OUTFILE%
@ECHO customerDataType  >> %OUTFILE%
@ECHO customerPaymentProfileBaseType >> %OUTFILE% 
@ECHO customerPaymentProfileExType  >> %OUTFILE%
@ECHO customerPaymentProfileMaskedType  >> %OUTFILE%
@ECHO customerPaymentProfileType  >> %OUTFILE%
@ECHO customerProfileMaskedType  >> %OUTFILE%
@ECHO customerProfileType  >> %OUTFILE%
@ECHO customerType  >> %OUTFILE%
@ECHO lineItemType  >> %OUTFILE%
@ECHO mobileDeviceType  >> %OUTFILE%
@ECHO paymentMaskedType  >> %OUTFILE%
@ECHO paymentScheduleType  >> %OUTFILE%
@ECHO paymentSimpleType  >> %OUTFILE%
@ECHO paymentType  >> %OUTFILE%
@ECHO profileTransactionType  >> %OUTFILE%
@ECHO profileTransAmountType  >> %OUTFILE%
@ECHO profileTransAuthCaptureType  >> %OUTFILE%
@ECHO profileTransAuthOnlyType  >> %OUTFILE%
@ECHO profileTransCaptureOnlyType  >> %OUTFILE%
@ECHO profileTransOrderType  >> %OUTFILE%
@ECHO profileTransPriorAuthCaptureType  >> %OUTFILE%
@ECHO profileTransRefundType  >> %OUTFILE%
@ECHO transactionDetailsType  >> %OUTFILE%
@ECHO transactionRequestType  >> %OUTFILE%
@ECHO transactionSummaryType  >> %OUTFILE%
@ECHO  >> %OUTFILE%
@ECHO  */ >> %OUTFILE%

@rem command to remove blank lines in file
"%CYGWIN_EXE%\perl.exe" -i -n -e "print if /\S/" %OUTFILE%
@rem command to replace WWWW to specified
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/WWWW/specified/g' %OUTFILE%
@rem command to replace HTHT### to using System;
"%CYGWIN_EXE%\perl.exe" -p -i -e 's/HTHT###/using System;/g' %OUTFILE%

@rem deleting the .bak file created by perl command
del /S /Q *.bak > NUL
@ECHO The RequestFactoryWithSpecified file is generated @location: "%OUTFILE%"
 
@ECHO ************************************************************************
@ECHO Next Steps: 
@ECHO 1. Compare the generated file with the previous version of file on Github (.org)
@ECHO 2. Run the diff on both the files and apply the differences in RequestFactoryWithSpecified.cs file.
@ECHO 3. Commit the new .org file and RequestFactoryWithSpecified.cs file on Github.
@ECHO ************************************************************************
EXIT /B 0
@REM EOF!

