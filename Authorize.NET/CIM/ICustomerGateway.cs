using System;
namespace AuthorizeNet {

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For CIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/CustomerProfiles
    [Obsolete("AuthorizeNetCIM is deprecated, use AuthorizeNet::API instead. For CIM, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/CustomerProfiles.", false)]
    public interface ICustomerGateway {
        string AddCreditCard(string profileID, string cardNumber, int expirationMonth, int expirationYear, string cardCode, AuthorizeNet.Address billToAddress);
        string AddCreditCard(string profileID, string cardNumber, int expirationMonth, int expirationYear, string cardCode);

        string AddECheckBankAccount(string profileID, BankAccountType bankAccountType, string bankRoutingNumber, string bankAccountNumber, string personNameOnAccount);
        string AddECheckBankAccount(string profileID, BankAccountType bankAccountType, string bankRoutingNumber, string bankAccountNumber, string personNameOnAccount, string bankName, EcheckType eCheckType, Address billToAddress);
        string AddECheckBankAccount(string profileID, BankAccount bankAccount, Address billToAddress);

        string AddShippingAddress(string profileID, AuthorizeNet.Address address);
        string AddShippingAddress(string profileID, string first, string last, string street, string city, string state, string zip, string country, string phone);
        
        IGatewayResponse AuthorizeAndCapture(string profileID, string paymentProfileID, decimal amount);
        IGatewayResponse AuthorizeAndCapture(string profileID, string paymentProfileID, decimal amount, decimal tax, decimal shipping);
        IGatewayResponse AuthorizeAndCapture(AuthorizeNet.Order order);

        IGatewayResponse Authorize(string profileID, string paymentProfileID, decimal amount);
        IGatewayResponse Authorize(string profileID, string paymentProfileID, decimal amount, decimal tax, decimal shipping);
        IGatewayResponse Authorize(AuthorizeNet.Order order);

        IGatewayResponse Capture(string profileID, string paymentProfileId, string cardCode, decimal amount, string approvalCode);

        IGatewayResponse PriorAuthCapture(string profileID, string paymentProfileId, string shippingProfileId, string transId, decimal amount);
        IGatewayResponse PriorAuthCapture(string profileID, string paymentProfileId, string transId, decimal amount);
        IGatewayResponse PriorAuthCapture(string transId, decimal amount);

        [Obsolete("This method has been deprecated, instead use the overloaded method without the appoval code")]
        IGatewayResponse Refund(string profileID, string paymentProfileId, string transactionId, string approvalCode, decimal amount);

        [Obsolete("This method has been deprecated, instead use the overloaded method without the appoval code")]
        IGatewayResponse Void(string profileID, string paymentProfileId, string approvalCode, string transactionId);

        IGatewayResponse Refund(string profileID, string paymentProfileId, string transactionId, decimal amount);
        IGatewayResponse Void(string profileID, string paymentProfileId, string transactionId);


        AuthorizeNet.Customer CreateCustomer(string email, string description);
        bool DeleteCustomer(string profileID);
        bool DeletePaymentProfile(string profileID, string paymentProfileID);
        bool DeleteShippingAddress(string profileID, string shippingAddressID);
        AuthorizeNet.Customer GetCustomer(string profileID);
        string[] GetCustomerIDs();
        AuthorizeNet.Address GetShippingAddress(string profileID, string shippingAddressID);
        bool UpdateCustomer(AuthorizeNet.Customer customer);
        bool UpdatePaymentProfile(string profileID, AuthorizeNet.PaymentProfile profile);
        bool UpdateShippingAddress(string profileID, AuthorizeNet.Address address);
        string ValidateProfile(string profileID, string paymentProfileID, string shippingAddressID, AuthorizeNet.ValidationMode mode);
        string ValidateProfile(string profileID, string paymentProfileID, AuthorizeNet.ValidationMode mode);
    }
}
