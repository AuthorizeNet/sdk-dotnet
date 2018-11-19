﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace AuthorizeNet {

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For AIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions
    [Obsolete("AuthorizeNetAIM is deprecated, use AuthorizeNet::API instead. For AIM, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions.", false)]
    public class SIMResponse : AuthorizeNet.IGatewayResponse {

        NameValueCollection _post;
        string _merchantHash;
        public SIMResponse(NameValueCollection post) {
            _post = post;
        }

        /// <summary>
        /// Validates that what was passed by Auth.net is valid
        /// </summary>
        public bool Validate(string merchantHash, string apiLogin) {
            return Crypto.IsMatch(merchantHash, apiLogin, this.TransactionID, this.Amount, this.MD5Hash);
        }

        public SIMResponse() : this(HttpContext.Current.Request.Form) { }

        public string MD5Hash {
            get {
                return FindKey("x_MD5_Hash");
            }
        }

        public string ResponseCode {
            get {
                return FindKey("x_response_code");
            }
        }

        public string ResponseReasonCode
        {
            get {
                return FindKey("x_response_reason_code");
            }
        }

        public string Message {
            get {
                return FindKey("x_response_reason_text");
            }
        }

        public bool Approved {
            get {
                return this.ResponseCode == "1";
            }
        }

        public string InvoiceNumber {
            get {
                return FindKey(ApiFields.InvoiceNumber);
            }
        }

        public decimal Amount {
            get {
                var sAmount =  FindKey(ApiFields.Amount);
                decimal result = 0.00M;
                decimal.TryParse(sAmount, out result);
                return result;
            }
        }

        public string TransactionID {
            get {
                return FindKey(ApiFields.TransactionID);
            }
        }

        public string AuthorizationCode {
            get {
                return FindKey(ApiFields.AuthorizationCode);
            }
        }

        public string CardNumber {
            get {
                return FindKey(ApiFields.CreditCardNumber);
            }
        }

        public string CardType
        {
            get { return FindKey(ApiFields.CreditCardType); }
        }

        public string GetValue(string name)
        {
            return FindKey(name);
        }

        string FindKey(string key) {
            string result = null;

            if (_post[key] != null) {
                result = _post[key];
            }

            return result;
        }

        public string GetValueByIndex(int position)
        {
            return ParseResponse(position);
        }

        internal string ParseResponse(int index)
        {
            var result = "";
            if (_post.AllKeys.Count() > index)
            {
                result = _post[index];
            }
            return result;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.AppendFormat("<li>Code = {0}", this.ResponseCode);
            sb.AppendFormat("<li>Auth = {0}", this.AuthorizationCode);
            sb.AppendFormat("<li>Message = {0}", this.Message);
            sb.AppendFormat("<li>TransID = {0}", this.TransactionID);
            sb.AppendFormat("<li>Approved = {0}", this.Approved);
            return sb.ToString();
        }
    }
}
