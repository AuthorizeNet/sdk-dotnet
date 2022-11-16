namespace AuthorizeNet.Util
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text.RegularExpressions;
#if NET45
    using System.Diagnostics;
#endif
#if NETSTANDARD || NET6_0
    using Microsoft.Extensions.Logging;
#endif
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
#if NET45
        private static TraceSource traceSource = new TraceSource("AnetDotNetSdkTrace");

        public void error(string logMessage) { Trace(TraceEventType.Error, logMessage); }
        public void info(string logMessage) { Trace(TraceEventType.Information, logMessage); }
        public void debug(string logMessage) { Trace(TraceEventType.Verbose, logMessage); }
        public void warn(string logMessage) { Trace(TraceEventType.Warning, logMessage); }
        public void trace(string logMessage)
        {
            Trace(TraceEventType.Verbose, logMessage);
        }
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
#endif
#if NETSTANDARD || NET6_0
        private readonly ILogger _logger;
        private bool _loggingSensitiveData = false;
        public Log(ILogger logger, bool loggingSensitiveData)
        {
            this._logger = logger;
            this._loggingSensitiveData = loggingSensitiveData;
        }
        public void error(string logMessage)
        {
            this._logger.LogError(maskSensitiveMessage(logMessage));
        }
        public void info(string logMessage)
        {
            this._logger.LogInformation(maskSensitiveMessage(logMessage));
        }
        public void debug(string logMessage)
        {
            this._logger.LogDebug(maskSensitiveMessage(logMessage));
        }
        public void warn(string logMessage)
        {
            this._logger.LogWarning(maskSensitiveMessage(logMessage));
        }
        public void trace(string logMessage)
        {
            this._logger.LogTrace(maskSensitiveMessage(logMessage));
        }
        private string maskSensitiveMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return message;

            if (this._loggingSensitiveData)
                return message;

            string maskedXmlMessage = SensitiveDataTextLogger.maskSensitiveXmlString(message);
            string maskedMessage = SensitiveDataTextLogger.maskCreditCards(maskedXmlMessage);
            return maskedMessage;
        }
#endif
        public void error(object logMessage) { error(logMessage.ToString()); }
        public void info(object logMessage) { info(logMessage.ToString()); }
        public void debug(object logMessage) { debug(logMessage.ToString()); }
        public void warn(object logMessage) { warn(logMessage.ToString()); }
        public void trace(object logMessage) { trace(logMessage.ToString()); }
    }

    public class LogFactory
    {
#if NET45
        private static readonly Log Logger = new Log();
        public static Log getLog(Type classType)
        {
            return Logger;
        }
#endif
#if NETSTANDARD || NET6_0
        public static ILoggerFactory LoggerFactory { get; set; }
        public static bool LoggingSensitiveData { get; set; }
        public static Log getLog(Type classType)
        {
            if (null == LoggerFactory)
                throw new Exception($"The static property LogFactory.LoggerFactory is not set.");

            var logger = LoggerFactory.CreateLogger(classType);

            return new Log(logger, LogFactory.LoggingSensitiveData);

        }
#endif
    }

}