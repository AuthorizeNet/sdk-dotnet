using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet.APICore;

namespace AuthorizeNet
{

    /// <summary>
    /// An abstraction for the AuthNET API, allowing you store credit card and bank account information with Authorize.net
    /// </summary>
    public class PaymentProfile
    {

        public Address BillingAddress { get; set; }
        public string ProfileID { get; set; }
        public bool IsBusiness { get; set; }

        public string DriversLicenseNumber { get; set; }
        public string DriversLicenseDOB { get; set; }
        public string DriversLicenseState { get; set; }

        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public string CardExpiration { get; set; }
        public string CardCode { get; set; }
        public string TaxID { get; set; }

        #region Custom - Not provided by Authorize.net
        public string BankNameOnAccount { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankRoutingNumber { get; set; }
        public bankAccountTypeEnum BankAccountType { get; set; }
        #endregion


        /// <summary>
        /// Creates an API object, ready to send to AuthNET servers.
        /// </summary>
        /// <returns></returns>
        public customerPaymentProfileExType ToAPI()
        {
            var result = new customerPaymentProfileExType();

            if (null != this.BillingAddress)
            { result.billTo = this.BillingAddress.ToAPIType(); }

            result.customerPaymentProfileId = this.ProfileID;

            if (!String.IsNullOrEmpty(this.DriversLicenseNumber))
            {
                result.driversLicense = new driversLicenseType();
                result.driversLicense.dateOfBirth = this.DriversLicenseDOB;
                result.driversLicense.number = this.DriversLicenseNumber;
                result.driversLicense.state = this.DriversLicenseState;
            }

            if (this.IsBusiness)
            {
                result.customerType = customerTypeEnum.business;
            }
            else
            {
                result.customerType = customerTypeEnum.individual;
            }
            result.customerTypeSpecified = true;

            result.payment = new paymentType();
            if (!String.IsNullOrEmpty(this.CardNumber))
            {
                var card = new creditCardType();
                card.cardCode = this.CardCode;
                card.cardNumber = this.CardNumber;
                card.expirationDate = this.CardExpiration;
                result.payment.Item = card;
            }

            #region Added 4/5/2014
            if (!string.IsNullOrEmpty(this.BankAccountNumber))
            {
                bankAccountType new_bank = new bankAccountType();
                new_bank.nameOnAccount = BankNameOnAccount;
                new_bank.accountNumber = BankAccountNumber;
                new_bank.routingNumber = BankRoutingNumber;
                new_bank.accountType = BankAccountType;

                result.payment.Item = new_bank;
            }
            #endregion

            if (!String.IsNullOrEmpty(this.TaxID))
            {
                result.taxId = this.TaxID;
            }
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProfile"/> class, using the passed-in API type to create the profile.
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        public PaymentProfile(customerPaymentProfileMaskedType apiType)
        {

            if (apiType.billTo != null)
                this.BillingAddress = new Address(apiType.billTo);

            this.ProfileID = apiType.customerPaymentProfileId;

            if (apiType.driversLicense != null)
            {
                this.DriversLicenseNumber = apiType.driversLicense.number;
                this.DriversLicenseState = apiType.driversLicense.state;
                this.DriversLicenseDOB = apiType.driversLicense.dateOfBirth;
            }

            if (apiType.customerTypeSpecified)
            {
                this.IsBusiness = apiType.customerType == customerTypeEnum.business;
            }
            else
            {
                this.IsBusiness = false;
            }

            if (apiType.payment != null)
            {
                #region Altered 4/5/2014
                if (apiType.payment.Item is bankAccountMaskedType)
                {
                    var bankAccount = (bankAccountMaskedType)apiType.payment.Item;
                    this.BankNameOnAccount = bankAccount.nameOnAccount;
                    this.BankAccountNumber = bankAccount.accountNumber;
                    this.BankRoutingNumber = bankAccount.routingNumber;
                    this.BankAccountType = bankAccount.accountType;
                }
                else if (apiType.payment.Item is creditCardMaskedType)
                {
                    var card = (creditCardMaskedType)apiType.payment.Item;
                    this.CardType = card.cardType;
                    this.CardNumber = card.cardNumber;
                    this.CardExpiration = card.expirationDate;
                }
                #endregion
            }
            this.TaxID = apiType.taxId;
        }

    }

    public partial class ProfileAmountType
    {
        public string ID { get; set; }
        public ProfileAmountType() { }


    }
}
