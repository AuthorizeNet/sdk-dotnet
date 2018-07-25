using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AuthorizeNet
{
    /// <summary>
    /// A request representing a PriorAuthCapture - the final transfer of funds that happens after an auth.
    /// </summary>

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For AIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions
    [Obsolete("AuthorizeNetAIM is deprecated, use AuthorizeNet::API instead. For AIM, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions.", false)]
    public class PriorAuthCaptureRequest : GatewayRequest
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorAuthCaptureRequest"/> class.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="transactionId">The transaction id.</param>
        public PriorAuthCaptureRequest(decimal amount, string transactionId)
        {
            this.SetApiAction(RequestAction.PriorAuthCapture);
            this.Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
            this.Queue(ApiFields.TransactionID, transactionId);
        }

    }
}
