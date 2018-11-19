using System;
namespace AuthorizeNet {

    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For AIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/Reporting 
    [Obsolete("AuthorizeNetREPORTING is deprecated, use AuthorizeNet::API instead. For REPORTING, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/Reporting.", false)]
    public interface IReportingGateway {
        System.Collections.Generic.List<AuthorizeNet.Batch> GetBatchStatistics(string batchId);
        System.Collections.Generic.List<AuthorizeNet.Batch> GetSettledBatchList(bool includeStats);
        System.Collections.Generic.List<AuthorizeNet.Batch> GetSettledBatchList(DateTime from, DateTime to, bool includeStats);
        System.Collections.Generic.List<AuthorizeNet.Batch> GetSettledBatchList(DateTime from, DateTime to);
        System.Collections.Generic.List<AuthorizeNet.Batch> GetSettledBatchList();
        AuthorizeNet.Transaction GetTransactionDetails(string transactionID);
        System.Collections.Generic.List<AuthorizeNet.Transaction> GetTransactionList(DateTime from, DateTime to);
        System.Collections.Generic.List<AuthorizeNet.Transaction> GetTransactionList();
        System.Collections.Generic.List<AuthorizeNet.Transaction> GetTransactionList(string batchId);
        System.Collections.Generic.List<AuthorizeNet.Transaction> GetUnsettledTransactionList();
    }
}
