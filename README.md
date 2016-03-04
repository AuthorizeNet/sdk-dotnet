# Authorize.Net .Net SDK

[![Travis](https://img.shields.io/travis/AuthorizeNet/sdk-dotnet/master.svg)]
(https://travis-ci.org/AuthorizeNet/sdk-dotnet)
[![Code Climate](https://codeclimate.com/github/AuthorizeNet/sdk-dotnet/badges/gpa.svg)](https://codeclimate.com/github/AuthorizeNet/sdk-dotnet)
[![NuGet](https://badge.fury.io/nu/authorizenet.svg)](https://www.nuget.org/packages/authorizenet)

`PM> Install-Package AuthorizeNet`



## Prerequisites

Requires .NET 3.5 or later and Microsoft&reg; Visual Studio 2008 or 2010; Nunit 2.6.3;


## Installation
To install AuthorizeNet, run the following command in the Package Manager Console:

`PM> Install-Package AuthorizeNet`


## Usage
Apart from this README, you can find details and examples of using the SDK in the following places:  

- [Github Sample Code Repository](https://github.com/AuthorizeNet/sample-code-csharp)
- [Developer Center Reference](http://developer.authorize.net/api/reference/index.html)  

### Charging a Credit Card
````csharp
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX; 
            
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0718"
            };

            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = 133.45m,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            var controller = new createTransactionController(request);
            controller.Execute();

            var response = controller.GetApiResponse();
            
            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }
````

### Setting the Production Environment
Set the appropriate environment constant using the ApiOperationBase RunEnvironment.  For example, in the method above, to switch to production environment use:
```csharp
ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
```  

### Direct Post Method (DPM)

Direct Post Method allows developers to fully customize the experience of the entire payment flow, while simplifying PCI compliance.

See http://developer.authorize.net/integration/fifteenminutes/csharp/ for a quickstart guide to using the DPM method


### Server Integration Method (SIM)

SIM provides a customizable, secure hosted payment form to make integration easy for Web merchants that do not have an SSL certificate.

Place the following code in the default action of a simple MVC application to display a SIM payment button.

````csharp
 public ActionResult Index()
        {

            String checkoutform = SIMFormGenerator.OpenForm(ApiLogin, TransactionKey, 2.25M, "", true);
            checkoutform = checkoutform+"<input type = \"submit\" class=\"submit\" value = \"Order with SIM!\" />";
            checkoutform = checkoutform+SIMFormGenerator.EndForm();

            return Content("<html>" + checkoutform + "</html>");
        }

````  


## Running the SDK Tests

All the tests can be run against a stub backend using the USELOCAL run configuration.

Get a sandbox account at https://developer.authorize.net/sandbox/
Update app.config in the AuthorizeNetTest folder to run all the tests against your sandbox account

For reporting tests, go to https://sandbox.authorize.net/ under Account tab->Transaction Details API and enable it.


## Credit Card Test Numbers

For your reference, you can use the following test credit card numbers.
The expiration date must be set to the present date or later. Use 123 for
the CCV code.

| Card                        | Test Numbers                                                 |
| :-------------------------- | :----------------------------------------------------------- |
| American Express            | 370000000000002                                              |
| Discover                    | 6011000000000012                                             |
| Visa                        | 4007000000027                                                |
| MasterCard                  | 5555555555554444                                             |
| JCB                         | 3088000000000017                                             |
| Diners Club/ Carte Blanche  | 38000000000006                                               |
| Visa (Card Present Track 1) | %B4111111111111111^DOE/JOHN^1803101000000000020000831000000? |


