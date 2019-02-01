using System;
using AuthorizeNet.APICore;

namespace AuthorizeNet {

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For ARB, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling 
    [Obsolete("AuthorizeNetARB is deprecated, use AuthorizeNet::API instead. For ARB, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling.", false)]
    public interface ISubscriptionRequest {
        decimal Amount { get; set; }
        AuthorizeNet.Address BillingAddress { get; set; }
        short BillingCycles { get; set; }
        short BillingInterval { get; set; }
        AuthorizeNet.BillingIntervalUnits BillingIntervalUnits { get; set; }
        string CardCode { get; set; }
        int CardExpirationMonth { get; set; }
        int CardExpirationYear { get; set; }
        string CardNumber { get; set; }
        BankAccount eCheckBankAccount { get; set; }
        string CustomerEmail { get; set; }
        string CustomerID { get; set; }
        AuthorizeNet.ISubscriptionRequest SetTrialPeriod(short trialBillingCycles, decimal trialAmount);
        AuthorizeNet.Address ShippingAddress { get; set; }
        DateTime StartsOn { get; set; }
        string SubscriptionID { get; set; }
        string SubscriptionName { get; set; }
        string Description { get; set; }
        string Invoice { get; set; }
        ARBSubscriptionType ToAPI();
        ARBSubscriptionType ToUpdateableAPI();
        decimal TrialAmount { get; set; }
        short TrialBillingCycles { get; set; }
        AuthorizeNet.ISubscriptionRequest UsingCreditCard(string firstName, string lastName, string cardNumber, int cardExpirationYear, int cardExpirationMonth);
        AuthorizeNet.ISubscriptionRequest UsingPaymentProfile(string customerProfileId, string customerPaymentProfileId, string customerAddressId);
        AuthorizeNet.ISubscriptionRequest WithBillingAddress(AuthorizeNet.Address add);
        AuthorizeNet.ISubscriptionRequest WithShippingAddress(AuthorizeNet.Address add);
    }
}
