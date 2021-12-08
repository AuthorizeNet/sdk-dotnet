using System;
using System.Diagnostics;
using System.Globalization;

namespace AuthorizeNet.Util
{
	/// <summary>
	///
	///
	/// </summary>
	public static class LogHelper
	{

		static LogHelper()
		{
		}

		public static void Debug(Log logger, string format, params object[] arguments)
		{
			var logMessage = GetMessage(logger, format, arguments);
			if (null != logMessage) { logger.Debug(logMessage); }
		}

		public static void Error(Log logger, string format, params object[] arguments)
		{
			var logMessage = GetMessage(logger, format, arguments);
			if (null != logMessage) { logger.Error(logMessage); }
		}

		public static void Info(Log logger, string format, params object[] arguments)
		{
			var logMessage = GetMessage(logger, format, arguments);
			if (null != logMessage) { logger.Info(logMessage); }
		}

		public static void Warn(Log logger, string format, params object[] arguments)
		{
			var logMessage = GetMessage(logger, format, arguments);
			if (null != logMessage) { logger.Warn(logMessage); }
		}

		private static string GetMessage(Log logger, string format, params object[] arguments)
		{
			string logMessage = null;

			if (null != logger && null != format && 0 < format.Trim().Length)
			{
				logMessage = string.Format(CultureInfo.InvariantCulture, format, arguments);
				//do encoding etc here or output neutralization as necessary
			}
			return logMessage;
		}
	}

	public class Log
	{
		private static readonly TraceSource traceSource = new("AnetDotNetSdkTrace");

		public void Error(string logMessage) => Trace(TraceEventType.Error, logMessage);
		public void Info(string logMessage) => Trace(TraceEventType.Information, logMessage);
		public void Debug(string logMessage) => Trace(TraceEventType.Verbose, logMessage);
		public void Warn(string logMessage) => Trace(TraceEventType.Warning, logMessage);

		public void Error(object logMessage) => this.Error(logMessage.ToString());
		public void Info(object logMessage) => this.Info(logMessage.ToString());
		public void Debug(object logMessage) => this.Debug(logMessage.ToString());
		public void Warn(object logMessage) => this.Warn(logMessage.ToString());

		public static void Trace(TraceEventType eventType, string message)
		{
			try
			{
				if (traceSource.Switch.ShouldTrace(eventType))
				{
					var tracemessage = string.Format("{0}\t[{1}]\t{2}", DateTime.Now.ToString("MM/dd/yy HH:mm:ss"), eventType, message);
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
		private static readonly Log Logger = new();
		public static Log GetLog(Type classType) => Logger;
	}

}