using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace AuthorizeNet.Api.Controllers
{

#pragma warning disable 1591
	public class DeleteCustomerPaymentProfileController : ApiOperationBase<deleteCustomerPaymentProfileRequest, deleteCustomerPaymentProfileResponse>
	{

		public DeleteCustomerPaymentProfileController(deleteCustomerPaymentProfileRequest apiRequest) : base(apiRequest)
		{
		}

		override protected void ValidateRequest()
		{
			var request = GetApiRequest();

			//validate required fields
			//if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
			//if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");

			//validate not-required fields
		}
	}
#pragma warning restore 1591
}