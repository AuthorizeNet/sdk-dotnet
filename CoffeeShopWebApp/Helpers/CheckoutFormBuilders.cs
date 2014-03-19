using AuthorizeNet;
using System.Web;
using System.Collections.Specialized;

namespace CoffeeShopWebApp
{
	public static class CheckoutFormBuilders
	{
        /// <summary>
        /// Creates a CreditCard form which has names which are compliant with the CheckoutFormReaders using the Authorize.NET
        /// API for naming guidelines
        /// </summary>
        public static string CreditCardForm(bool isTest)
        {
            string inputs = "<h2>Payment Information</h2>";
            if (isTest)
            {
                inputs += @"
					<div style = 'border: 1px solid #990000; padding:12px; margin-bottom:24px; background-color:#ffffcc;width:300px'>Test Mode</div>
					<div style = 'float:left;width:250px;'>
						<label>Credit Card Number</label>
						<div id = 'CreditCardNumber'>
							<input type = 'text' size = '28' name = 'x_card_num' value = '4111111111111111' id = 'x_card_num'/>
						</div>
					</div>	
					<div style = 'float:left;width:70px;'>
						<label>Exp.</label>
						<div id = 'CreditCardExpiration'>
							<input type = 'text' size = '5' maxlength = '5' name = 'x_exp_date' value = '0116' id = 'x_exp_date'/>
						</div>
					</div>
					<div style = 'float:left;width:70px;'>
						<label>CCV</label>
						<div id = 'CCV'>
							<input type = 'text' size = '5' maxlength = '5' name = 'x_card_code' id = 'x_card_code' value = '123' />
						</div>
					</div>";
            }
            else
            {
                inputs += @"
					<div style = 'float:left;width:250px;'>
						<label>Credit Card Number</label>
						<div id = 'CreditCardNumber'>
							<input type = 'text' size = '28' name = 'x_card_num' id = 'x_card_num'/>
						</div>
					</div>	
					<div style = 'float:left;width:70px;'>
						<label>Exp.</label>
						<div id = 'CreditCardExpiration'>
							<input type = 'text' size = '5' maxlength = '5' name = 'x_exp_date' id = 'x_exp_date'/>
						</div>
					</div>
					<div style = 'float:left;width:70px;'>
						<label>CCV</label>
						<div id = 'CCV'>
							<input type = 'text' size = '5' maxlength = '5' name = 'x_card_code' id = 'x_card_code' />
						</div>
					</div>";
            }

            return inputs;
        }
	}
}

