using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AuthorizeNet {
    public class UnlinkedCredit:GatewayRequest {

        public UnlinkedCredit(decimal amount, string cardNumber, string expirationMonthAndYear) {
            this.SetApiAction(RequestAction.UnlinkedCredit);
            this.Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
            this.Queue(ApiFields.CreditCardNumber, cardNumber);
            this.Queue(ApiFields.CreditCardExpiration, expirationMonthAndYear);
        }
    }
}
