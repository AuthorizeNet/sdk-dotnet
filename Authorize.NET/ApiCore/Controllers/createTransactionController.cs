namespace AuthorizeNet.ApiCore.Controllers
{
    using System;
    using AuthorizeNet.ApiCore.Contracts.V1;
    using AuthorizeNet.ApiCore.Controllers.Bases;

#pragma warning disable 1591
    public class createTransactionController : ApiOperationBase<createTransactionRequest, createTransactionResponse> {

	    public createTransactionController(createTransactionRequest apiRequest) : base(apiRequest) {
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
            RequestFactoryWithSpecified.createTransactionRequest(request);
        }
    }
#pragma warning restore 1591
}