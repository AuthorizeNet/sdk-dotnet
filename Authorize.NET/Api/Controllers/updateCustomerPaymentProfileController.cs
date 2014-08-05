namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public class updateCustomerPaymentProfileController : ApiOperationBase<updateCustomerPaymentProfileRequest, updateCustomerPaymentProfileResponse> {

	    public updateCustomerPaymentProfileController(updateCustomerPaymentProfileRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    //if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
		    //if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");
		
		    //validate not-required fields		
	    }

        protected override void BeforeExecute()
        {
            var request = GetApiRequest();
            RequestFactoryWithSpecified.updateCustomerPaymentProfileRequest(request);
        }
    }
#pragma warning restore 1591
}