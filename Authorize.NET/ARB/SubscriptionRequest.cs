﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet.APICore;

namespace AuthorizeNet {

    public enum BillingIntervalUnits {
        Days,
        Months
    }

    /// <summary>
    /// This is the abstracted SubscriptionRequest class - it provides a simplified way of dealing with the underlying
    /// ARB API. This class uses a Fluent Interface to build out the request - creating only what you need.
    /// </summary>

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For ARB, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling 
    [Obsolete("AuthorizeNetARB is deprecated, use AuthorizeNet::API instead. For ARB, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling.", false)]
    public partial class SubscriptionRequest : ISubscriptionRequest {
        private SubscriptionRequest() {
            this.BillingIntervalUnits = BillingIntervalUnits.Months;
            this.BillingInterval = 1;
            //the default value for no end date
            this.BillingCycles = 9999;
            this.StartsOn = DateTime.Today;
        }

        //Factory
        public static SubscriptionRequest CreateMonthly(string email, string subscriptionName, decimal amount) {
            return CreateMonthly(email, subscriptionName, amount,9999);
        }
        /// <summary>
        /// Creates a monthly subscription request.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subscriptionName">Name of the subscription.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="numberOfBillings">The number of billings. So if you wanted to create a monthly subscription that lasts for a year - this would be 12</param>
        /// <returns></returns>
       public static SubscriptionRequest CreateMonthly(string email, string subscriptionName, decimal amount, short numberOfBillings) {
            var sub = new SubscriptionRequest();
            sub.CustomerEmail = email;
            sub.Amount = amount;
            sub.SubscriptionName = subscriptionName;
            sub.BillingCycles = numberOfBillings;
            return sub;
        }
       public static SubscriptionRequest CreateAnnual(string email, string subscriptionName, decimal amount) {
           return CreateAnnual(email, subscriptionName, amount, 9999);
       }

        /// <summary>
        /// Returns a new subscription request.
        /// </summary>
       /// <returns>SubscriptionRequest object.</returns>
       public static SubscriptionRequest CreateNew()
       {
           return new SubscriptionRequest();
       }

       /// <summary>
       /// Creates an annual subscription.
       /// </summary>
       /// <param name="email">The email.</param>
       /// <param name="subscriptionName">Name of the subscription.</param>
       /// <param name="amount">The amount.</param>
       /// <param name="numberOfBillings">The number of billings. So if you wanted to create a yearly subscription that lasts for a year - this would be 1</param>
       /// <returns></returns>
       public static SubscriptionRequest CreateAnnual(string email, string subscriptionName, decimal amount, short numberOfBillings) {
            var sub = new SubscriptionRequest();
            sub.CustomerEmail = email;
            sub.Amount = amount;
            
            sub.SubscriptionName = subscriptionName;
            sub.BillingCycles = numberOfBillings;           
            sub.BillingInterval = 12;

            return sub;
        }

       /// <summary>
       /// Creates a weekly subscription that bills every 7 days.
       /// </summary>
       /// <param name="email">The email.</param>
       /// <param name="subscriptionName">Name of the subscription.</param>
       /// <param name="amount">The amount.</param>
       /// <returns></returns>
       public static SubscriptionRequest CreateWeekly(string email, string subscriptionName, decimal amount) {
           return CreateWeekly(email, subscriptionName, amount,9999);
       }
       /// <summary>
       /// Creates a weekly subscription that bills every 7 days. 
       /// </summary>
       /// <param name="email">The email.</param>
       /// <param name="subscriptionName">Name of the subscription.</param>
       /// <param name="amount">The amount.</param>
       /// <param name="numberOfBillings">The number of billings. If you want this subscription to last for a month, this should be set to 4</param>
       /// <returns></returns>
       public static SubscriptionRequest CreateWeekly(string email, string subscriptionName, decimal amount, short numberOfBillings) {
            var sub = new SubscriptionRequest();
            sub.CustomerEmail = email;
            sub.Amount = amount;
            
            sub.SubscriptionName = subscriptionName;
            sub.BillingCycles = numberOfBillings;           

            sub.BillingIntervalUnits = BillingIntervalUnits.Days;
            sub.BillingInterval = 7;

            return sub;
        }

        //Fluent bits
       /// <summary>
       /// Adds a credit card payment to the subscription. This is required.
       /// </summary>
       /// <param name="firstName">The first name.</param>
       /// <param name="lastName">The last name.</param>
       /// <param name="cardNumber">The card number.</param>
       /// <param name="cardExpirationYear">The card expiration year.</param>
       /// <param name="cardExpirationMonth">The card expiration month.</param>
       /// <returns></returns>
        public SubscriptionRequest UsingCreditCard(string firstName, string lastName, string cardNumber, int cardExpirationYear, int cardExpirationMonth) {
            this.CardNumber = cardNumber;
            this.CardExpirationYear = cardExpirationYear;
            this.CardExpirationMonth = cardExpirationMonth;

            this.BillingAddress = new Address{
                First = firstName,
                Last = lastName
            };

            return this;
        }

