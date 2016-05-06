namespace AuthorizeNet.Util
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public static class HttpUtility {

        //Max response size allowed: 64 MB
        private const int MaxResponseLength = 67108864;
	    private static readonly Log Logger = LogFactory.getLog(typeof(HttpUtility));
        private static bool _proxySet;// = false;

        static readonly bool UseProxy = AuthorizeNet.Environment.getBooleanProperty(Constants.HttpsUseProxy);
        static readonly String ProxyHost = AuthorizeNet.Environment.GetProperty(Constants.HttpsProxyHost);
        static readonly int ProxyPort = AuthorizeNet.Environment.getIntProperty(Constants.HttpsProxyPort);

        private static Uri GetPostUrl(AuthorizeNet.Environment env) 
	    {
		    var postUrl = new Uri(env.getXmlBaseUrl() + "/xml/v1/request.api");
            Logger.debug(string.Format("Creating PostRequest Url: '{0}'", postUrl));

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
            Logger.debug(string.Format("MerchantInfo->LoginId/TransactionKey: '{0}':'{1}'->{2}", 
                request.merchantAuthentication.name, request.merchantAuthentication.ItemElementName, request.merchantAuthentication.Item));
		    
	        var postUrl = GetPostUrl(env);
            var webRequest = (HttpWebRequest) WebRequest.Create(postUrl);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml";
            webRequest.KeepAlive = true;
            webRequest.Proxy = SetProxyIfRequested(webRequest.Proxy);

            //set the http connection timeout 
            var httpConnectionTimeout = AuthorizeNet.Environment.getIntProperty(Constants.HttpConnectionTimeout);
            webRequest.Timeout = (httpConnectionTimeout != 0 ? httpConnectionTimeout : Constants.HttpConnectionDefaultTimeout);

            //set the time out to read/write from stream
            var httpReadWriteTimeout = AuthorizeNet.Environment.getIntProperty(Constants.HttpReadWriteTimeout);
            webRequest.ReadWriteTimeout = (httpReadWriteTimeout != 0 ? httpReadWriteTimeout : Constants.HttpReadWriteDefaultTimeout);

            var requestType = typeof (TQ);
            var serializer = new XmlSerializer(requestType);
            using (var writer = new XmlTextWriter(webRequest.GetRequestStream(), Encoding.UTF8))
	        {
	            serializer.Serialize(writer, request);
	        }

            // Get the response
            String responseAsString = null;
            Logger.debug(string.Format("Retreiving Response from Url: '{0}'", postUrl));
            using (var webResponse = webRequest.GetResponse())
            {
                Logger.debug(string.Format("Received Response: '{0}'", webResponse));

                using (var responseStream = webResponse.GetResponseStream())
                {
                    if (null != responseStream)
                    {
                        var result = new StringBuilder();

                        using (var reader = new StreamReader(responseStream))
                        {
                            while (!reader.EndOfStream)
                            {
                                result.Append((char)reader.Read());

                                if (result.Length >= MaxResponseLength)
                                {
                                    throw new Exception("response is too long.");
                                }
                            }

                            responseAsString = result.Length > 0 ? result.ToString() : null;
                        }
                        Logger.debug(string.Format("Response from Stream: '{0}'", responseAsString));
                    }
                }
            }
            if (null != responseAsString)
            {
                using (var memoryStreamForResponseAsString = new MemoryStream(Encoding.UTF8.GetBytes(responseAsString)))
                {
                    var responseType = typeof (TS);
                    var deSerializer = new XmlSerializer(responseType);

                    Object deSerializedObject;
                    try
                    {
                        // try deserializing to the expected response type
                        deSerializedObject = deSerializer.Deserialize(memoryStreamForResponseAsString);
                    }
                    catch (Exception) 
                    {
                        // probably a bad response, try if this is an error response
                        memoryStreamForResponseAsString.Seek(0, SeekOrigin.Begin); //start from beginning of stream
                        var genericDeserializer = new XmlSerializer(typeof (ANetApiResponse));
                        deSerializedObject = genericDeserializer.Deserialize(memoryStreamForResponseAsString);
                    }

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

            return response;
	    }

        public static IWebProxy SetProxyIfRequested(IWebProxy proxy)
        {
            var newProxy = proxy as WebProxy;

            if (UseProxy)
            {
                var proxyUri = new Uri(string.Format("{0}://{1}:{2}", Constants.ProxyProtocol, ProxyHost, ProxyPort));
                if (!_proxySet)
                {
                    Logger.info(string.Format("Setting up proxy to URL: '{0}'", proxyUri));
                    _proxySet = true;
                }
                if (null == proxy || null == newProxy)
                {
                    newProxy = new WebProxy(proxyUri);
                }

                newProxy.UseDefaultCredentials = true;
                newProxy.BypassProxyOnLocal = true;
            }
            return (newProxy ?? proxy);
        }
    }


#pragma warning restore 1591
}
//http://ecommerce.shopify.com/c/shopify-apis-and-technology/t/c-webrequest-put-and-xml-49458
//http://www.808.dk/?code-csharp-httpwebrequest
