# Authorize.Net .NET SDK

[![Travis CI Status](https://travis-ci.org/AuthorizeNet/sdk-dotnet.svg?branch=master)](https://travis-ci.org/AuthorizeNet/sdk-dotnet)
[![Code Climate](https://codeclimate.com/github/AuthorizeNet/sdk-dotnet/badges/gpa.svg)](https://codeclimate.com/github/AuthorizeNet/sdk-dotnet)
[![NuGet](https://badge.fury.io/nu/authorizenet.svg)](https://www.nuget.org/packages/authorizenet)


## Requirements
* .NET 3.5 or later
* Microsoft&reg; Visual Studio 2008 or later
* Nunit 2.6.3;
* An Authorize.Net account (see _Registration & Configuration_ section below)

### Migrating from older versions  
 Since August 2018, the Authorize.Net API has been reorganized to be more merchant focused. AuthorizeNetAIM, AuthorizeNetARB, AuthorizeNetCIM, Reporting and AuthorizeNetSIM classes have all been deprecated in favor of `net\authorize\api`. To see the full list of mapping of new features corresponding to the deprecated features, see [MIGRATING.md](MIGRATING.md). 

### Contribution  
  - If you need information or clarification about Authorize.Net features, create an issue with your question. You can also search the [Authorize.Net developer community](https://community.developer.authorize.net/) for discussions related to your question.  
  - Before creating pull requests, read [the contributors guide](CONTRIBUTING.md). 

### TLS 1.2
The Authorize.Net APIs only support connections using the TLS 1.2 security protocol. Make sure to upgrade all required components to support TLS 1.2. Keep these components up to date to mitigate the risk of new security flaws.


## Installation
To install the AuthorizeNet .NET SDK, run the following command in the Package Manager Console:

`PM> Install-Package AuthorizeNet`

## Registration & Configuration
Use of this SDK and the Authorize.Net APIs requires having an account on the Authorize.Net system. You can find these details in the Settings section.
If you don't currently have a production Authorize.Net account, [sign up for a sandbox account](https://developer.authorize.net/sandbox/).

### Authentication
To authenticate with the Authorize.Net API, use your account's API Login ID and Transaction Key. If you don't have these credentials, obtain them from the Merchant Interface.  For production accounts, the Merchant Interface is located at (https://account.authorize.net/); and for sandbox accounts, at (https://sandbox.authorize.net).

After you have obtained your credentials, load them into the appropriate variables in your code. The below sample code shows how to set the credentials as part of the API request.

#### To set your API credentials for an API request:
```csharp
ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
{
    name = "YOUR_API_LOGIN_ID",
    ItemElementName = ItemChoiceType.transactionKey,
    Item = "YOUR_TRANSACTION_KEY",
};
```

Never include your API Login ID and Transaction Key directly in a file in a publically accessible portion of your website. As a best practice, define the API Login ID and Transaction Key in a constants file, and reference those constants in your code.

### Switching between the sandbox environment and the production environment
Authorize.Net maintains a complete sandbox environment for testing and development purposes. The sandbox environment is an exact replica of our production environment, with simulated transaction authorization and settlement. By default, this SDK is configured to use the sandbox environment. To switch to the production environment, set the appropriate environment constant using ApiOperationBase `RunEnvironment` method.  For example:
```csharp
// For PRODUCTION use
ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
```

API credentials are different for each environment, so be sure to switch to the appropriate credentials when switching environments.


## SDK Usage Examples and Sample Code
When using this SDK, downloading the Authorize.Net sample code repository is recommended.
* [Authorize.Net C# Sample Code Repository (on GitHub)](https://github.com/AuthorizeNet/sample-code-csharp)

The repository contains comprehensive sample code for common uses of the Authorize.Net API.

The API Reference contains details and examples of the structure and formatting of the Authorize.Net API.
* [Developer Center API Reference](http://developer.authorize.net/api/reference/index.html)

Use the examples in the API Reference to determine which methods and information to include in an API request using this SDK.

## Create a Chase Pay Transaction

Use this method to authorize and capture a payment using a tokenized credit card number issued by Chase Pay. Chase Pay transactions are only available to merchants using the Paymentech processor.

The following information is required in the request:
- **payment token**
- **expiration date**
- **cryptogram** received from the token provider
- **tokenRequestorName**
- **tokenRequestorId**
- **tokenRequestorEci**

When using the SDK to submit Chase Pay transactions, consider the following points:
- `tokenRequesterName` must be populated with **`”CHASE_PAY”`**
- `tokenRequestorId` must be populated with the **`Token Requestor ID`** provided by Chase Pay services for each transaction during consumer checkout
- `tokenRequesterEci` must be populated with the **`ECI Indicator`** provided by Chase Pay services for each transaction during consumer checkout  


## Building & Testing the SDK

### Running the SDK Tests
Run the tests against a stub backend by using the USELOCAL run configuration.

Update app.config in the AuthorizeNetTest folder to run all the tests against your sandbox account.

### Testing Guide
For additional help in testing your code, Authorize.Net maintains a [comprehensive testing guide](http://developer.authorize.net/hello_world/testing_guide/) that includes test credit card numbers to use and special triggers to generate certain responses from the sandbox environment.

## Logging Sensitive Data
A new sensitive data logger has been introduced with the Authorize.Net .NET SDK, which is an enhancement on the existing logging framework. 

The logger uses `System.Diagnostics` namespace in .NET Framework. No external libraries need to be installed along with the application to use the logger. 

Enable the logger by providing the following configuration in the `app.config/web.config` files of your application. The log levels supported are `'Verbose','Information','Warning'` and `'Error'`.

If you have previously enabled logging in your application, configurations will need to be updated as below:
```
<configuration>
  <system.diagnostics>
    <sources>
      <source name="AnetDotNetSdkTrace"
              switchName="sourceSwitch"
              switchType="System.Diagnostics.SourceSwitch">
        <listeners>
          <add name="textListener"
              type="AuthorizeNet.Util.SensitiveDataTextLogger, AuthorizeNet"
              initializeData="logfile.log">
          </add>
          <add name="consoleListener"
                        type="AuthorizeNet.Util.SensitiveDataConsoleLogger, AuthorizeNet">
          </add>          
          <remove name="Default" />
        </listeners>
      </source>
    </sources>
    <switches>
      <add name="sourceSwitch" value="Warning"/>
    </switches>
  </system.diagnostics>
</configuration>
```
As of now, two types of listeners, viz. `TextListener` and `ConsoleListener` are supported with the logger. The corresponding sensitive listener types `AuthorizeNet.Util.SensitiveDataTextLogger` and `AuthorizeNet.Util.SensitiveDataConsoleLogger` mask the sensitive data before logging into log file and console respectively.

The sensitive fields that are masked during logging are:
* Card Number
* Card Code
* Expiration Date
* Transaction Key
* Account Number
* Name on Account

There is also a list of regular expressions which the sensitive logger uses to mask credit card numbers while logging. 

More information on the regular expressions used during sensitive data logging [can be found here](https://github.com/AuthorizeNet/sdk-dotnet/blob/master/Authorize.NET/Util/SensitiveDataConfigType.cs).

To unmask sensitive data, use the default `TextWriterTraceListener` and `ConsoleTraceListener`.
```
<configuration>
  <system.diagnostics>
    <sources>
      <source name="AnetDotNetSdkTrace"
              switchName="sourceSwitch"
              switchType="System.Diagnostics.SourceSwitch">
        <listeners>
          <add name="textListener"
              type="System.Diagnostics.TextWriterTraceListener"
              initializeData="logFile.log">
          </add>
          <add name="consoleListener"
                        type="System.Diagnostics.ConsoleTraceListener">
          </add>          
          <remove name="Default" />
        </listeners>
      </source>
    </sources>
    <switches>
      <add name="sourceSwitch" value="Warning"/>
    </switches>
  </system.diagnostics>
</configuration>
```
`AnetDotNetSdkTrace` should be used as the source name, as it is being used by the TraceSource inside logger framework code.

### Transaction Hash Upgrade
Authorize.Net is phasing out the MD5 based `transHash` element in favor of the SHA-512 based `transHashSHA2`. The setting in the Merchant Interface which controlled the MD5 Hash option is no longer available, and the `transHash` element will stop returning values at a later date to be determined. For information on how to use `transHashSHA2`, see the [Transaction Hash Upgrade Guide](https://developer.authorize.net/support/hash_upgrade/).

## License
This repository is distributed under a proprietary license. See the provided [`LICENSE.txt`](/LICENSE.txt) file.
