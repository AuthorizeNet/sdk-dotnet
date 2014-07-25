namespace AuthorizeNet.ApiCore.Controllers
{
    using System;
    using AuthorizeNet.ApiCore.Contracts.V1;
    using AuthorizeNet.ApiCore.Controllers.Bases;

#pragma warning disable 1591
    public class ARBGetSubscriptionListController : ApiOperationBase<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse> {

	    public ARBGetSubscriptionListController(ARBGetSubscriptionListRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    if ( 0 == request.searchType) throw new ArgumentException( "SearchType cannot be null");
		
		    //validate not-required fields		
	    }
    }
#pragma warning restore 1591
}