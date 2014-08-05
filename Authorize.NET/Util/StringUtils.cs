namespace AuthorizeNet.Util
{
    /// <summary>
    /// 
    /// </summary>
    public class StringUtils {

/*
	    private static Log _logger = LogFactory.getLog(typeof( StringUtils));
*/
	
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="doublestringValue"></param>
	    /// <returns></returns>
	    public static double ParseDouble(string doublestringValue) {
		    double amount = 0.0;
		
		    if ( null != doublestringValue && 0 < doublestringValue.Trim().Length)
		    {
			    double.TryParse(doublestringValue.Trim(), out amount );
		    }
		
		    return amount;
	    }
	
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="intstringValue"></param>
	    /// <returns></returns>
	    public static int ParseInt(string intstringValue) {
		    int amount = 0;
		
		    if ( null != intstringValue && 0 < intstringValue.Trim().Length)
		    {
			    int.TryParse(intstringValue.Trim(), out amount);
		    }
		
		    return amount;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="boolstringValue"></param>
	    /// <returns></returns>
	    public static bool ParseBool(string boolstringValue) {
		    bool result = false;
		
		    if ( null != boolstringValue && 0 < boolstringValue.Trim().Length)
		    {
			    bool.TryParse(boolstringValue.Trim(), out result);
		    }
		
		    return result;
	    }
    }
}