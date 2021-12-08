using System.Collections.Generic;

namespace AuthorizeNet.Api.Controllers.Bases
{
	/**
     * @author ramittal
     *
     */
#pragma warning disable 1591
	public interface IApiOperation<TQ, TS>
		where TQ : Contracts.V1.ANetApiRequest
		where TS : Contracts.V1.ANetApiResponse
	{
		TS GetApiResponse();
		Contracts.V1.ANetApiResponse GetErrorResponse();
		TS ExecuteWithApiResponse(Environment environment = null);
		void Execute(Environment environment = null);
		Contracts.V1.messageTypeEnum GetResultCode();
		List<string> GetResults();
	}
#pragma warning restore 1591
}