        /// <summary>
        /// Adds a full billing address - which is required for a credit card.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <returns></returns>
        public SubscriptionRequest WithBillingAddress(Address add) {
            this.BillingAddress = add;
            return this;
        }


        /// <summary>
        /// Adds a shipping address to the request.
        /// </summary>
        /// <param name="add">The address to ship to</param>
        /// <returns></returns>
        public SubscriptionRequest WithShippingAddress(Address add) {
            this.ShippingAddress = add;
            return this;
        }

        /// <summary>
        /// Sets a trial period for the subscription. This is part of the overall subscription plan.
        /// </summary>
        /// <param name="trialBillingCycles">The trial billing cycles.</param>
        /// <param name="trialAmount">The trial amount.</param>
        /// <returns></returns>
        public SubscriptionRequest SetTrialPeriod(short trialBillingCycles, decimal trialAmount) {
            this.TrialBillingCycles = trialBillingCycles;
            this.TrialAmount = trialAmount;

            return this;
        }

        public string CustomerEmail { get; set; }
        public string CustomerID { get; set; }
        public string SubscriptionID { get; set; }
        public string SubscriptionName { get; set; }
        public short BillingInterval { get; set; }
        public BillingIntervalUnits BillingIntervalUnits { get; set; }
        public DateTime StartsOn { get; set; }
        public short BillingCycles { get; set; }
        public short TrialBillingCycles { get; set; }
        public decimal Amount { get; set; }
        public decimal TrialAmount { get; set; }

        //CreditCard
        public string CardNumber { get; set; }
        public int CardExpirationYear { get; set; }
        public int CardExpirationMonth { get; set; }
        public string CardCode { get; set; }

        // eCheck
        public BankAccount eCheckBankAccount { get; set; }

        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }

