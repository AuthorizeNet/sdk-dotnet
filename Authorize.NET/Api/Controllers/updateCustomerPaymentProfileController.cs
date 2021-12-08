using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
namespace AuthorizeNet.Api.Controllers
{

#pragma warning disable 1591
	public class UpdateCustomerPaymentProfileController : ApiOperationBase<UpdateCustomerPaymentProfileRequest, UpdateCustomerPaymentProfileResponse>
	{

		public UpdateCustomerPaymentProfileController(UpdateCustomerPaymentProfileRequest apiRequest) : base(apiRequest)
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

		protected override void BeforeExecute()
		{
			var request = GetApiRequest();
			RequestFactoryWithSpecified.UpdateCustomerPaymentProfileRequest(request);
		}
	}
#pragma warning restore 1591
}