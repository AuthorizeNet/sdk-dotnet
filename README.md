# Authorize.Net .Net SDK

[![Build Status](https://travis-ci.org/AuthorizeNet/sdk-dotnet.png?branch=master)]
(https://travis-ci.org/AuthorizeNet/sdk-dotnet)

`PM> Install-Package AuthorizeNet`



## Prerequisites

Requires .NET 3.5 or later and Microsoft&reg; Visual Studio 2008 or 2010; Nunit 2.6.3;


## Installation
To install AuthorizeNet, run the following command in the Package Manager Console:

`PM> Install-Package AuthorizeNet`


## Registration & Configuration

All the tests can be run against a stub backend using the USELOCAL run configuration.

Get a sandbox account at https://developer.authorize.net/sandbox/
Update app.config in the AuthorizeNetTest folder to run all the tests against your sandbox account

For reporting tests, go to https://sandbox.authorize.net/ under Account tab->Transaction Details API and enable it.


## Usage

### Advanced Merchant Integration (AIM)

````csharp
            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new AuthorizationRequest("5424000000000015", "0224", (decimal)20.10, "AuthCap transaction approved testing", true);
            string description = "AuthCap transaction approved testing";
            IGatewayResponse actual = target.Send(request, description);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
````

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

### Automated Recurring Billing (ARB)
````csharp
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

            interval.length = 1;   
            interval.unit = ARBSubscriptionUnitEnum.months;

            paymentScheduleType schedule = new paymentScheduleType
            {
                interval = interval,
                startDate = DateTime.Now.AddDays(1),      
                totalOccurrences = 9999,      // 9999 indicates no end date
                trialOccurrences = 3
            };

            #region Payment Information
            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0718"
            };

            paymentType cc = new paymentType { Item = creditCard };
            #endregion

            nameAndAddressType addressInfo = new nameAndAddressType()
            {
                firstName = "John",
                lastName = "Doe"
            };

            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
            {
                amount = 35.55m,
                trialAmount = 0.00m,
                paymentSchedule = schedule,
                billTo = addressInfo,
                payment = cc
            };

            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };

            var controller = new ARBCreateSubscriptionController(request);  
            controller.Execute();

            ARBCreateSubscriptionResponse response = controller.GetApiResponse();  

            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response != null && response.messages.message != null)
                {
                    Console.WriteLine("Success, Subscription ID : " + response.subscriptionId.ToString());
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }
            
````
### Customer Information Manager (CIM)
````csharp
ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            customerProfileType customerProfile = new customerProfileType();
            customerProfile.merchantCustomerId = "TestCustomerID";
            customerProfile.email = "john@doe.com";

            var request = new createCustomerProfileRequest { profile = customerProfile, validationMode = validationModeEnum.none};

            var controller = new createCustomerProfileController(request);          
            controller.Execute();

            createCustomerProfileResponse response = controller.GetApiResponse(); 

            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response != null && response.messages.message != null)
                {
                    Console.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
            }

````



## Credit Card Test Numbers

For your reference, you can use the following test credit card numbers.
The expiration date must be set to the present date or later. Use 123 for
the CCV code.

| Card                        | Test Numbers                                                 |
| :-------------------------- | :----------------------------------------------------------- |
| American Express            | 370000000000002                                              |
| Discover                    | 6011000000000012                                             |
| Visa                        | 4007000000027                                                |
| JCB                         | 3088000000000017                                             |
| Diners Club/ Carte Blanche  | 38000000000006                                               |
| Visa (Card Present Track 1) | %B4111111111111111^DOE/JOHN^1803101000000000020000831000000? |

## New Model

We’re exploring a new model of maintaining the SDKs which allows us to be more responsive to API changes.  This model is consistent across the different SDK languages, which is great for us, however we do not want to sacrifice your productivity by losing the inherent efficiencies in the existing object model or the specific languages.  We’re introducing the new model as "supplementary" at this time and we would appreciate your feedback.  Let us know what you really think!  Here’s an example of a server side call with ApplePay data in the new model.

