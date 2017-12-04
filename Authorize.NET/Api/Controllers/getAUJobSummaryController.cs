namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public class getAUJobSummaryController : ApiOperationBase<getAUJobSummaryRequest, getAUJobSummaryResponse> {

	    public getAUJobSummaryController(getAUJobSummaryRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    //if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
		    //if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");
		
		    //validate not-required fields		
	    }

#if !NETSTANDARD2_0
        protected override void BeforeExecute()
        {
            var request = GetApiRequest();
        }
#endif

    }
#pragma warning restore 1591
}