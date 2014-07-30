namespace AuthorizeNet 
{
    using System;
    using System.Configuration;
    using System.Linq;

    /**
     *	Determines which environment to post transactions against.
     *  By placing the merchant's payment gateway account in Test Mode in the
     *  Merchant Interface. New payment gateway accounts are placed in Test Mode
     *  by default. For more information about Test Mode, see the Merchant
     *  Integration Guide at http://www.authorize.net/support/merchant/.
     *
     *  When processing test transactions in Test Mode, the payment gateway will
     *  return a transaction ID of "0." This means you cannot test follow-on
     *  transactions, for example, credits, voids, etc., while in Test Mode.
     *  To test follow-on transactions, you can either submit x_test_request=TRUE
     *  as indicated above, or process a test transaction with any valid credit card
     *  number in live mode, as explained below.
     *
     *  Note: Transactions posted against live merchant accounts using either of
     *  the above testing methods are not submitted to financial institutions for
     *  authorization and are not stored in the Merchant Interface.
     *
     *  If testing in the live environment is successful, you are ready to submit
     *  live transactions and verify that they are being submitted successfully.
     *  Either remove the x_test_request field from the transaction request string,
     *  or set it to "FALSE;" or, if you are using Test Mode, turn it off in the
     *  Merchant Interface. To receive a true response, you must submit a
     *  transaction using a real credit card number. You can use any valid credit
     *  card number to submit a test transaction. You can void successful
     *  transactions immediately to prevent live test transactions from being
     *  processed. This can be done quickly on the Unsettled Transactions page of
     *  the Merchant Interface. It is recommended that when testing using a live
     *  credit card, you use a nominal value, such as $0.01. That way, if you forget
     *  to void the transaction, the impact will be minimal. For VISA verification
     *  transactions, submit a $0.00 value instead, if the processor accepts it.
     */
    public class Environment {
        public static readonly Environment SANDBOX = new Environment("https://sandbox.authorize.net", "https://apitest.authorize.net", "https://sandbox.authorize.net");
        public static readonly Environment SANDBOX_TESTMODE = new Environment("https://sandbox.authorize.net", "https://apitest.authorize.net", "https://sandbox.authorize.net");
	    public static readonly Environment PRODUCTION = new Environment("https://secure.authorize.net","https://api.authorize.net","https://cardpresent.authorize.net");
	    public static readonly Environment PRODUCTION_TESTMODE = new Environment("https://secure.authorize.net","https://api.authorize.net","https://cardpresent.authorize.net");
	    public static readonly Environment LOCAL_VM = new Environment("http://WW725RAMITTAL1","http://WW725RAMITTAL1/xml/v1/request.api","http://WW725RAMITTAL1/xml/v1/request.api");
	    public static readonly Environment HOSTED_VM = new Environment("http://WW758AKALGI02.qa.intra","http://WW758AKALGI02.qa.intra/xml/v1/request.api","http://WW758AKALGI02.qa.intra/xml/v1/request.api");
        public static readonly Environment CUSTOM = new Environment(null, null, null);
	    //http://ww725ramittal1.qa.intra/xml/v1/request.api	

	    private String _baseUrl;
	    private String _xmlBaseUrl;
	    private String _cardPresentUrl;

	    private Environment(String baseUrl, String xmlBaseUrl, String cardPresentUrl) {
		    _baseUrl = baseUrl;
		    _xmlBaseUrl = xmlBaseUrl;
		    _cardPresentUrl = cardPresentUrl;
	    }

	    /**
	     * @return the baseUrl
	     */
	    public String getBaseUrl() {
		    return _baseUrl;
	    }

	    /**
	     * @return the xmlBaseUrl
	     */
	    public String getXmlBaseUrl() {
		    return _xmlBaseUrl;
	    }

	    /**
	     * @return the cardPresentUrl
	     */
	    public String getCardPresentUrl() {
		    return _cardPresentUrl;
	    }

	    /**
	     * If a custom environment needs to be supported, this convenience create
	     * method can be used to pass in a custom baseUrl.
	     *
	     * @param baseUrl
	     * @param xmlBaseUrl
	     * @return Environment object
	     */
	    public static Environment createEnvironment(String baseUrl, String xmlBaseUrl) {

		    return createEnvironment( baseUrl, xmlBaseUrl, null);
	    }

	    /**
	     * If a custom environment needs to be supported, this convenience create
	     * method can be used to pass in a custom baseUrl.
	     *
	     * @param baseUrl
	     * @param xmlBaseUrl
	     * @param cardPresentUrl
	     *
	     * @return Environment object
	     */
	    public static Environment createEnvironment(String baseUrl, String xmlBaseUrl, String cardPresentUrl) {
		    var environment = Environment.CUSTOM;
		    environment._baseUrl = baseUrl;
		    environment._xmlBaseUrl = xmlBaseUrl;
		    environment._cardPresentUrl = cardPresentUrl;

		    return environment;
	    }
	
	    /**
	     * Reads a integer value from property file and/or the environment
	     * Values in property file supersede the values set in environment
	     * @param propertyName name of the integer property to read
	     * @return int property value
	     */
	    public static int getIntProperty( String propertyName) 
	    {
	        var stringValue = GetProperty(propertyName);
            var value = (AuthorizeNet.Util.StringUtils.ParseInt(stringValue));
		
		    return value;
	    }

	    /**
	     * Reads a boolean value from property file and/or the environment
	     * Values in property file supersede the values set in environment
	     * @param propertyName name of the boolean property to read
	     * @return boolean property value
	     */
	    public static bool getBooleanProperty( String propertyName) 
	    {
		    var value = false;
		    var stringValue = GetProperty(propertyName);
		    if ( null != stringValue)
		    {
			    Boolean.TryParse(stringValue.Trim(), out value); 
		    }
		
		    return value;
	    }

	    /// <summary>
	    /// Reads the value from property file and/or the environment 
	    /// Values in property file supersede the values set in environmen
	    /// </summary>
        /// <param name="propertyName">propertyName name of the property to read</param>
        /// <returns>String property value</returns>
	    public static String GetProperty(String propertyName) {
		    String stringValue = null;

	        String propValue = null;
            if ( ConfigurationManager.AppSettings.AllKeys.Contains(propertyName))
	        {
	            propValue = ConfigurationManager.AppSettings[propertyName];
	        }

            var envValue = System.Environment.GetEnvironmentVariable(propertyName);
		    if ( null != propValue && propValue.Trim().Length > 0 )
		    {
			    stringValue = propValue;
		    }
		    else if ( null != envValue && envValue.Trim().Length > 0 )
		    {
			    stringValue = envValue;
		    }
		    return stringValue;
	    }
    }
}