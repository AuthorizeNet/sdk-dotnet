using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuthorizeNet.APICore;

namespace AuthorizeNet {
    /// <summary>
    /// The gateway for requesting Reports from Authorize.Net
    /// </summary>
    
    //@deprecated since version 1.9.8  
    //@deprecated We have reorganized and simplified the Authorize.Net API to ease integration and to focus on merchants' needs.  
    //@deprecated We have deprecated AIM, ARB, CIM, and Reporting as separate options, in favor of AuthorizeNet::API.
    //@deprecated We have also deprecated SIM as a separate option, in favor of Accept Hosted. See https://developer.authorize.net/api/reference/features/accept_hosted.html for details on Accept Hosted.  
    //@deprecated For details on the deprecation and replacement of legacy Authorize.Net methods, visit https://developer.authorize.net/api/upgrade_guide/.   
    //@deprecated For AIM, refer examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/Reporting 
    [Obsolete("AuthorizeNetREPORTING is deprecated, use AuthorizeNet::API instead. For REPORTING, see examples in https://github.com/AuthorizeNet/sample-code-php/tree/master/Reporting.", false)]
    public class ReportingGateway : IReportingGateway {
    
        HttpXmlUtility _gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerGateway"/> class.
        /// </summary>
        /// <param name="apiLogin">The API login.</param>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="mode">Test or Live.</param>
        public ReportingGateway(string apiLogin, string transactionKey, ServiceMode mode) {
            
            if (mode == ServiceMode.Live) {
                _gateway = new HttpXmlUtility(ServiceMode.Live, apiLogin, transactionKey);
            } else {
                _gateway = new HttpXmlUtility(ServiceMode.Test, apiLogin, transactionKey);
            }
        }
    
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerGateway"/> class.
        /// </summary>
        /// <param name="apiLogin">The API login.</param>
        /// <param name="transactionKey">The transaction key.</param>
        public ReportingGateway(string apiLogin, string transactionKey) : this(apiLogin, transactionKey, ServiceMode.Test) { }
        
        /// <summary>
        /// Returns all Settled Batches for the last 30 days
        /// </summary>
        public List<Batch> GetSettledBatchList(bool includeStats) {
            var from = DateTime.Today.AddDays(-30);
            var to = DateTime.Today;
            return GetSettledBatchList(from, to, includeStats);
        }


        /// <summary>
        /// returns the most recent 1000 transactions that are unsettled
        /// </summary>
        /// <returns></returns>
        public List<Transaction> GetUnsettledTransactionList() {
            var response = (getUnsettledTransactionListResponse)_gateway.Send(new getUnsettledTransactionListRequest());
            return Transaction.NewListFromResponse(response.transactions);
        }

        /// <summary>
        /// Returns all Settled Batches for the last 30 days
        /// </summary>
        public List<Batch> GetSettledBatchList() {
            var from = DateTime.Today.AddDays(-30);
            var to = DateTime.Today;
            return GetSettledBatchList(from, to, false);
        }

        /// <summary>
        /// Returns batch settlements for the specified date range
        /// </summary>
        public List<Batch> GetSettledBatchList(DateTime from, DateTime to) {
            return GetSettledBatchList(from, to, false);
        }

        /// <summary>
        /// Returns charges and statistics for a given batch
        /// </summary>
        /// <param name="batchId">the batch id</param>
        /// <returns>a batch with statistics</returns>
        public List<Batch> GetBatchStatistics(string batchId)
        {
            var req = new getBatchStatisticsRequest();
            req.batchId = batchId;
            var response = (getBatchStatisticsResponse)_gateway.Send(req);
            return Batch.NewFromResponse(response);
        }

        /// <summary>
        /// Returns batch settlements for the specified date range
        /// </summary>
        public List<Batch> GetSettledBatchList(DateTime from, DateTime to, bool includeStats) {
            var req = new getSettledBatchListRequest();

            req.firstSettlementDate = from.ToUniversalTime();
            req.lastSettlementDate = to.ToUniversalTime();

            req.firstSettlementDateSpecified = true;
            req.lastSettlementDateSpecified = true;
            if (includeStats) {
                req.includeStatistics = true;
                req.includeStatisticsSpecified = true;
            }
            var response = (getSettledBatchListResponse)_gateway.Send(req);

            return Batch.NewFromResponse(response);
        }

        /// <summary>
        /// Returns all transaction within a particular batch
        /// </summary>
        public List<Transaction> GetTransactionList(string batchId) {
            var req = new getTransactionListRequest();
            req.batchId = batchId;
            var response = (getTransactionListResponse)_gateway.Send(req);
            return Transaction.NewListFromResponse(response.transactions);
        }
        /// <summary>
        /// Returns Transaction details for a given transaction ID (transid)
        /// </summary>
        /// <param name="transactionID"></param>
        public Transaction GetTransactionDetails(string transactionID){
            var req = new getTransactionDetailsRequest();
            req.transId = transactionID;
            var response = (getTransactionDetailsResponse) _gateway.Send(req);
            return Transaction.NewFromResponse(response.transaction);
        }

        /// <summary>
        /// Returns all transactions for the last 30 days
        /// </summary>
        /// <returns></returns>
        public List<Transaction> GetTransactionList() {
            return GetTransactionList(DateTime.Today.AddDays(-30), DateTime.Today);

        }

        /// <summary>
        /// Returns all transactions for a given time period. This can result in a number of calls to the API
        /// </summary>
        public List<Transaction> GetTransactionList(DateTime from, DateTime to) {

            var batches = GetSettledBatchList(from, to);
            var result = new List<Transaction>();
            foreach (var batch in batches) {
                result.AddRange(GetTransactionList(batch.ID.ToString()));
            }
            return result;
        }

        /// <summary>
        /// Returns all transactions for a given customerProfileId
        /// </summary>
        public List<Transaction> GetTransactionList(string customerProfileId, string customerPaymentProfileId)
        {
            var request = new getTransactionListForCustomerRequest();
            request.customerProfileId = customerProfileId;
            request.customerPaymentProfileId = customerPaymentProfileId;
            
            var response = (getTransactionListResponse)_gateway.Send(request);
            return Transaction.NewListFromResponse(response.transactions);
        } 
    }
}
