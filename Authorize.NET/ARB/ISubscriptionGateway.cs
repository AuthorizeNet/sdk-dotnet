using System;
using AuthorizeNet.Api.Contracts.V1;

namespace AuthorizeNet {
    public interface ISubscriptionGateway {
        bool CancelSubscription(string subscriptionID);
        ISubscriptionRequest CreateSubscription(ISubscriptionRequest subscription);
        ARBSubscriptionStatusEnum GetSubscriptionStatus(string subscriptionID);
        bool UpdateSubscription(ISubscriptionRequest subscription);
    }
}
