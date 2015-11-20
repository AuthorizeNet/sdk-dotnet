namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public class ARBGetSubscriptionController : ApiOperationBase<ARBGetSubscriptionRequest, ARBGetSubscriptionResponse> {

	    public ARBGetSubscriptionController(ARBGetSubscriptionRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    if ( request.subscriptionId == null) throw new ArgumentException( "Subscription ID cannot be null");
		    
            //if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");
		
		    //validate not-required fields		
	    }

        protected override void BeforeExecute()
        {
            var request = GetApiRequest();
            RequestFactoryWithSpecified.ARBGetSubscriptionRequest(request);
        }
    }
#pragma warning restore 1591
}