using System;

namespace AuthorizeNet.Util
{
	//@deprecated since version 1.9.8
	//@deprecated Since it is not using by New model Code
	[Obsolete("Since the classes using it are deprecated", false)]
	class HtmlHelper
	{
		/// <summary>
		/// This will issue a full HTML document with a built-in script, which will redirect the browser away from
		/// Authorize.NET to the URL you pass in. Be sure the toURL is absolute.  This can be used in your DPM Replay Response Endpoint
		/// </summary>
		/// <param name="toUrl"></param>
		/// <returns></returns>
		public static string RelayResponseRedirecter(string toUrl)
		{

			return string.Format("<html><head><script type='text/javascript' charset='utf-8'>	window.location='{0}';</script><noscript><meta http-equiv='refresh' content='1;url={0}'></noscript></head><body></body></html>", toUrl);

		}
	}
}
