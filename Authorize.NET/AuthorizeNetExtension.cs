#if NETSTANDARD || NET6_0
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace AuthorizeNet
{
    /// <summary>
    /// AuthorizeNet ServiceProvider Extension. 
    /// </summary>
    public static class AuthorizeNetExtension
    {
        /// <summary>
        /// Setup client library options. Call this extension function to setup Authorize.Net client library. 
        /// It set the ILoggerFactory for logging, 
        ///    set ApiOperationBase&lt;ANetApiRequest, ANetApiResponse&gt;.RunEnvironment according to AuthorizeNetOptions.Environment
        ///    set ApiOperationBase&lt;ANetApiRequest, ANetApiResponse&gt;.MerchantAuthentication according to AuthorizeNetOptions.ApiLoginId and AuthorizeNetOptions.TransactionKey.
        /// </summary>
        /// <param name="services">Service Provider. <see cref="IServiceProvider"/></param>
        /// <param name="optionsBuilder">The AuthorizeNetOptions builder delegation. </param>
        /// <param name="merchantAuenticationBuilder">The default merchant authentication builder delegation.</param>
        /// <exception cref="Exception">
        /// The AuthorizeNetOptions can be configured either in ConfigureService with IServiceCollection.Configure&lt;AuthorizeNetOptions&gt; or use optionsBuilder. Exception is thrown when neither of them is set. 
        /// The merchantAuenticationBuilder is called to setup the default marchant authentication. 
        /// ILoggerFactory is required in .Net Core, exception is thrown when the ILoggerFactory is not configured in DI container. 
        /// </exception>
        public static void SetupAuthorizeNet(this IServiceProvider services, Action<AuthorizeNetOptions> optionsBuilder = null, Action<merchantAuthenticationType> merchantAuenticationBuilder = null)
        {
            AuthorizeNetOptions options = null;
            IOptions<AuthorizeNetOptions> optionsMonitor = services.GetService<IOptions<AuthorizeNetOptions>>();

            if (optionsMonitor == null && optionsBuilder == null)
                throw new Exception($"The AuthorizeNetOptions is not configured and optionsBuilder Action is not set.");

            if (optionsMonitor != null)
                options = optionsMonitor.Value;

            optionsBuilder?.Invoke(options);

            //setup ILoggerFactory
            ILoggerFactory loggerFactory = services.GetService<ILoggerFactory>();
            if (loggerFactory == null)
                throw new Exception($"Can't Get the ILoggerFactory service.");

            LogFactory.LoggerFactory = loggerFactory;
            LogFactory.LoggingSensitiveData = options.LoggingSensitiveData;

            //setup environment
            AuthorizeNet.Environment.GetConfigurationPropertyValue = (propName) =>
            {
                string propValue = null;
                if (propName == Constants.HttpConnectionTimeout)
                {
                    if (options.ConnectionTimeout != null)
                        propValue = options.ConnectionTimeout.ToString();
                }
                else if (propName == Constants.HttpReadWriteTimeout)
                {
                    if (options.ReadWriteTimeout != null)
                        propValue = options.ReadWriteTimeout.ToString();
                }
                else if (propName == Constants.HttpsUseProxy)
                {
                    if (options.UseProxy != null)
                        propValue = options.UseProxy.ToString();
                }
                else if (propName == Constants.HttpsProxyHost)
                {
                    propValue = options.ProxyHost;
                }
                else if (propName == Constants.HttpsProxyPort)
                {
                    if (options.ProxyPort != null)
                        propValue = options.ProxyPort.ToString();
                }
                else
                {
                    throw new Exception($"The property {propName} is not configured in AuthorizeNetOptions ");
                }

                return propValue;
            };


            AuthorizeNet.Environment runningEnvironment = AuthorizeNet.Environment.LOCAL_VM;

            if (0 == string.Compare(options.Environment, "PRODUCTION", true))
            {
                runningEnvironment = AuthorizeNet.Environment.PRODUCTION;
            }
            else if (0 == string.Compare(options.Environment, "SANDBOX", true))
            {
                runningEnvironment = AuthorizeNet.Environment.SANDBOX;
            }
            else if (0 == string.Compare(options.Environment, "LOCAL_VM", true))
            {
                runningEnvironment = AuthorizeNet.Environment.LOCAL_VM;
            }
            else if (0 == string.Compare(options.Environment, "HOSTED_VM", true))
            {
                runningEnvironment = AuthorizeNet.Environment.HOSTED_VM;
            }
            else if (0 == string.Compare(options.Environment, "CUSTOM", true))
            {
                if (string.IsNullOrWhiteSpace(options.BaseUrl))
                    throw new Exception("The AuthorizeNetOptions.BaseUrl can not be empty in Custom environment.");

                if (string.IsNullOrWhiteSpace(options.XmlBaseUrl))
                    throw new Exception("The AuthorizeNetOptions.XmlBaseUrl can not be empty in Custom environment.");

                runningEnvironment = AuthorizeNet.Environment.createEnvironment(options.BaseUrl, options.XmlBaseUrl, options.CardPresentUrl);
            }
            else
            {
                throw new Exception($"Invalid environment name: {options.Environment}, it must be Production, SandBox, Local_VM, Hosted_VM or Custom");
            }

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = runningEnvironment;

            //setup merchant authentication
            merchantAuthenticationType merchantAuthentication = new merchantAuthenticationType
            {
                name = options.ApiLoginId,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = options.TransactionKey
            };
            merchantAuenticationBuilder?.Invoke(merchantAuthentication);
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = merchantAuthentication;
        }

    }
}
#endif