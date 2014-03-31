# Authorize.Net .Net SDK

`PM> Install-Package AuthorizeNet`



## Prerequisites

Requires .NET 3.5 or later and Microsoft&reg; Visual Studio 2008 or 2010


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

````
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

````
 public ActionResult Index()
        {

            String checkoutform = SIMFormGenerator.OpenForm(ApiLogin, TransactionKey, 2.25M, "http://developer.authorize.net", true);
            checkoutform = checkoutform+"<input type = \"submit\" class=\"submit\" value = \"Order with SIM!\" />";
            checkoutform = checkoutform+SIMFormGenerator.EndForm();

            return Content("<html>" + checkoutform + "</html>");
        }

````  

### Automated Recurring Billing (ARB)
````
            SubscriptionGateway target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("john@doe.com",
                                                                                  "ARB Subscrition Test 5", (decimal)5.50,
                                                                                  12);
            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 1;
            subscription.CardExpirationYear = 20;



            Address billToAddress = new Address();
            billToAddress.First = "John";
            billToAddress.Last = "Doe";
            subscription.BillingAddress = billToAddress;

            ISubscriptionRequest actual = null;

            try
            {
                actual = target.CreateSubscription(subscription);
            }
            catch (Exception e)
            {
                string s = e.Message;
                Console.WriteLine("Failed to create SUB: "+e.ToString());
            }
            
````
### Customer Information Manager (CIM)
````
            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            try
            {
                actual = target.CreateCustomer("john@doe.com", "new customer profile");
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

````



## Credit Card Test Numbers

For your reference, you can use the following test credit card numbers.
The expiration date must be set to the present date or later. Use 123 for
the CCV code.

American Express::  370000000000002
Discover::  6011000000000012
Visa::  4007000000027
JCB:: 3088000000000017
Diners Club/ Carte Blanche::  38000000000006
Visa (Card Present Track 1):: %B4111111111111111^DOE/JOHN^1803101000000000020000831000000?

