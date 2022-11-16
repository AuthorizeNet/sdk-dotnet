#if NETSTANDARD || NET6_0
using AuthorizeNet.APICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeNet
{
    /// <summary>
    /// Authorize.Net connection options, the Environment, ApiLoginId and TransactionKey are required. 
    /// When Environment is CUSTOM, the BaseUrl, XmlBaseUrl are required.
    /// </summary>
    public class AuthorizeNetOptions
    {
        /// <summary>
        /// Environment Name, can be SandBox, Production or Custom. The BaseUrl and XmlBaseUrl are required when Environment is Custom.
        /// </summary>
        public string Environment { get; set; } = "SANDBOX";
        public string ApiLoginId { get; set; }
        public string TransactionKey { get; set; }

        public bool? UseProxy { get; set; }
        public string ProxyHost { get; set; }
        public int? ProxyPort { get; set; }

        public int? ConnectionTimeout { get; set; }
        public int? ReadWriteTimeout { get; set; }

        public string BaseUrl { get; set; }
        public string XmlBaseUrl { get; set; }
        public string CardPresentUrl { get; set; }

        public bool LoggingSensitiveData { get; set; }

    }
}
#endif