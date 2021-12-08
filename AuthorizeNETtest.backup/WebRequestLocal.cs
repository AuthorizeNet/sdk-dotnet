using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace AuthorizeNETtest
{
    public class WebRequestCreateLocal : IWebRequestCreate
    {
        public WebRequestCreateLocal()
        {
        }

        public WebRequestCreateLocal(string response)
        {
            ResponseString = response;
        }

        public string ResponseString
        {
            get { return ResponseStrings[ResponseStringCount - 1]; }
            set
            {
                ResponseStringCount = 0;
                if (ResponseStrings == null)
                {
                    ResponseStrings = new string[] {value};
                }
                else
                {
                    ResponseStrings[0] = value;
                }
            }
        }
        public string[] ResponseStrings { get; set; }
        public int ResponseStringCount { get; set; }

        public WebRequest Create(Uri uri)
        {
            SerializationInfo si = new SerializationInfo(typeof(HttpWebRequest), new System.Runtime.Serialization.FormatterConverter());
            StreamingContext sc = new StreamingContext();
            WebHeaderCollection headers = new WebHeaderCollection();
            WebProxy proxy = new WebProxy();
            si.AddValue("_HttpRequestHeaders", new WebHeaderCollection(), typeof(WebHeaderCollection));
            si.AddValue("_Proxy", null, typeof(IWebProxy));
            si.AddValue("_KeepAlive", true);
            si.AddValue("_Pipelined", true);
            si.AddValue("_AllowAutoRedirect", true);
            si.AddValue("_AllowWriteStreamBuffering", true);
            si.AddValue("_HttpWriteMode", 0);
            si.AddValue("_MaximumAllowedRedirections", 0);
            si.AddValue("_AutoRedirects", 0);
            si.AddValue("_Timeout", 500); //need to check below
            si.AddValue("_ReadWriteTimeout", 500);
            si.AddValue("_MaximumResponseHeadersLength", 128);
            si.AddValue("_ContentLength", 0);
            si.AddValue("_MediaType", 0);
            si.AddValue("_OriginVerb", 0);
            si.AddValue("_ConnectionGroupName", null);
            si.AddValue("_Version", HttpVersion.Version11, typeof(Version));
            si.AddValue("_OriginUri", new Uri("http://localhost"), typeof(Uri));

            WebRequestLocal request = new WebRequestLocal(si, sc);
            ResponseStringCount++;            
            request.ResponseString = ResponseString;

            return request;
        }
    }

    public class WebRequestLocal : HttpWebRequest
    {
        public WebRequestLocal(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public string ResponseString { get; set; }

        public override long ContentLength { get; set; }

        public override Stream GetRequestStream()
        {
            MemoryStream ms = new MemoryStream();
            ms.Capacity = (int) ContentLength;
            return ms;
        }

        public override WebResponse GetResponse()
        {
            SerializationInfo si = new SerializationInfo(typeof(HttpWebResponse), new System.Runtime.Serialization.FormatterConverter());
            StreamingContext sc = new StreamingContext();
            WebHeaderCollection headers = new WebHeaderCollection();
            si.AddValue("m_HttpResponseHeaders", headers);
            si.AddValue("m_Uri", new Uri("http://localhost"));
            si.AddValue("m_Certificate", null);
            si.AddValue("m_Version", HttpVersion.Version11);
            si.AddValue("m_StatusCode", HttpStatusCode.OK);
            si.AddValue("m_ContentLength", 0);
            si.AddValue("m_Verb", "GET");
            si.AddValue("m_StatusDescription", "Local Response");
            si.AddValue("m_MediaType", null);

            WebResponseLocal response = new WebResponseLocal(si, sc);
            response.ResponseString = ResponseString;
            return response;
        }
    }

    public class WebResponseLocal : HttpWebResponse
    {
        public WebResponseLocal(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public string ResponseString { get; set; }

        public override Stream GetResponseStream()
        {
            Encoding ascii = Encoding.ASCII;
            byte[] bytes = ascii.GetBytes(ResponseString);
            MemoryStream ms = new MemoryStream();
            ms.Write(bytes, 0, bytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
