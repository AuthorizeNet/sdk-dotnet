using System.Collections.Generic;
using System.Linq;
using AuthorizeNet.APICore;

namespace AuthorizeNet {
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

        /// <summary>
        /// Gets the subscription details.
        /// </summary>
        /// <param name="subscriptionID">The subscription ID.</param>
        /// <returns></returns>
        public ARBGetSubscriptionResponse GetSubscription(string subscriptionID)
        {
            var req = new ARBGetSubscriptionRequest();
            req.subscriptionId = subscriptionID;
            var response = (ARBGetSubscriptionResponse)_gateway.Send(req);
            
            return response;
        }

        /// <summary>
        /// Gets a list of all subscriptions
        /// </summary>
        /// <returns></returns>
        public List<SubscriptionDetail> GetSubscriptionList(
            ARBGetSubscriptionListSearchTypeEnum searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive,
            int page = 0,
            int pageSize = 100)
        {
            var req = new ARBGetSubscriptionListRequest();
            req.searchType = searchType;
            req.paging = new Paging
            {
                limit = pageSize,
                offset = page
            };
            req.sorting = new ARBGetSubscriptionListSorting
            {
                orderBy = ARBGetSubscriptionListOrderFieldEnum.createTimeStampUTC,
                orderDescending = true
            };
            
            var response = (ARBGetSubscriptionListResponse)_gateway.Send(req);

            if (response == null || response.subscriptionDetails == null)
            {
                return null;
            }
            
            return response.subscriptionDetails.ToList();
        }
    }
}