        public string Invoice { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// This is mostly for internal processing needs - it takes the SubscriptionRequest and turns it into something the Gateway can serialize.
        /// </summary>
        /// <returns></returns>
        public ARBSubscriptionType ToAPI(){

            var sub = new ARBSubscriptionType();
            sub.name = this.SubscriptionName;

            bool isCard = true;
            StringBuilder sbError = new StringBuilder("");
            bool bError = false;
            if (String.IsNullOrEmpty(this.CardNumber) || (this.CardNumber.Trim().Length == 0))
            {
                if ((null == this.eCheckBankAccount) || String.IsNullOrEmpty(this.eCheckBankAccount.accountNumber) ||
                    (this.eCheckBankAccount.accountNumber.Trim().Length == 0))
                {
                    sbError.Append("Need a credit card number or a bank account number to set up this subscription");
                    bError = true;
                }
                else
                {
                    isCard = false;
                }
            }

            DateTime dt = new DateTime();
            if ( isCard && !CommonFunctions.ParseDateTime(this.CardExpirationYear, this.CardExpirationMonth, 1, out dt))
            {
                sbError.Append("Need a valid CardExpirationMonth and CardExpirationYear to set up this subscription");
                bError = true;
            }

            if (bError)
            {
                throw new InvalidOperationException(sbError.ToString());
            }

            if (isCard)
            {
                var creditCard = new creditCardType();
                creditCard.cardNumber = this.CardNumber;
                creditCard.expirationDate = dt.ToString("yyyy-MM"); // required format for API is YYYY-MM
                sub.payment = new paymentType();
                sub.payment.Item = creditCard;
            }
            else
            {
                var eCheck = new bankAccountType()
                    {
                        accountTypeSpecified = eCheckBankAccount.accountTypeSpecified,
                        accountType = (bankAccountTypeEnum)Enum.Parse(typeof(bankAccountTypeEnum), eCheckBankAccount.accountType.ToString(), true),
                        routingNumber = eCheckBankAccount.routingNumber,
                        accountNumber = eCheckBankAccount.accountNumber,
                        nameOnAccount = eCheckBankAccount.nameOnAccount,
                        echeckTypeSpecified = eCheckBankAccount.echeckTypeSpecified,
                        echeckType = (echeckTypeEnum)Enum.Parse(typeof(echeckTypeEnum), eCheckBankAccount.echeckType.ToString(), true),
                        bankName = eCheckBankAccount.bankName,
                        checkNumber = eCheckBankAccount.checkNumber
                    };
                sub.payment = new paymentType {Item = eCheck};
            }

            if(this.BillingAddress!=null)
                sub.billTo = this.BillingAddress.ToAPINameAddressType();
            if (this.ShippingAddress != null)
                sub.shipTo = this.ShippingAddress.ToAPINameAddressType();

            sub.paymentSchedule = new paymentScheduleType();
            sub.paymentSchedule.startDate = this.StartsOn;
            sub.paymentSchedule.startDateSpecified = true;

            sub.paymentSchedule.totalOccurrences = this.BillingCycles;
            sub.paymentSchedule.totalOccurrencesSpecified = true;

            // free 1 month trial
            if (this.TrialBillingCycles >= 0) {
                sub.paymentSchedule.trialOccurrences = this.TrialBillingCycles;
                sub.paymentSchedule.trialOccurrencesSpecified = true;
            }

            if (this.TrialAmount >= 0) {
                sub.trialAmount = this.TrialAmount;
                sub.trialAmountSpecified = true;
            }

            sub.amount = this.Amount;
            sub.amountSpecified = true;

            sub.paymentSchedule.interval = new paymentScheduleTypeInterval();
            sub.paymentSchedule.interval.length = this.BillingInterval;
            
            if (this.BillingIntervalUnits == BillingIntervalUnits.Months) {
                sub.paymentSchedule.interval.unit = ARBSubscriptionUnitEnum.months;
            } else {
                sub.paymentSchedule.interval.unit = ARBSubscriptionUnitEnum.days;
            }
            sub.customer = new customerType();
            sub.customer.email = this.CustomerEmail;

            sub.order = new orderType();
            sub.order.description = this.Description;
            sub.order.invoiceNumber = this.Invoice;

            sub.customer.id = this.CustomerID;

            return sub;

        }

        /// <summary>
        /// The Update function won't accept a change to some values - specifically the billing interval. This creates a request
        /// that the API can understand for updates only
        /// </summary>
        /// <returns></returns>
        public ARBSubscriptionType ToUpdateableAPI() {

            var sub = new ARBSubscriptionType();
            sub.name = this.SubscriptionName;

            if (!String.IsNullOrEmpty(this.CardNumber) && (this.CardNumber.Trim().Length > 0))
            {
                DateTime dt;
                if (!CommonFunctions.ParseDateTime(this.CardExpirationYear, this.CardExpirationMonth, 1, out dt))
                {
                    throw new InvalidOperationException("Need a valid CardExpirationMonth and CardExpirationYear to set up this subscription");
                }

                var creditCard = new creditCardType();
                creditCard.cardNumber = this.CardNumber;
                creditCard.expirationDate = dt.ToString("yyyy-MM");//string.Format("{0}-{1}", this.CardExpirationYear, this.CardExpirationMonth);  // required format for API is YYYY-MM
                sub.payment = new paymentType();
                sub.payment.Item = creditCard;
            }

            if ((this.eCheckBankAccount != null) && !String.IsNullOrEmpty(this.eCheckBankAccount.accountNumber) &&
                 (this.eCheckBankAccount.accountNumber.Trim().Length >0))
            {
                var eCheck = new bankAccountType()
                {
                    accountTypeSpecified = eCheckBankAccount.accountTypeSpecified,
                    accountType = (bankAccountTypeEnum)Enum.Parse(typeof(bankAccountTypeEnum), eCheckBankAccount.accountType.ToString(), true),
                    routingNumber = eCheckBankAccount.routingNumber,
                    accountNumber = eCheckBankAccount.accountNumber,
                    nameOnAccount = eCheckBankAccount.nameOnAccount,
                    echeckTypeSpecified = eCheckBankAccount.echeckTypeSpecified,
                    echeckType = (echeckTypeEnum)Enum.Parse(typeof(echeckTypeEnum), eCheckBankAccount.echeckType.ToString(), true),
                    bankName = eCheckBankAccount.bankName,
                    checkNumber = eCheckBankAccount.checkNumber
                };
                sub.payment = new paymentType { Item = eCheck };
            }

            if (this.BillingAddress != null)
                sub.billTo = this.BillingAddress.ToAPINameAddressType();
            if (this.ShippingAddress != null)
                sub.shipTo = this.ShippingAddress.ToAPINameAddressType();

            sub.paymentSchedule = new paymentScheduleType();
            sub.paymentSchedule.totalOccurrences = this.BillingCycles;
            sub.paymentSchedule.totalOccurrencesSpecified = true;

            sub.amount = this.Amount;
            sub.amountSpecified = true;

            sub.customer = new customerType();
            sub.customer.email = this.CustomerEmail;
            sub.customer.id = this.CustomerID;

            sub.order = new orderType();
            sub.order.description = this.Description;
            sub.order.invoiceNumber = this.Invoice;
                                                        
            return sub;

        }

    }
}
