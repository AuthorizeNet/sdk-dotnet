namespace AuthorizeNet.ApiCore.Controllers.Bases
{
    using System.Collections.Generic;

    /**
     * @author ramittal
     *
     */
#pragma warning disable 1591
    public interface IApiOperation<TQ, TS>  
        where TQ : AuthorizeNet.ApiCore.Contracts.V1.ANetApiRequest 
        where TS : AuthorizeNet.ApiCore.Contracts.V1.ANetApiResponse
	{
        void Execute();
        void Execute(Environment environment);
		List<string> GetResults ();
        AuthorizeNet.ApiCore.Contracts.V1.messageTypeEnum GetResultCode();
    }
#pragma warning restore 1591
}