using AuthorizeNet.Api.Contracts.V1;
namespace AuthorizeNet.Api.Controllers
{

#pragma warning disable 1591
	public class TransactionController
	//: ApiOperationBase<transactionRequestType, transactionResponse>
	{

		public TransactionController(transactionRequestType apiRequest)
		//: base(apiRequest)
		{
		}

		//override
		protected void ValidateRequest()
		{
			//var request = GetApiRequest();

			//validate required fields
			//if ( 0 == request.SearchType) throw new ArgumentException( "SearchType cannot be null");
			//if ( null == request.Paging) throw new ArgumentException("Paging cannot be null");

			//validate not-required fields
		}

		//protected override void BeforeExecute()
		protected void BeforeExecute()
		{
			//var request = GetApiRequest();
			//RequestFactoryWithSpecified.transactionRequestType(request);
		}
	}
#pragma warning restore 1591
}