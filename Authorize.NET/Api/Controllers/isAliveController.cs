using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;

namespace AuthorizeNet.Api.Controllers
{
#pragma warning disable 1591
	public class IsAliveController : ApiOperationBase<ANetApiRequest, isAliveResponse>
	{

		public IsAliveController(ANetApiRequest apiRequest)
			: base(apiRequest)
		{
		}

		override protected void ValidateRequest()
		{
			var request = GetApiRequest();

		}

		protected override void BeforeExecute()
		{
			var request = GetApiRequest();
			RequestFactoryWithSpecified.IsAliveRequest(request);
		}
	}
#pragma warning restore 1591
}