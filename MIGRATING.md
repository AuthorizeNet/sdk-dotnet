# Migrating from Legacy Authorize.Net Classes

Authorize.Net no longer supports several legacy classes, including AIM, ARB and others listed below, as part of sdk-dotnet. If you are using any of these, we recommend that you update your code to use the new Authorize.Net API classes under (sdk-dotnet/Authorize.NET/Api).

**For details on the deprecation and replacement of legacy Authorize.Net APIs, visit https://developer.authorize.net/api/upgrade_guide/.**

## Full list of classes that are no longer supported
| Class                               | New Feature                                                                                                                                                    | Sample Codes directory/repository                                                 |
|-------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------|
| AIM (Authorize.NET/AIM)             | [PaymentTransactions](https://developer.authorize.net/api/reference/index.html#payment-transactions)                                                           | [sample-code-csharp/PaymentTransactions](https://github.com/AuthorizeNet/sample-code-csharp/tree/master/PaymentTransactions)    |
| ARB (Authorize.NET/ARB)             | [RecurringBilling](https://developer.authorize.net/api/reference/index.html#recurring-billing)                                                                 | [sample-code-csharp/Recurring Billing](https://github.com/AuthorizeNet/sample-code-csharp/tree/master/RecurringBilling)          | 
| CIM (Authorize.NET/CIM)             | [CustomerProfiles](https://developer.authorize.net/api/reference/index.html#customer-profiles)                                                                 | [sample-code-csharp/CustomerProfiles](https://github.com/AuthorizeNet/sample-code-csharp/tree/master/CustomerProfiles)          |
| SIM (Authorize.NET/SIM)             | [Accept Hosted](https://developer.authorize.net/content/developer/en_us/api/reference/features/accept_hosted.html)                                             | Not available                                                                                                                         |
| Reporting	(Authorize.NET/Reporting) | [TransactionReporting](https://developer.authorize.net/api/reference/index.html#transaction-reporting)                                                         | [sample-code-csharp/TransactionReporting](https://github.com/AuthorizeNet/sample-code-csharp/tree/master/TransactionReporting) |
| CP (Authorize.NET/CP)             | [PaymentTransactions](https://developer.authorize.net/api/reference/index.html#payment-transactions)                                                           | [sample-code-csharp/PaymentTransactions](https://github.com/AuthorizeNet/sample-code-csharp/tree/master/PaymentTransactions)    |
| DPM (Authorize.NET/DPM)             |[Accept.JS](https://developer.authorize.net/api/reference/features/acceptjs.html)                                                           | [Sample Accept Application](https://github.com/AuthorizeNet/accept-sample-app)    |

## Example 
#### Corresponding new model code (charge-credit-card):
   ```Dotnet
using System;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace net.authorize.sample
{
    public class ChargeCreditCard
    {
        public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, decimal amount)
        {

			// Set the request to operate in either the sandbox or production environment
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

			// define the CreditCard information
            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0828",
                cardCode = "123"
            };

			// define the Billing address
            var billingAddress = new customerAddressType
            {
                firstName = "John",
                lastName = "Doe",
                address = "123 My St",
                city = "OurTown",
                zip = "98004"
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            // Add line Items
            var lineItems = new lineItemType[2];
            lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
            lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };

			// Create the payment transaction object
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    
				// charge the card
                amount = amount,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };
            
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            
            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
			
		}
	}
}		

```
