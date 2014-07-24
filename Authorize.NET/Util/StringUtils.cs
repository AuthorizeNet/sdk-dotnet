namespace AuthorizeNet.Util
{
    
    using System.Text;

    public class StringUtils {

	    private static Log logger = LogFactory.getLog(typeof( StringUtils));

	    /**
	     * Sanitize strings for output
	     */
        /*
	    public static string sanitizestring(string string) {

		    var retval = new StringBuilder();
		    java.text.stringCharacterIterator iterator = new java.text.stringCharacterIterator(
				    string);
		    char character = iterator.current();

		    while (character != java.text.CharacterIterator.DONE) {
			    if (character == '<') {
				    retval.append("&lt;");
			    } else if (character == '>') {
				    retval.append("&gt;");
			    } else if (character == '&') {
				    retval.append("&amp;");
			    } else if (character == '\"') {
				    retval.append("&quot;");
			    } else if (character == '\t') {
				    addCharEntity(9, retval);
			    } else if (character == '!') {
				    addCharEntity(33, retval);
			    } else if (character == '#') {
				    addCharEntity(35, retval);
			    } else if (character == '$') {
				    addCharEntity(36, retval);
			    } else if (character == '%') {
				    addCharEntity(37, retval);
			    } else if (character == '\'') {
				    addCharEntity(39, retval);
			    } else if (character == '(') {
				    addCharEntity(40, retval);
			    } else if (character == ')') {
				    addCharEntity(41, retval);
			    } else if (character == '*') {
				    addCharEntity(42, retval);
			    } else if (character == '+') {
				    addCharEntity(43, retval);
			    } else if (character == ',') {
				    addCharEntity(44, retval);
			    } else if (character == '-') {
				    addCharEntity(45, retval);
			    } else if (character == '.') {
				    addCharEntity(46, retval);
			    } else if (character == '/') {
				    addCharEntity(47, retval);
			    } else if (character == ':') {
				    addCharEntity(58, retval);
			    } else if (character == ';') {
				    addCharEntity(59, retval);
			    } else if (character == '=') {
				    addCharEntity(61, retval);
			    } else if (character == '?') {
				    addCharEntity(63, retval);
			    } else if (character == '@') {
				    addCharEntity(64, retval);
			    } else if (character == '[') {
				    addCharEntity(91, retval);
			    } else if (character == '\\') {
				    addCharEntity(92, retval);
			    } else if (character == ']') {
				    addCharEntity(93, retval);
			    } else if (character == '^') {
				    addCharEntity(94, retval);
			    } else if (character == '_') {
				    addCharEntity(95, retval);
			    } else if (character == '`') {
				    addCharEntity(96, retval);
			    } else if (character == '{') {
				    addCharEntity(123, retval);
			    } else if (character == '|') {
				    addCharEntity(124, retval);
			    } else if (character == '}') {
				    addCharEntity(125, retval);
			    } else if (character == '~') {
				    addCharEntity(126, retval);
			    } else {
				    retval.append(character);
			    }
			    character = iterator.next();
		    }
		    return retval.tostring();
	    }
        */
	    /**
	     * Convert integer to char entity
	     */
	    public static void addCharEntity(int i, StringBuilder sb) {

		    string padding = "";
		    if (i <= 9) {
			    padding = "00";
		    } else if (i <= 99) {
			    padding = "0";
		    }
		    string number = padding + i;
		    sb.Append("&#").Append(number).Append(";");
	    }

	    /**
	     * Return true if the string is null or "".
	     *
	     * @param str
	     * @return true if the string is "empty"
	     */
	    public static bool isEmpty(string str) {
		    return (str == null || str.Trim().Equals(""));
	    }

	    /**
	     * Return true if the string is not null and not == "".
	     *
	     * @param str
	     * @return true if the string is NOT "empty"
	     */
	    public static bool isNotEmpty(string str) {
		    return (str != null && !str.Equals(""));
	    }
	
	    public static double parseDouble(string doublestringValue) {
		    double amount = 0.0;
		
		    if ( null != doublestringValue && 0 < doublestringValue.Trim().Length)
		    {
			    double.TryParse(doublestringValue.Trim(), out amount );
		    }
		
		    return amount;
	    }
	
	    public static int parseInt(string intstringValue) {
		    int amount = 0;
		
		    if ( null != intstringValue && 0 < intstringValue.Trim().Length)
		    {
			    int.TryParse(intstringValue.Trim(), out amount);
		    }
		
		    return amount;
	    }

	    public static bool parseBool(string boolstringValue) {
		    bool result = false;
		
		    if ( null != boolstringValue && 0 < boolstringValue.Trim().Length)
		    {
			    bool.TryParse(boolstringValue.Trim(), out result);
		    }
		
		    return result;
	    }
    }
}