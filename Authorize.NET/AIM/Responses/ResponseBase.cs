using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizeNet {

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For AIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions
    [Obsolete("AuthorizeNetAIM is deprecated, use AuthorizeNet::API instead. For AIM, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions.", false)]
    public abstract class ResponseBase {
        public string[] RawResponse;
        private Dictionary<int, string> _apiKeyDict = new Dictionary<int, string>();

        internal Dictionary<int, string> APIResponseKeys {
            get { return _apiKeyDict; }
        }

        internal int ParseInt(int index) {
            int result = 0;
            if (RawResponse.Length > index)
                int.TryParse(RawResponse[index].ToString(), out result);
            return result;
        }

        internal decimal ParseDecimal(int index) {
            decimal result = 0;
            if (RawResponse.Length > index)
                decimal.TryParse(RawResponse[index].ToString(), out result);
            return result;
        }

        internal string ParseResponse(int index) {
            var result = "";
            if (RawResponse.Length > index) {
                result = RawResponse[index].ToString();
            }
            return result;
        }
        public int FindByValue(string val) {
            int result = 0;
            for (int i = 0; i < RawResponse.Length; i++) {
                if (RawResponse[i].ToString() == val) {
                    result = i;
                    break;
                }
            }
            return result;
        }
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            foreach (var key in APIResponseKeys.Keys) {
                sb.AppendFormat("{0} = {1}\n", APIResponseKeys[key], ParseResponse(index));
                index++;
            }
            return sb.ToString();
        }

    }
}
