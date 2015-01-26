﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AuthorizeNet {
    /// <summary>
    /// Capture only function
    /// </summary>
    public class CardPresentCaptureOnly:GatewayRequest {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardPresentCaptureOnly"/> class.
        /// </summary>
        /// <param name="authCode">The auth code.</param>
        /// <param name="cardNumber">The card number.</param>
        /// <param name="expirationMonthAndYear">The expiration month and year.</param>
        /// <param name="amount">The amount.</param>
        public CardPresentCaptureOnly(string authCode, string cardNumber, string expirationMonthAndYear, decimal amount) {
            this.SetApiAction(RequestAction.Capture);
            this.Queue(ApiFields.AuthorizationCode, authCode);
            this.Queue(ApiFields.CreditCardNumber, cardNumber);
            this.Queue(ApiFields.CreditCardExpiration, expirationMonthAndYear);
            this.Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
        }
    }
}
