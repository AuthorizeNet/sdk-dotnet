﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuthorizeNet {

    /// <summary>
    /// The devise which read in the Credit Card
    /// </summary>
    public enum DeviceType {
        /// <summary>
        /// 
        /// </summary>
        Unknown = 1,
        /// <summary>
        /// 
        /// </summary>
        UnattendedTerminal = 2,
        /// <summary>
        /// 
        /// </summary>
        SelfServiceTerminal = 3,
        /// <summary>
        /// 
        /// </summary>
        ElectronicCashRegister = 4,
        /// <summary>
        /// 
        /// </summary>
        PersonalComputerBasedTerminal = 5,
        /// <summary>
        /// 
        /// </summary>
        AirPay = 6,
        /// <summary>
        /// 
        /// </summary>
        WirelessPOS = 7,
        /// <summary>
        /// 
        /// </summary>
        Website = 8,
        /// <summary>
        /// 
        /// </summary>
        DialTerminal = 9,
        /// <summary>
        /// 
        /// </summary>
        VirtualTerminal = 10

    }

    /// <summary>
    /// The gateway which runs the credit card transaction
    /// </summary>

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated CP and CNP both use similar request structure (with differences in payment fields).   
    //@deprecated For CP, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions 
    [Obsolete("AuthorizeNetCP is deprecated, use AuthorizeNet::API instead. For CP, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/PaymentTransactions.", false)]
    public class CardPresentGateway:Gateway, ICardPresentGateway {

        string _serviceUrl = "https://cardpresent.authorize.net/gateway/transact.dll";
        DeviceType _deviceType = DeviceType.PersonalComputerBasedTerminal;
        string _marketType = "2";

        /// <summary>
        /// Initializes a new instance of the <see cref="CardPresentGateway"/> class.
        /// </summary>
        /// <param name="apiLogin">The API login.</param>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="isTest">if set to <c>true</c> [is test].</param>
        public CardPresentGateway(string apiLogin, string transactionKey, bool isTest)
            : base(apiLogin, transactionKey, isTest) {
            if (isTest) {
                _serviceUrl = "https://test.authorize.net/gateway/transact.dll";
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CardPresentGateway"/> class.
        /// </summary>
        /// <param name="apiLogin">The API login.</param>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="marketType">Type of the market.</param>
        /// <param name="deviceType">Type of the device.</param>
        /// <param name="isTest">if set to <c>true</c> [is test].</param>
        public CardPresentGateway(string apiLogin, string transactionKey, string marketType, DeviceType deviceType, bool isTest)
            : base(apiLogin, transactionKey, isTest) {
            _deviceType = deviceType;
            _marketType = marketType;
            if (isTest) {
                _serviceUrl = "https://test.authorize.net/gateway/transact.dll";
            }

        }


        /// <summary>
        /// Sends the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public override IGatewayResponse Send(IGatewayRequest request, string description) {

            var device = (int)_deviceType;
            //make sure to send in the API version
            request.Queue("x_cpversion", "1.0");
            request.Queue("x_market_type", _marketType);
            request.Queue("x_device_type", device.ToString());
            request.Queue("x_response_format", "1");

            LoadAuthorization(request);
            request.Queue(ApiFields.Description, description);

            //make sure this is empty
            request.RelayResponse = "";

            var response = SendRequest(_serviceUrl, request);


            return new CardPresentResponse(response.Split('|'));
        }

    }
}