### Apple Pay Example
````csharp
        static void Main(string[] args)
        {
            merchantAuthenticationType CustomMerchantAuthenticationType = new merchantAuthenticationType
            {
                name = "5KP3u95bQpv",
                ItemElementName = ItemChoiceType.transactionKey,
                Item = "4Ktq966gC55GAX7S",
            };

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            //create a transaction
            var transactionRequestType = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                //amount = SetValidTransactionAmount(Counter),
                amount = 15,
                payment = new paymentType { Item = new opaqueDataType { dataDescriptor = "COMMON.APPLE.INAPP.PAYMENT", dataValue = "eyJkYXRhIjoiQkRQTldTdE1tR2V3UVVXR2c0bzdFXC9qKzFjcTFUNzhxeVU4NGI2N2l0amNZSTh3UFlBT2hzaGpoWlBycWRVcjRYd1BNYmo0emNHTWR5KysxSDJWa1BPWStCT01GMjV1YjE5Y1g0bkN2a1hVVU9UakRsbEIxVGdTcjhKSFp4Z3A5ckNnc1NVZ2JCZ0tmNjBYS3V0WGY2YWpcL284WkliS25yS1E4U2gwb3VMQUtsb1VNbit2UHU0K0E3V0tycXJhdXo5SnZPUXA2dmhJcStIS2pVY1VOQ0lUUHlGaG1PRXRxK0grdzB2UmExQ0U2V2hGQk5uQ0hxenpXS2NrQlwvMG5xTFpSVFliRjBwK3Z5QmlWYVdIZWdoRVJmSHhSdGJ6cGVjelJQUHVGc2ZwSFZzNDhvUExDXC9rXC8xTU5kNDdrelwvcEhEY1JcL0R5NmFVTStsTmZvaWx5XC9RSk4rdFMzbTBIZk90SVNBUHFPbVhlbXZyNnhKQ2pDWmxDdXcwQzltWHpcL29iSHBvZnVJRVM4cjljcUdHc1VBUERwdzdnNjQybTRQendLRitIQnVZVW5lV0RCTlNEMnU2amJBRzMiLCJ2ZXJzaW9uIjoiRUNfdjEiLCJoZWFkZXIiOnsiYXBwbGljYXRpb25EYXRhIjoiOTRlZTA1OTMzNWU1ODdlNTAxY2M0YmY5MDYxM2UwODE0ZjAwYTdiMDhiYzdjNjQ4ZmQ4NjVhMmFmNmEyMmNjMiIsInRyYW5zYWN0aW9uSWQiOiJjMWNhZjVhZTcyZjAwMzlhODJiYWQ5MmI4MjgzNjM3MzRmODViZjJmOWNhZGYxOTNkMWJhZDlkZGNiNjBhNzk1IiwiZXBoZW1lcmFsUHVibGljS2V5IjoiTUlJQlN6Q0NBUU1HQnlxR1NNNDlBZ0V3Z2ZjQ0FRRXdMQVlIS29aSXpqMEJBUUloQVBcL1wvXC9cLzhBQUFBQkFBQUFBQUFBQUFBQUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL01Gc0VJUFwvXC9cL1wvOEFBQUFCQUFBQUFBQUFBQUFBQUFBQVwvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cLzhCQ0JheGpYWXFqcVQ1N1BydlZWMm1JYThaUjBHc014VHNQWTd6ancrSjlKZ1N3TVZBTVNkTmdpRzV3U1RhbVo0NFJPZEpyZUJuMzZRQkVFRWF4ZlI4dUVzUWtmNHZPYmxZNlJBOG5jRGZZRXQ2ek9nOUtFNVJkaVl3cFpQNDBMaVwvaHBcL200N242MHA4RDU0V0s4NHpWMnN4WHM3THRrQm9ONzlSOVFJaEFQXC9cL1wvXC84QUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cLys4NXZxdHB4ZWVoUE81eXNMOFl5VlJBZ0VCQTBJQUJHbStnc2wwUFpGVFwva0RkVVNreHd5Zm84SnB3VFFRekJtOWxKSm5tVGw0REdVdkFENEdzZUdqXC9wc2hCWjBLM1RldXFEdFwvdERMYkUrOFwvbTB5Q21veHc9IiwicHVibGljS2V5SGFzaCI6IlwvYmI5Q05DMzZ1QmhlSEZQYm1vaEI3T28xT3NYMkora0pxdjQ4ek9WVmlRPSJ9LCJzaWduYXR1cmUiOiJNSUlEUWdZSktvWklodmNOQVFjQ29JSURNekNDQXk4Q0FRRXhDekFKQmdVckRnTUNHZ1VBTUFzR0NTcUdTSWIzRFFFSEFhQ0NBaXN3Z2dJbk1JSUJsS0FEQWdFQ0FoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQXdKekVsTUNNR0ExVUVBeDRjQUdNQWFBQnRBR0VBYVFCQUFIWUFhUUJ6QUdFQUxnQmpBRzhBYlRBZUZ3MHhOREF4TURFd05qQXdNREJhRncweU5EQXhNREV3TmpBd01EQmFNQ2N4SlRBakJnTlZCQU1lSEFCakFHZ0FiUUJoQUdrQVFBQjJBR2tBY3dCaEFDNEFZd0J2QUcwd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFOQzgra2d0Z212V0YxT3pqZ0ROcmpURUJSdW9cLzVNS3ZsTTE0NnBBZjdHeDQxYmxFOXc0ZklYSkFEN0ZmTzdRS2pJWFlOdDM5ckx5eTd4RHdiXC81SWtaTTYwVFoyaUkxcGo1NVVjOGZkNGZ6T3BrM2Z0WmFRR1hOTFlwdEcxZDlWN0lTODJPdXA5TU1vMUJQVnJYVFBITmNzTTk5RVBVblBxZGJlR2M4N20wckFnTUJBQUdqWERCYU1GZ0dBMVVkQVFSUk1FK0FFSFpXUHJXdEpkN1laNDMxaENnN1lGU2hLVEFuTVNVd0l3WURWUVFESGh3QVl3Qm9BRzBBWVFCcEFFQUFkZ0JwQUhNQVlRQXVBR01BYndCdGdoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQURnWUVBYlVLWUNrdUlLUzlRUTJtRmNNWVJFSW0ybCtYZzhcL0pYditHQlZRSmtPS29zY1k0aU5ERkFcL2JRbG9nZjlMTFU4NFRId05SbnN2VjNQcnY3UlRZODFncTBkdEM4elljQWFBa0NISUkzeXFNbko0QU91NkVPVzlrSmsyMzJnU0U3V2xDdEhiZkxTS2Z1U2dRWDhLWFFZdVpMazJScjYzTjhBcFhzWHdCTDNjSjB4Z2VBd2dkMENBUUV3T3pBbk1TVXdJd1lEVlFRREhod0FZd0JvQUcwQVlRQnBBRUFBZGdCcEFITUFZUUF1QUdNQWJ3QnRBaEJjbCtQZjMrVTRwazEzblZEOW53UVFNQWtHQlNzT0F3SWFCUUF3RFFZSktvWklodmNOQVFFQkJRQUVnWUJhSzNFbE9zdGJIOFdvb3NlREFCZitKZ1wvMTI5SmNJYXdtN2M2VnhuN1phc05iQXEzdEF0OFB0eSt1UUNnc3NYcVprTEE3a3oyR3pNb2xOdHY5d1ltdTlVandhcjFQSFlTK0JcL29Hbm96NTkxd2phZ1hXUnowbk1vNXkzTzFLelgwZDhDUkhBVmE4OFNyVjFhNUpJaVJldjNvU3RJcXd2NXh1WmxkYWc2VHI4dz09In0=" } }

            };

            var createRequest = new createTransactionRequest
            {
                refId = "2345",
                transactionRequest = transactionRequestType,
            };

            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();

            if (createResponse != null)
            {
                var tResponse = createResponse.transactionResponse;

                if ((tResponse!=null)&&(tResponse.responseCode=="1"))
                {
                    Console.WriteLine("AUTH CODE : " + tResponse.authCode);
                    Console.WriteLine("TRANS ID  : " + tResponse.transId);
                    Console.ReadLine();
                }
            }
        }
````
