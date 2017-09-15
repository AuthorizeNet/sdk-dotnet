namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
#if !NETSTANDARD2_0
    public class createFingerPrintController : ApiOperationBase<createFingerPrintRequest, createFingerPrintResponse> {

	    public createFingerPrintController(createFingerPrintRequest apiRequest) : base(apiRequest) {
	    }

	    override protected void ValidateRequest() {
            var request = GetApiRequest();
		
		    //validate required fields		
		    //if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
            if (null == request.supportInformation) throw new ArgumentException("supportInformation cannot be null");
		
		    //validate not-required fields		
	    }

        protected override void BeforeExecute()
        {
            var request = GetApiRequest();
            RequestFactoryWithSpecified.createFingerPrintRequest(request);
        }
    }
#endif
#pragma warning restore 1591
}