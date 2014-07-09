namespace  authorizenet.apicore.controller.bases
{
    using System.Collections.Generic;

    /**
     * @author ramittal
     *
     */
#pragma warning disable 1591
    public interface IApiOperation<TQ, TS>  
        where TQ : authorizenet.apicore.contract.v1.ANetApiRequest 
        where TS : authorizenet.apicore.contract.v1.ANetApiResponse
	{
		void Execute (Environment environment);
		List<string> GetResults ();
        authorizenet.apicore.contract.v1.messageTypeEnum GetResultCode();
    }
#pragma warning restore 1591
}