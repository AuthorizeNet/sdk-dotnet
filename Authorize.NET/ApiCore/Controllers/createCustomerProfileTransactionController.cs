namespace AuthorizeNet.ApiCore.Controllers
{
    using System;
    using AuthorizeNet.ApiCore.Contracts.V1;
    using AuthorizeNet.ApiCore.Controllers.Bases;

#pragma warning disable 1591
    public class createCustomerProfileTransactionController : ApiOperationBase<createCustomerProfileTransactionRequest, createCustomerProfileTransactionResponse> {

	    public createCustomerProfileTransactionController(createCustomerProfileTransactionRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    //if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
		    //if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");
		
		    //validate not-required fields		
	    }
    }
#pragma warning restore 1591
}