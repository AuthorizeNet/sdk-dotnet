using System;
using AuthorizeNet.APICore;
namespace AuthorizeNet
{

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For ARB, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling 
    [Obsolete("AuthorizeNetARB is deprecated, use AuthorizeNet::API instead. For ARB, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/RecurringBilling.", false)]
    public interface ISubscriptionGateway
    {
        bool CancelSubscription(string subscriptionID);
        ISubscriptionRequest CreateSubscription(ISubscriptionRequest subscription);
        ARBSubscriptionStatusEnum GetSubscriptionStatus(string subscriptionID);
        bool UpdateSubscription(ISubscriptionRequest subscription);

        System.Collections.Generic.List<SubscriptionDetail> GetSubscriptionList(
            ARBGetSubscriptionListSearchTypeEnum searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive,
            int page = 0,
            int pageSize = 100);
    }
}
