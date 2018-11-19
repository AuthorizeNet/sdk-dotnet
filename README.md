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
 Since August 2018, the Authorize.Net API has been reorganized to be more merchant focused. AuthorizeNetAIM, AuthorizeNetARB, AuthorizeNetCIM, Reporting and AuthorizeNetSIM classes have all been deprecated in favor of `net\authorize\api` . To see the full list of mapping of new features corresponding to the deprecated features, you can see [MIGRATING.md](MIGRATING.md).  

### Contribution  
  - If you need information or clarification about any Authorize.Net features, please create an issue for it. Also you can search in the [Authorize.Net developer community](https://community.developer.authorize.net/).  
  - Before creating pull requests, please read [CONTRIBUTING.md](CONTRIBUTING.md)  

### TLS 1.2
The Authorize.Net APIs only support connections using the TLS 1.2 security protocol. It's important to make sure you have new enough versions of all required components to support TLS 1.2. Additionally, it's very important to keep these components up to date going forward to mitigate the risk of any security flaws that may be discovered in your system or any libraries it uses.


## Installation
To install the AuthorizeNet .NET SDK, run the following command in the Package Manager Console:

`PM> Install-Package AuthorizeNet`


## Registration & Configuration
Use of this SDK and the Authorize.Net APIs requires having an account on our system. You can find these details in the Settings section.
If you don't currently have a production Authorize.Net account and need a sandbox account for testing, you can easily sign up for one [here](https://developer.authorize.net/sandbox/).

### Authentication
To authenticate with the Authorize.Net API you will need to use your account's API Login ID and Transaction Key. If you don't have these values, you can obtain them from our Merchant Interface site. Access the Merchant Interface for production accounts at (https://account.authorize.net/) or sandbox accounts at (https://sandbox.authorize.net).

Once you have your keys simply load them into the appropriate variables in your code, as per the below sample code dealing with the authentication part of the API request.

#### To set your API credentials for an API request:
```csharp
ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
{
    name = "YOUR_API_LOGIN_ID",
    ItemElementName = ItemChoiceType.transactionKey,
    Item = "YOUR_TRANSACTION_KEY",
};
```

You should never include your Login ID and Transaction Key directly in a file that's in a publically accessible portion of your website. A better practice would be to define these in a constants file, and then reference those constants in the appropriate place in your code.

### Switching between the sandbox environment and the production environment
Authorize.Net maintains a complete sandbox environment for testing and development purposes. This sandbox environment is an exact duplicate of our production environment with the transaction authorization and settlement process simulated. By default, this SDK is configured to communicate with the sandbox environment. To switch to the production environment, set the appropriate environment constant using ApiOperationBase `RunEnvironment` method.  For example:
```csharp
// For PRODUCTION use
ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
```

API credentials are different for each environment, so be sure to switch to the appropriate credentials when switching environments.


## SDK Usage Examples and Sample Code
To get started using this SDK, it's highly recommended to download our sample code repository:
* [Authorize.Net C# Sample Code Repository (on GitHub)](https://github.com/AuthorizeNet/sample-code-csharp)

In that respository, we have comprehensive sample code for all common uses of our API:

Additionally, you can find details and examples of how our API is structured in our API Reference Guide:
* [Developer Center API Reference](http://developer.authorize.net/api/reference/index.html)

The API Reference Guide provides examples of what information is needed for a particular request and how that information would be formatted. Using those examples, you can easily determine what methods would be necessary to include that information in a request using this SDK.


## Building & Testing the SDK

### Running the SDK Tests
All the tests can be run against a stub backend using the USELOCAL run configuration.

Get a sandbox account at https://developer.authorize.net/sandbox/
Update app.config in the AuthorizeNetTest folder to run all the tests against your sandbox account

For reporting tests, go to https://sandbox.authorize.net/ under Account tab->Transaction Details API and enable it.

### Testing Guide
For additional help in testing your own code, Authorize.Net maintains a [comprehensive testing guide](http://developer.authorize.net/hello_world/testing_guide/) that includes test credit card numbers to use and special triggers to generate certain responses from the sandbox environment.

## Logging Sensitive Data
A new sensitive data logger has been introduced with the Authorize.Net .NET SDK, which is an enhancement on the existing logging framework. 

The logger uses `System.Diagnostics` namespace in .NET Framework. No external libraries need to be installed along with the application to use the logger. 

The logger can be enabled by providing the following configuration in the `app.config/web.config` files of your application. The log levels supported are `'Verbose','Information','Warning'` and `'Error'`.

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
              type="AuthorizeNet.Util.SensitiveDataTextLogger, AuthorizeNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
              initializeData="logfile.log">
          </add>
          <add name="consoleListener"
                        type="AuthorizeNet.Util.SensitiveDataConsoleLogger, AuthorizeNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null">
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
As of now, two types of listeners, viz. `TextListener` and `ConsoleListener` are supported with the logger. The corresponding listener types `AuthorizeNet.Util.SensitiveDataTextLogger` and `AuthorizeNet.Util.SensitiveDataConsoleLogger` mask the sensitive data before logging into log file and console respectively.

The list of sensitive fields which can be masked during logging are:
* Card Number,
* Card Code,
* Expiration Date,
* Name on Account,
* Transaction Key, and
* Account Number. 

There is a list of regular expressions which the SensitiveFilterLayout uses to mask credit card numbers while logging. 

Further information on the sensitive data logging and regular expressions can be found at this [location](https://github.com/AuthorizeNet/sdk-dotnet/blob/master/Authorize.NET/Util/SensitiveDataConfigType.cs).

If you don't want to mask any sensitive data, you can use the default `TextWriterTraceListener` and `ConsoleTraceListener`.
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


## License
This repository is distributed under a proprietary license. See the provided [`LICENSE.txt`](/LICENSE.txt) file.