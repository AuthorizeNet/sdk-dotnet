namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public class ARBGetSubscriptionListController : ApiOperationBase<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse> {

	    public ARBGetSubscriptionListController(ARBGetSubscriptionListRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		}
    }
#pragma warning restore 1591
}