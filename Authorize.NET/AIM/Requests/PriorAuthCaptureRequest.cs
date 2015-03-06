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
