using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet.APICore;

namespace AuthorizeNet {

    /// <summary>
    /// An abstraction for the AuthNET API, allowing you store credit card information with Authorize.net
    /// </summary>
    public class PaymentProfile {

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

        // eCheck
        public BankAccount eCheckBankAccount { get; set; }


        /// <summary>
        /// Creates an API object, ready to send to AuthNET servers.
        /// </summary>
        /// <returns></returns>
        public customerPaymentProfileExType ToAPI() {
            var result = new customerPaymentProfileExType();
            
            if (null != this.BillingAddress)
            { result.billTo = this.BillingAddress.ToAPIType(); }

            result.customerPaymentProfileId = this.ProfileID;
            
            if (!String.IsNullOrEmpty(this.DriversLicenseNumber)) {
                result.driversLicense = new driversLicenseType();
                result.driversLicense.dateOfBirth = this.DriversLicenseDOB;
                result.driversLicense.number = this.DriversLicenseNumber;
                result.driversLicense.state = this.DriversLicenseState;
            }

            if (this.IsBusiness) {
                result.customerType = customerTypeEnum.business;
            } else {
                result.customerType = customerTypeEnum.individual;
            }
            result.customerTypeSpecified = true;
            
            result.payment = new paymentType();
            if (!String.IsNullOrEmpty(this.CardNumber) && (this.CardNumber.Trim().Length > 0))
            {
                var card = new creditCardType();
                card.cardCode = this.CardCode;
                card.cardNumber = this.CardNumber;
                card.expirationDate = this.CardExpiration;
                result.payment.Item = card;
            }
            else if ((this.eCheckBankAccount != null) && !String.IsNullOrEmpty(this.eCheckBankAccount.accountNumber) && (this.eCheckBankAccount.accountNumber.Trim().Length > 0))
            {
                var bankAccount = new bankAccountType()
                    {
                        accountTypeSpecified = this.eCheckBankAccount.accountTypeSpecified,
                        accountType =(bankAccountTypeEnum)Enum.Parse(typeof(bankAccountTypeEnum), this.eCheckBankAccount.accountType.ToString(), true),
                        routingNumber = this.eCheckBankAccount.routingNumber,
                        accountNumber = this.eCheckBankAccount.accountNumber,
                        nameOnAccount = this.eCheckBankAccount.nameOnAccount,
                        echeckTypeSpecified = this.eCheckBankAccount.echeckTypeSpecified,
                        echeckType = (echeckTypeEnum)Enum.Parse(typeof(echeckTypeEnum), this.eCheckBankAccount.echeckType.ToString(), true),
                        bankName = this.eCheckBankAccount.bankName,
                        checkNumber = this.eCheckBankAccount.checkNumber
                    };
                result.payment.Item = bankAccount;
            }

            if (!String.IsNullOrEmpty(this.TaxID)) {
                result.taxId = this.TaxID;
            }
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentProfile"/> class, using the passed-in API type to create the profile.
        /// </summary>
        /// <param name="apiType">Type of the API.</param>
        public PaymentProfile(customerPaymentProfileMaskedType apiType) {
            
            if(apiType.billTo!=null)
                this.BillingAddress = new Address(apiType.billTo);
            
            this.ProfileID = apiType.customerPaymentProfileId;
            
            if (apiType.driversLicense!=null) {
                this.DriversLicenseNumber = apiType.driversLicense.number;
                this.DriversLicenseState = apiType.driversLicense.state;
                this.DriversLicenseDOB = apiType.driversLicense.dateOfBirth;
            }

            if (apiType.customerTypeSpecified) {
                this.IsBusiness = apiType.customerType == customerTypeEnum.business;
            } else {
                this.IsBusiness = false;
            }

            if (apiType.payment != null)
            {
                if (apiType.payment.Item is creditCardMaskedType)
                {
                    var card = (creditCardMaskedType) apiType.payment.Item;
                    this.CardType = card.cardType;
                    this.CardNumber = card.cardNumber;
                    this.CardExpiration = card.expirationDate;
                }
                else if (apiType.payment.Item is bankAccountMaskedType)
                {
                    var bankAcct = (bankAccountMaskedType)apiType.payment.Item;
                    this.eCheckBankAccount = new BankAccount()
                        {
                            accountTypeSpecified = bankAcct.accountTypeSpecified,
                            accountType = (BankAccountType)Enum.Parse(typeof(BankAccountType), bankAcct.accountType.ToString(), true),
                            routingNumber = bankAcct.routingNumber,
                            accountNumber = bankAcct.accountNumber,
                            nameOnAccount = bankAcct.nameOnAccount,
                            echeckTypeSpecified = bankAcct.echeckTypeSpecified,
                            echeckType = (EcheckType)Enum.Parse(typeof(EcheckType), bankAcct.echeckType.ToString(), true),
                            bankName = bankAcct.bankName,
                            checkNumber = ""
                        };
                }
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
