namespace AuthorizeNet.Util
{
    using System;
    using System.Diagnostics;
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
        private static TraceSource traceSource = new TraceSource("AnetDotNetSdkTrace");

        public void error(string logMessage) { Trace(TraceEventType.Error, logMessage); }
        public void info(string logMessage) { Trace(TraceEventType.Information, logMessage); }
        public void debug(string logMessage) { Trace(TraceEventType.Verbose, logMessage); }
        public void warn(string logMessage) { Trace(TraceEventType.Warning, logMessage); }

        public void error(object logMessage) { error(logMessage.ToString()); }
        public void info(object logMessage)  { info(logMessage.ToString());  }
        public void debug(object logMessage) { debug(logMessage.ToString()); }
        public void warn(object logMessage)  { warn(logMessage.ToString());  }

        public static void Trace(TraceEventType eventType, string message)
        {
            try
            {
                if (traceSource.Switch.ShouldTrace(eventType))
                {
                    string tracemessage = string.Format("{0}\t[{1}]\t{2}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), eventType, message);
                    foreach (TraceListener listener in traceSource.Listeners)
                    {
                        listener.WriteLine(tracemessage);
                        listener.Flush();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
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