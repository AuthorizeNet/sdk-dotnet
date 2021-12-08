using System;

using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace AuthorizeNet.Api.Controllers
{
#pragma warning disable 1591
	public class CreateCustomerProfileFromTransactionController : ApiOperationBase<createCustomerProfileFromTransactionRequest, createCustomerProfileResponse>
	{

		public CreateCustomerProfileFromTransactionController(createCustomerProfileFromTransactionRequest apiRequest)
			: base(apiRequest)
		{
		}

		override protected void ValidateRequest()
		{
			var request = GetApiRequest();

			//validate required fields
			if (null == request.transId) throw new ArgumentException("transactionId cannot be null");

			//validate not-required fields
		}
	}
#pragma warning restore 1591
}