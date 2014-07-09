namespace authorizenet.test
{
    using System;
    using System.Net;
    using authorizenet;
    //using authorizenet.data.creditcard.AVSCode;
    //using authorizenet.data.creditcard.CardType;
    //using authorizenet.data.echeck.BankAccountType;
    //using authorizenet.data.echeck.ECheckType;
    using authorizenet.data;
    using authorizenet.util;

    public abstract class UnitTestData {
	    protected static string apiLoginID ;
	    protected static string transactionKey ;
	    protected static string cp_apiLoginID ;
	    protected static string cp_transactionKey ;
	    protected static string merchantMD5Key ;
	    protected static Merchant merchant = null;
	
	    // customer information
	    protected const string firstName = "John";
	    protected const string lastName = "Doe";
	    protected const string address = "123 Any Street";
	    protected const string city = "Any City";
	    protected const string state = "CA";
	    protected const string zipPostalCode = "94114";
	    protected const string magicSplitTenderZipCode = "46225";
	    protected const string company = "John Doe Innovations";
	    protected const string country = "US";
	    protected const string customerId = "CUST000000";
	    protected const string customerIP = "127.0.0.1";
	    protected const string email = "customer@merchant.com";
	    protected const string email2 = "customer2@merchant.com";
	    protected const string phone = "415-555-1212";
	    protected const string fax = "415-555-1313";
	    protected const string customerDescription = "Customer A";
	    protected const string customerDescription2 = "Customer B";

	    // email receipt information
	    protected const string headerEmailReceipt = "Thank you for purchasing " +
			    "Widgets from The Antibes Company";
	    protected const string footerEmailReceipt = "If you have any problems, " +
			    "please contact us at +44 20 5555 1212";
	    protected const string merchantEmail = "merchant@merchant.com";

	    // order information
	    protected const string orderDescription = "Widgets";

	    protected string InvoiceNumber = (DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond).ToString();
	    protected const string MdfKey = "notes";
	    protected const string mdfValue = "Would like a blue widget.";

	    // order item information
	    protected const string itemDescription = "A widget for widgeting.";
	    protected const string itemId = "widget_a_1000";
	    protected const string itemName = "Widget A";
	    protected const decimal itemPrice = 19.99m;
	    protected const decimal itemQuantity = 1.00m;
	    protected const string itemDescription2 = "A FREE micro widget.";
	    protected const string itemId2 = "mwidget_z_0001";
	    protected const string itemName2 = "Micro Widget Z";
	    protected const decimal itemPrice2 = 0.01m;
	    protected const decimal itemQuantity2 = 1.00m;

	    // shipping address information is the same as the customer address

	    // shipping charges information
	    protected const decimal dutyAmount = 0.00m;
	    protected const string dutyItemDescription = "VAT";
	    protected const string dutyItemName = "VAT Tax";
	    protected const decimal freightAmount = 5.00m;
	    protected const string freightDescription = "Flat rate";
	    protected const string freightItemName = "Shipping";
	    protected const string purchaseOrderNumber = "PO-1001";
	    protected const decimal taxAmount = 2.37m;
	    protected const string taxDescription = "9.5%";
	    protected const bool taxExempt = false;
	    protected const string taxItemName = "California Tax";

	    // credit card information
	    protected const string creditCardNumber = "4111-1111-1111-1111";
	    protected const string rawCreditCardNumber = "4111111111111111";
	    protected const string maskedCreditCardNumber = "xxxx1111";
	    protected const CardType cardType = CardType.VISA;
	    protected const string creditCardExpMonth = "12";
	    protected const string creditCardExpYear = "2020";
	    protected const AvsCode avsCode = AvsCode.P;
	    protected const string cardCodeVerification = "P";
	    protected const string cardholderAuthenticationIndicator = "5";
	    protected const string cardholderAuthenticationValue = "123";

	    // eCheck information
	    protected const string bankAccountName = "Test Checking";
	    protected const string bankAccountNumber = "1234567890";
	    protected const BankAccountType bankAccountType = BankAccountType.CHECKING;
	    protected const string bankCheckNumber = "1001";
	    protected const string bankName = "Bank of America";
	    protected const ECheckType eCheckType = ECheckType.WEB;
	    protected const string routingNumber = "111000025";

	    // transaction information
	    protected const decimal totalAmount = 27.36m;

	    protected const string reportingBatchId = "814302";
	    protected const string reportingTransId = "2156009012";

        private static Log logger = LogFactory.getLog(typeof(UnitTestData));
	
	    //static URL url = null;

	    static string[] propertiesList = {
		    Constants.HTTP_USE_PROXY,
		    Constants.HTTP_PROXY_HOST,
		    Constants.HTTP_PROXY_PORT,
		    Constants.HTTPS_USE_PROXY,
		    Constants.HTTPS_PROXY_HOST,
		    Constants.HTTPS_PROXY_PORT,
		    /*
		    not needed http/https			
		    ".nonProxyHosts",
  		    ".proxyPassword",
		    ".proxyUser",
		    "_proxy",
		    */
		    };

	
	    /**
	     * Default static constructor
	     * Try to initialize proxy, if necessary, from environment variables
	     * to open connection to Internet
	     */
	    //protected UnitTestData()
	    static UnitTestData()
	    {
		    //getPropertyFromNames get the value from properties file or environment
		    apiLoginID = GetPropertyFromNames(Constants.ENV_API_LOGINID, Constants.PROP_API_LOGINID);
		    transactionKey = GetPropertyFromNames(Constants.ENV_TRANSACTION_KEY, Constants.PROP_TRANSACTION_KEY);
		    cp_apiLoginID = GetPropertyFromNames(Constants.ENV_CP_API_LOGINID, Constants.PROP_CP_API_LOGINID);
		    cp_transactionKey = GetPropertyFromNames(Constants.ENV_CP_TRANSACTION_KEY, Constants.PROP_CP_TRANSACTION_KEY);
		    merchantMD5Key = GetPropertyFromNames(Constants.ENV_MD5_HASHKEY, Constants.PROP_MD5_HASHKEY);

		    if ((null == apiLoginID) ||
			    (null == transactionKey) ||
			    (null == cp_apiLoginID) ||
			    (null == cp_transactionKey))
		    {
			    throw new ArgumentException("LoginId and/or TransactionKey have not been set.");
		    }
		    else
		    {
			    authorizenet.util.LogHelper.info( logger,
					    "PropertyValues: ApiLoginId:'%s', TransactionKey:'%s', CPApiLoginId:'%s', CPTransactionKey:'%s', MD5Key:'%s' ", 
					    apiLoginID, transactionKey, cp_apiLoginID, cp_transactionKey, merchantMD5Key);
			    merchant = Merchant.CreateMerchant( authorizenet.Environment.SANDBOX, apiLoginID, transactionKey);
		    }
	    }

	    protected static string[] GetPropertiesList()
	    {
		    return propertiesList;
	    }
	
	    public static string GetPropertyFromNames(string pFirstName, string pSecondName) {
		    var value = authorizenet.Environment.getProperty(pFirstName) ?? 
                            authorizenet.Environment.getProperty(pSecondName);

	        return value;
	    }
    }
}