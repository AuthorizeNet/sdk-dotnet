namespace AuthorizeNet.Api.Controllers.Bases
{
    using System.Collections.Generic;

    /**
     * @author ramittal
     *
     */
#pragma warning disable 1591
    public interface IApiOperation<TQ, TS>  
        where TQ : AuthorizeNet.Api.Contracts.V1.ANetApiRequest 
        where TS : AuthorizeNet.Api.Contracts.V1.ANetApiResponse
	{
        TS GetApiResponse();
        AuthorizeNet.Api.Contracts.V1.ANetApiResponse GetErrorResponse();
        TS ExecuteWithApiResponse(AuthorizeNet.Environment environment = null);
        void Execute(AuthorizeNet.Environment environment = null);
        AuthorizeNet.Api.Contracts.V1.messageTypeEnum GetResultCode();
        List<string> GetResults();
    }
#pragma warning restore 1591
}