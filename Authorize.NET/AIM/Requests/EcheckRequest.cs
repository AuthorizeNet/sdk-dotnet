using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AuthorizeNet {
    public class EcheckRequest:GatewayRequest {

        /// <summary>
        /// Creates an ECheck transaction (defaulted to WEB) request for use with the AIM gateway
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckRequest(decimal amount, string bankABACode, string bankAccountNumber,
            BankAccountType acctType, string bankName, string acctName, string bankCheckNumber) :
            this(EcheckType.WEB, amount, bankABACode, bankAccountNumber, acctType, bankName, acctName, bankCheckNumber) { }


        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="type">The Echeck Transaction type: ARC, BOC, CCD, PPD, TEL, WEB</param>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckRequest(EcheckType type, decimal amount, string bankABACode, string bankAccountNumber,
            BankAccountType acctType, string bankName, string acctName, string bankCheckNumber) {

            Queue(ApiFields.Method, "ECHECK");
            this.EcheckType = type;
            this.BankName = bankName;
            this.BankABACode = bankABACode;
            this.BankAccountName = acctName;
            this.BankAccountNumber = bankAccountNumber;
            this.BankAccountType = acctType;
            this.BankCheckNumber = bankCheckNumber;
            this.Amount = amount.ToString(CultureInfo.InvariantCulture);

        }

        /// <summary>
        /// Sets the eCheck request as a recurring payment
        /// </summary>
        /// <returns></returns>
        public EcheckRequest AsRecurring() {
            this.RecurringBilling = "Y";
            return this;
        }

    }

    public class EcheckAuthorizationRequest : EcheckRequest
    {

        /// <summary>
        /// Creates an ECheck transaction (defaulted to WEB) request for use with the AIM gateway
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckAuthorizationRequest(decimal amount, string bankABACode, string bankAccountNumber,
                                          BankAccountType acctType, string bankName, string acctName,
                                          string bankCheckNumber) :
                                              this(
                                              EcheckType.WEB, amount, bankABACode, bankAccountNumber, acctType, bankName,
                                              acctName,
                                              bankCheckNumber)
        {
        }


        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="type">The Echeck Transaction type: ARC, BOC, CCD, PPD, TEL, WEB</param>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckAuthorizationRequest(EcheckType type, decimal amount, string bankABACode, string bankAccountNumber,
                                          BankAccountType acctType, string bankName, string acctName,
                                          string bankCheckNumber) :
                                              base(
                                              type, amount, bankABACode, bankAccountNumber, acctType, bankName, acctName,
                                              bankCheckNumber)
        {
            SetApiAction(RequestAction.Authorize);
        }
    }

    public class EcheckCaptureRequest : EcheckRequest
    {

        /// <summary>
        /// Creates an ECheck transaction (defaulted to WEB) request for use with the AIM gateway
        /// </summary>
        /// <param name="authCode">The auth code.</param>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckCaptureRequest(string authCode, decimal amount, string bankABACode, string bankAccountNumber,
                                    BankAccountType acctType, string bankName, string acctName,
                                    string bankCheckNumber) :
                                        this(authCode,
                                             EcheckType.WEB, amount, bankABACode, bankAccountNumber, acctType, bankName,
                                             acctName,
                                             bankCheckNumber)
        {
        }


        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="authCode">The auth code.</param>
        /// <param name="type">The Echeck Transaction type: ARC, BOC, CCD, PPD, TEL, WEB</param>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckCaptureRequest(string authCode, EcheckType type, decimal amount, string bankABACode,
                                    string bankAccountNumber,
                                    BankAccountType acctType, string bankName, string acctName,
                                    string bankCheckNumber) :
                                        base(
                                        type, amount, bankABACode, bankAccountNumber, acctType, bankName, acctName,
                                        bankCheckNumber)
        {
            this.SetApiAction(RequestAction.Capture);
            this.Queue(ApiFields.AuthorizationCode, authCode);
        }
    }

    public class EcheckPriorAuthCaptureRequest : GatewayRequest
    {
        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="transactionId">The transaction id.</param>
        public EcheckPriorAuthCaptureRequest(string transactionId, decimal amount)
        {
            this.SetApiAction(RequestAction.PriorAuthCapture);
            this.Queue(ApiFields.Method, "ECHECK");
            this.Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
            this.Queue(ApiFields.TransactionID, transactionId);
        }
    }

    public class EcheckCreditRequest : GatewayRequest
    {
        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="transactionId">The transaction id.</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        public EcheckCreditRequest(string transactionId, decimal amount, string bankAccountNumber)
        {
            this.SetApiAction(RequestAction.Credit);
            this.Queue(ApiFields.Method, "ECHECK");
            this.Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
            this.Queue(ApiFields.TransactionID, transactionId);
            this.BankAccountNumber = bankAccountNumber;
        }
    }

    public class EcheckUnlinkedCreditRequest : EcheckRequest
    {
        /// <summary>
        /// Creates an ECheck transaction (defaulted to WEB) request for use with the AIM gateway
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckUnlinkedCreditRequest(decimal amount, string bankABACode, string bankAccountNumber,
                                          BankAccountType acctType, string bankName, string acctName,
                                          string bankCheckNumber) :
                                              this(
                                              EcheckType.WEB, amount, bankABACode, bankAccountNumber, acctType, bankName,
                                              acctName,
                                              bankCheckNumber)
        {
        }


        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="type">The Echeck Transaction type: ARC, BOC, CCD, PPD, TEL, WEB</param>
        /// <param name="amount"></param>
        /// <param name="bankABACode">The valid routing number of the customer’s bank</param>
        /// <param name="bankAccountNumber">The customer’s valid bank account number</param>
        /// <param name="acctType">CHECKING, BUSINESSCHECKING, SAVINGS</param>
        /// <param name="bankName">The name of the bank that holds the customer’s account</param>
        /// <param name="acctName">The name associated with the bank account</param>
        /// <param name="bankCheckNumber">The check number on the customer’s paper check</param>
        public EcheckUnlinkedCreditRequest(EcheckType type, decimal amount, string bankABACode, string bankAccountNumber,
                                          BankAccountType acctType, string bankName, string acctName,
                                          string bankCheckNumber) :
                                              base(
                                              type, amount, bankABACode, bankAccountNumber, acctType, bankName, acctName,
                                              bankCheckNumber)
        {
            SetApiAction(RequestAction.UnlinkedCredit);
        }
    }

    public class EcheckVoidRequest : GatewayRequest
    {
        /// <summary>
        /// Creates an ECheck transaction request for use with the AIM gateway
        /// </summary>
        /// <param name="transactionId">The transaction id.</param>
        public EcheckVoidRequest(string transactionId)
        {
            this.SetApiAction(RequestAction.Void);
            this.Queue(ApiFields.TransactionID, transactionId);
        }
    }
}
