using System.Collections.Generic;

namespace AuthorizeNet
{

	/// <summary>
	/// These are field names and explanations only
	/// </summary>
	public class ApiFields
	{
		public const string CardholderAuthenticationValue = "x_cardholder_authentication_value";
		public const string FooterEmailReceipt = "x_footer_email_receipt";
		public const string EncapChar = "x_encap_char";
		public const string HeaderEmailReceipt = "x_header_email_receipt";
		public const string AuthenticationIndicator = "x_authentication_indicator";

		public const string BankABACode = "x_bank_aba_code";
		public const string BankAcctNum = "x_bank_acct_num";
		public const string BankAcctType = "x_bank_acct_type";
		public const string BankName = "x_bank_name";
		public const string BankAcctName = "x_bank_acct_name";
		public const string EcheckType = "x_echeck_type";
		public const string BankCheckNumber = "x_bank_check_number";


		public const string LineItem = "x_line_item";
		public const string ShipToAddress = "x_ship_to_address";
		public const string ShipToCity = "x_ship_to_city";
		public const string ShipToState = "x_ship_to_state";
		public const string ShipToZip = "x_ship_to_zip";
		public const string ShipToCountry = "x_ship_to_country";
		public const string ShipToCompany = "x_ship_to_company";
		public const string ShipToFirstName = "x_ship_to_first_name";
		public const string ShipToLastName = "x_ship_to_last_name";
		public const string TestRequest = "x_test_request";


		public const string Type = "x_type";
		public const string Version = "x_version";
		/// <summary>
		/// The merchant's unique API Login ID
		/// </summary>
		public const string ApiLogin = "x_login";

		/// <summary>
		/// The merchant's unique Transaction Key
		/// </summary>
		public const string TransactionKey = "x_tran_key";

		/// <summary>
		/// True, False
		/// </summary>
		public const string AllowPartialAuth = "x_allow_partial_Auth";

		/// <summary>
		/// Whether to return the data in delimited fashion
		/// </summary>
		public const string DelimitData = "x_delim_data";

		/// <summary>
		/// If the return from AuthorizeNet is delimited - this is the character to use. Default is pipe
		/// </summary>
		public const string DelimitCharacter = "x_delim_char";


		/// <summary>
		/// The relay response - leave this set as TRUE
		/// </summary>
		public const string RelayResponse = "x_relay_response";

		/// <summary>
		/// The solution ID of your product or integration used for better reporting
		/// </summary>
		public const string SolutionID = "x_solution_id";

		public ApiFields()
		{
			ApiKeys = new List<string>
			{
				"x_login",

				"x_tran_key",

				"x_allow_partial_Auth",

				"x_delim_data",

				"x_delim_char",

				"x_relay_response",

				"x_version",

				"x_type",

				"x_method",

				"x_recurring_billing",

				"x_amount",

				"x_card_num",

				"x_exp_date",

				"x_card_code",

				"x_card_type",

				"x_trans_id",

				"x_split_tender",

				"x_auth_code",

				"x_test_request",

				"x_duplicate_window",

				"x_invoice_num",

				"x_description",

				"x_first_name",

				"x_last_name",

				"x_company",

				"x_address",

				"x_city",

				"x_state",

				"x_zip",

				"x_country",

				"x_phone",

				"x_fax",

				"x_email",

				"x_email_customer",

				"x_merchant_email",

				"x_cust_id",

				"x_customer_ip",

				"x_ship_to_first_name",

				"x_ship_to_last_name",

				"x_ship_to_company",

				"x_ship_to_address",

				"x_ship_to_city",

				"x_ship_to_state",

				"x_ship_to_zip",

				"x_ship_to_country",

				"x_tax",
				"x_freight",
				"x_duty",
				"x_tax_exempt",
				"x_po_num",
				"x_solution_id"
			};


		}

		public List<string> ApiKeys
		{
			get;
			set;
		}



		/// <summary>
		/// Required - The merchant's transaction version
		/// </summary>
		public const string ApiVersion = "x_version";

