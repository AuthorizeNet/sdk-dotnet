namespace AuthorizeNet.Test
{
    using System;
    using System.Configuration;
    using System.Linq;
    using AuthorizeNet.Util;

    public abstract class UnitTestData {
	    protected static string ApiLoginId ;
	    protected static string TransactionKey ;
	    protected static string CpApiLoginId ;
	    protected static string CpTransactionKey ;
	    protected static string MerchantMd5Key ;

        private static readonly Log Logger = LogFactory.getLog(typeof(UnitTestData));
	
	    /**
	     * Default static constructor
	     */
	    static UnitTestData()
	    {
		    //getPropertyFromNames get the value from properties file or environment
		    ApiLoginId = GetPropertyFromNames(Constants.ENV_API_LOGINID, Constants.PROP_API_LOGINID);
		    TransactionKey = GetPropertyFromNames(Constants.ENV_TRANSACTION_KEY, Constants.PROP_TRANSACTION_KEY);
		    CpApiLoginId = GetPropertyFromNames(Constants.ENV_CP_API_LOGINID, Constants.PROP_CP_API_LOGINID);
		    CpTransactionKey = GetPropertyFromNames(Constants.ENV_CP_TRANSACTION_KEY, Constants.PROP_CP_TRANSACTION_KEY);
		    MerchantMd5Key = GetPropertyFromNames(Constants.ENV_MD5_HASHKEY, Constants.PROP_MD5_HASHKEY);

		    if ((null == ApiLoginId) ||
			    (null == TransactionKey) ||
			    (null == CpApiLoginId) ||
			    (null == CpTransactionKey))
		    {
			    throw new ArgumentException("LoginId and/or TransactionKey have not been set.");
		    }
		    else
		    {
			    AuthorizeNet.Util.LogHelper.info( Logger,
					    "PropertyValues: ApiLoginId:'%s', TransactionKey:'%s', CPApiLoginId:'%s', CPTransactionKey:'%s', MD5Key:'%s' ", 
					    ApiLoginId, TransactionKey, CpApiLoginId, CpTransactionKey, MerchantMd5Key);
		    }
	    }
	
	    public static string GetPropertyFromNames(string pFirstName, string pSecondName)
	    {
		    var value = AuthorizeNet.Environment.GetProperty(pFirstName) ?? 
                            AuthorizeNet.Environment.GetProperty(pSecondName);

	        return value;
	    }
    }
}