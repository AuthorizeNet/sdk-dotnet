namespace AuthorizeNet 
{
    using System;
    using System.Configuration;
    using System.Linq;

    /*================================================================================
    * 
    * Determines the target environment to post transactions.
    *
    * SANDBOX should be used for testing. Transactions submitted to the sandbox 
    * will not result in an actual card payment. Instead, the sandbox simulates 
    * the response. Use the Testing Guide to generate specific gateway responses.
    *
    * PRODUCTION connects to the production gateway environment.
    *
    *===============================================================================*/

    public class Environment {
        public static readonly Environment SANDBOX = new Environment("https://test.authorize.net", "https://apitest.authorize.net", "https://test.authorize.net");
        public static readonly Environment PRODUCTION = new Environment("https://secure2.authorize.net","https://api2.authorize.net","https://cardpresent.authorize.net");
	    public static readonly Environment LOCAL_VM = new Environment(null, null, null);
	    public static readonly Environment HOSTED_VM = new Environment(null, null, null);
        public static Environment CUSTOM = new Environment(null, null, null);

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