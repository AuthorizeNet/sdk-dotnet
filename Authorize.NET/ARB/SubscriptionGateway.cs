﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using AuthorizeNet.APICore;

namespace AuthorizeNet {

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For ARB, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling 
    [Obsolete("AuthorizeNetARB is deprecated, use AuthorizeNet::API instead. For ARB, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling.", false)]
    public class SubscriptionGateway : ISubscriptionGateway {


        HttpXmlUtility _gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionGateway"/> class.
        /// </summary>
        /// <param name="apiLogin">The API login.</param>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="mode">The mode.</param>
        public SubscriptionGateway(string apiLogin, string transactionKey, ServiceMode mode) {
            
            if (mode == ServiceMode.Live) {
                _gateway = new HttpXmlUtility(ServiceMode.Live, apiLogin, transactionKey);
            } else {
                _gateway = new HttpXmlUtility(ServiceMode.Test, apiLogin, transactionKey);

            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionGateway"/> class.
        /// </summary>
        /// <param name="apiLogin">The API login.</param>
        /// <param name="transactionKey">The transaction key.</param>
        public SubscriptionGateway(string apiLogin, string transactionKey) : this(apiLogin, transactionKey, ServiceMode.Test) { }

        /// <summary>
        /// Creates a new subscription
        /// </summary>
        /// <param name="subscription">The subscription to create - requires that you add a credit card and billing first and last.</param>
        public ISubscriptionRequest CreateSubscription(ISubscriptionRequest subscription) {
            var sub = subscription.ToAPI();
            var req = new ARBCreateSubscriptionRequest();
            req.subscription = sub;
            var response = (ARBCreateSubscriptionResponse)_gateway.Send(req);
            subscription.SubscriptionID = response.subscriptionId;
            return subscription;

        }
        /// <summary>
        /// Updates the subscription.
        /// </summary>
        /// <param name="subscription">The subscription to update. Can't change billing intervals however.</param>
        /// <returns></returns>
        public bool UpdateSubscription(ISubscriptionRequest subscription) {
            var sub = subscription.ToUpdateableAPI();
            var req = new ARBUpdateSubscriptionRequest();
            req.subscription = sub;
            req.subscriptionId = subscription.SubscriptionID;
            var response = (ARBUpdateSubscriptionResponse)_gateway.Send(req);
            return true;

        }
        /// <summary>
        /// Cancels the subscription.
        /// </summary>
        /// <param name="subscriptionID">The subscription ID.</param>
        /// <returns></returns>
        public bool CancelSubscription(string subscriptionID) {
            var req = new ARBCancelSubscriptionRequest();

            req.subscriptionId = subscriptionID;
            //will throw if there are errors
            var response = (ARBCancelSubscriptionResponse)_gateway.Send(req);
            return true;
        }

        /// <summary>
        /// Gets the subscription status.
        /// </summary>
        /// <param name="subscriptionID">The subscription ID.</param>
        /// <returns></returns>
        public ARBSubscriptionStatusEnum GetSubscriptionStatus(string subscriptionID) {
            var req = new ARBGetSubscriptionStatusRequest();
            req.subscriptionId = subscriptionID;
            var response = (ARBGetSubscriptionStatusResponse)_gateway.Send(req);

            return response.status;
        }

        

    }
}
