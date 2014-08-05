namespace AuthorizeNet.Util
{
    using System;
    using System.Net;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public static class HttpUtility {

	    private static readonly Log Logger = LogFactory.getLog(typeof(HttpUtility));
        private static bool ProxySet = false;

        static bool UseProxy = AuthorizeNet.Environment.getBooleanProperty(Constants.HTTPS_USE_PROXY);
        static String ProxyHost = AuthorizeNet.Environment.GetProperty(Constants.HTTPS_PROXY_HOST);
        static int ProxyPort = AuthorizeNet.Environment.getIntProperty(Constants.HTTPS_PROXY_PORT);

        private static Uri GetPostUrl(AuthorizeNet.Environment env) 
	    {
		    var postUrl = new Uri(env.getXmlBaseUrl() + "/xml/v1/request.api");
            Logger.info(string.Format("Creating PostRequest Url: '{0}'", postUrl));

		    return postUrl;
	    }

        public static ANetApiResponse PostData<TQ, TS>(AuthorizeNet.Environment env, TQ request) 
            where TQ : ANetApiRequest 
            where TS : ANetApiResponse
        {
            ANetApiResponse response = null;
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }
            Logger.info(string.Format("MerchantInfo->LoginId/TransactionKey: '{0}':'{1}'->{2}", 
                request.merchantAuthentication.name, request.merchantAuthentication.ItemElementName, request.merchantAuthentication.Item));
		    
	        var postUrl = GetPostUrl(env);
            var webRequest = (HttpWebRequest) WebRequest.Create(postUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml";
            webRequest.KeepAlive = true;
            webRequest.Proxy = SetProxyIfRequested(webRequest.Proxy);

            var requestType = typeof (TQ);

            var serializer = new XmlSerializer(requestType);

	        using (var writer = new XmlTextWriter(webRequest.GetRequestStream(), Encoding.UTF8))
	        {
	            serializer.Serialize(writer, request);
	        }
	        // Get the response
	        using (var webResponse = webRequest.GetResponse())
	        {
	            Logger.info(string.Format("Received Response: '{0}'", webResponse));

	            var responseType = typeof (TS);
	            var deSerializer = new XmlSerializer(responseType);
	            using (var stream = webResponse.GetResponseStream())
	            {
	                Logger.info(string.Format("Deserializing Response from Stream: '{0}'", stream));

	                if (null != stream)
	                {
	                    var deSerializedObject = deSerializer.Deserialize(stream);
                        //if error response
                        if (deSerializedObject is ErrorResponse)
                        {
                            response = deSerializedObject as ErrorResponse;
                        }
                        else
                        {
                            //actual response of type expected
                            if (deSerializedObject is TS)
                            {
                                response = deSerializedObject as TS;
                            }
                            else if (deSerializedObject is ANetApiResponse) //generic response
                            {
	                            response = deSerializedObject as ANetApiResponse;
	                        }
                        }
                    }
	            }
	        }

	        return response;
	    }

        public static WebProxy SetProxyIfRequested(IWebProxy proxy)
        {
            var newProxy = proxy as WebProxy;
            if (UseProxy)
            {
                var proxyUri = new Uri(string.Format("{0}://{1}:{2}", Constants.PROXY_PROTOCOL, ProxyHost, ProxyPort));
                if (!ProxySet)
                {
                    Logger.info(string.Format("Setting up proxy to URL: '{0}'", proxyUri));
                    ProxySet = true;
                }
                if (null == proxy)
                {
                    newProxy = new WebProxy(proxyUri);
                }
                if (null != newProxy)
                {
                    newProxy.UseDefaultCredentials = true;
                }
            }

            return newProxy;
        }
    }


#pragma warning restore 1591
}
//http://ecommerce.shopify.com/c/shopify-apis-and-technology/t/c-webrequest-put-and-xml-49458
//http://www.808.dk/?code-csharp-httpwebrequest
