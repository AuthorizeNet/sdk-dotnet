# Authorize.Net .Net SDK

`nuget install AuthorizeNet -version 1.8.0`



## Prerequisites



## Installation


## Registration & Configuration

Get a sandbox account at https://developer.authorize.net/sandbox/
To run rspec tests, create a spec/credentials.yml with the following keys and the values obtained as described below.

To get spec/reporting_spec.rb to pass, go to https://sandbox.authorize.net/ under Account tab->Transaction Details API and enable it.


## Usage

### Advanced Merchant Integration (AIM)

````

````

### Direct Post Method (DPM)

A generator is provided to aid in setting up a Direct Post Method application. In the example below +payments+ is the name of the controller to generate.
````

````

After running the generator you will probably want to customize the payment form found in <tt>app/views/payments/payment.erb</tt> and the receipt found in <tt>app/views/payments/receipt.erb</tt>.

There is also a default layout generated, <tt>app/views/layouts/authorize_net.erb</tt>. If you already have your own layout, you can delete that file and the reference to it in the controller (<tt>app/controllers/payments_controller.rb</tt>).

### Server Integration Method (SIM)

A generator is provided to aid in setting up a Server Integration Method application. In the example below +payments+ is the name of the controller to generate.
````

````  
After running the generator you will probably want to customize the payment page found in <tt>app/views/payments/payment.erb</tt> and the thank you page found in <tt>app/views/payments/thank_you.erb</tt>.

You may also want to customize the actual payment form and receipt pages. That can be done by making the necessary changes to the AuthorizeNet::SIM::Transaction object (<tt>@sim_transaction</tt>) found in the +payment+ action in <tt>app/controllers/payments_controller.rb</tt>. The styling of those hosted pages are controlled by the AuthorizeNet::SIM::HostedReceiptPage and AuthorizeNet::SIM::HostedPaymentForm objects (which are passed to the AuthorizeNet::SIM::Transaction).

There is also a default layout generated, <tt>app/views/layouts/authorize_net.erb</tt>. If you already have your own layout, you can delete that file and the reference to it in the controller (<tt>app/controllers/payments_controller.rb</tt>).

### Automated Recurring Billing (ARB)
````
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

