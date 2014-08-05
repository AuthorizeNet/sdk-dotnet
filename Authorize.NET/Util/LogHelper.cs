namespace AuthorizeNet.Util
{
    using System;
    using System.Globalization;

    /// <summary>
    /// 
    /// 
    /// </summary>
    public static class LogHelper {

	    static LogHelper() {
	    }

	    public static void debug(Log logger, string format, params object[] arguments) {
		    string logMessage = getMessage(logger, format, arguments);
		    if ( null != logMessage) { logger.debug(logMessage); }
	    }

	    public static void error(Log logger, string format, params object[]  arguments) {
		    string logMessage = getMessage(logger, format, arguments);
		    if ( null != logMessage) { logger.error(logMessage); }
	    }
	
	    public static void info(Log logger, string format, params object[]  arguments) {
		    string logMessage = getMessage(logger, format, arguments);
		    if ( null != logMessage) { logger.info(logMessage); }
	    }

	    public static void warn(Log logger, string format, params object[]  arguments) {
		    string logMessage = getMessage(logger, format, arguments);
		    if ( null != logMessage) { logger.warn(logMessage); }
	    }

	    private static string getMessage(Log logger, string format, params object[]  arguments) {
		    string logMessage = null;
		
		    if ( null != logger && null != format && 0 < format.Trim().Length) {
			    logMessage = string.Format(CultureInfo.InvariantCulture, format, arguments);
			    //do encoding etc here or output neutralization as necessary 
		    }
		    return logMessage;
	    }
    }

    public class Log
    {
        public void error(string logMessage) { System.Diagnostics.Trace.WriteLine(logMessage); }
        public void info(string logMessage)  { System.Diagnostics.Trace.WriteLine(logMessage); }
        public void debug(string logMessage) { System.Diagnostics.Trace.WriteLine(logMessage); }
        public void warn(string logMessage)  { System.Diagnostics.Trace.WriteLine(logMessage); }

        public void error(object logMessage) { error(logMessage.ToString()); }
        public void info(object logMessage)  { info(logMessage.ToString());  }
        public void debug(object logMessage) { debug(logMessage.ToString()); }
        public void warn(object logMessage)  { warn(logMessage.ToString());  }
    }

    public class LogFactory
    {
        private static readonly Log Logger = new Log();
        public static Log getLog(Type classType)
        {
            return Logger;
        }
    }
}