		/// <summary>
		/// The type of transaction:
		/// AUTH_CAPTURE (default), AUTH_ONLY, CAPTURE_ONLY, CREDIT, PRIOR_AUTH_CAPTURE, VOID
		/// </summary>
		public const string TransactionType = "x_type";

		/// <summary>
		/// CC or ECHECK
		/// </summary>
		public const string Method = "x_method";

		/// <summary>
		/// The recurring billing status
		/// </summary>
		public const string RecurringBilling = "x_recurring_billing";

		/// <summary>
		/// The amount of the transaction
		/// </summary>
		public const string Amount = "x_amount";
		/// <summary>
		/// The credit card number - between 13 and 16 digits without spaces. When x_type=CREDIT, only the last four digits are required
		/// </summary>
		public const string CreditCardNumber = "x_card_num";
		/// <summary>
		/// The expiration date - MMYY, MM/YY, MM-YY, MMYYYY, MM/YYYY, MM-YYYY
		/// </summary>
		public const string CreditCardExpiration = "x_exp_date";
		/// <summary>
		/// The three- or four-digit number on the back of a credit card (on the front for American Express).
		/// </summary>
		public const string CreditCardCode = "x_card_code";
		/// <summary>
		/// The credit card type or echeck in the case of echeck transactions.
		/// </summary>
		public const string CreditCardType = "x_card_type";
		/// <summary>
		/// The payment gateway assigned transaction ID of an original transaction - Required only for CREDIT, PRIOR_ AUTH_ CAPTURE, and VOID transactions
		/// </summary>
		public const string TransactionID = "x_trans_id";
		/// <summary>
		/// The payment gateway-assitned ID assigned when the original transaction includes  two or more partial payments. This is the identifier that is used to group transactions that are part of a split tender order.
		/// </summary>
		public const string SplitTenderId = "x_split_tender_id";
		/// <summary>
		/// The authorization code of an original transaction not authorized on the payment gateway
		/// </summary>
		public const string AuthorizationCode = "x_auth_code";
		/// <summary>
		/// The request to process test transactions
		/// </summary>
		public const string IsTestRequest = "x_test_request";
		/// <summary>
		/// The window of time after the submission of a transaction that a duplicate transaction can not be submitted
		/// </summary>
		public const string DuplicateWindowTime = "x_duplicate_window";

		/// <summary>
		/// The merchant assigned invoice number for the transaction
		/// </summary>
		public const string InvoiceNumber = "x_invoice_num";

		/// <summary>
		/// The transaction description
		/// </summary>
		public const string Description = "x_description";


		public const string FirstName = "x_first_name";

		public const string LastName = "x_last_name";

		public const string Company = "x_company";

		public const string Address = "x_address";

		public const string City = "x_city";

		public const string State = "x_state";

		public const string Zip = "x_zip";

		public const string Country = "x_country";

		public const string Phone = "x_phone";

		public const string Fax = "x_fax";

		public const string Email = "x_email";

		public const string EmailCustomer = "x_email_customer";

		public const string MerchantEmail = "x_merchant_email";

		/// <summary>
		/// The ID of the Customer as relates to your application
		/// </summary>
		public const string CustomerID = "x_cust_id";

		public const string CustomerIPAddress = "x_customer_ip";

		public const string ShipFirstName = "x_ship_to_first_name";

		public const string ShipLastName = "x_ship_to_last_name";

		public const string ShipCompany = "x_ship_to_company";

		public const string ShipAddress = "x_ship_to_address";

		public const string ShipCity = "x_ship_to_city";

		public const string ShipState = "x_ship_to_state";

		public const string ShipZip = "x_ship_to_zip";

		public const string ShipCountry = "x_ship_to_country";


		public const string Tax = "x_tax";
		public const string Freight = "x_freight";
		public const string Duty = "x_duty";
		public const string TaxExempt = "x_tax_exempt";
		public const string PONumber = "x_po_num";



		public bool ApiContainsKey(string key)
		{

			return ApiKeys.Contains(key);
		}



	}
}

