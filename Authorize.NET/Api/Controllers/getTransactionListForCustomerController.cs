namespace AuthorizeNet.Api.Controllers
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;

#pragma warning disable 1591
    public class getTransactionListForCustomerController : ApiOperationBase<getTransactionListForCustomerRequest, getTransactionListResponse> {

	    public getTransactionListForCustomerController(getTransactionListForCustomerRequest apiRequest) : base(apiRequest) {
	    }

	    protected override void ValidateRequest()
		{
            var request = GetApiRequest();

			if (String.IsNullOrEmpty(request.customerProfileId))
				throw new ArgumentException("Customer Profile ID is required.");
	    }

		protected override void BeforeExecute()
		{
			var request = GetApiRequest();
			RequestFactoryWithSpecified.getTransactionListForCustomerRequest(request);
		}
	}
#pragma warning restore 1591
}