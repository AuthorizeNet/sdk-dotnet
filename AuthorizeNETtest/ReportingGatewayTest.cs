using AuthorizeNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for ReportingGatewayTest and is intended to contain all ReportingGatewayTest Unit Tests
    /// </summary>
    [TestClass()]
    public class ReportingGatewayTest : BaseTest
    {
        /// <summary>
        /// GetBatchStatistics - success
        /// </summary>
        [TestMethod()]
        public void Reporting_GetBatchStatisticsTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getBatchStatisticsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><batch><batchId>3260033</batchId><settlementTimeUTC>2014-03-14T15:20:05Z</settlementTimeUTC><settlementTimeLocal>2014-03-14T08:20:05</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><statistics><statistic><accountType>Visa</accountType><chargeAmount>6.98</chargeAmount><chargeCount>2</chargeCount><refundAmount>0.00</refundAmount><refundCount>0</refundCount><voidCount>0</voidCount><declineCount>0</declineCount><errorCount>0</errorCount></statistic></statistics></batch></getBatchStatisticsResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string batchId = "3260033";
            var actual = target.GetBatchStatistics(batchId);

            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(batchId, actual[0].ID);
            Assert.AreEqual("creditCard", actual[0].PaymentMethod);
            Assert.AreEqual("settledSuccessfully", actual[0].State);
            Assert.AreEqual(1, actual[0].Charges.Count);
            Assert.AreEqual((decimal)6.98, actual[0].Charges[0].Amount);
            Assert.AreEqual("Visa", actual[0].Charges[0].CardType);
        }

        /// <summary>
        /// GetBatchStatistics - NoRecord
        /// </summary>
        [TestMethod()]
        public void Reporting_GetBatchStatisticsTest_NoRecord()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getBatchStatisticsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00004</code><text>No records found.</text></message></messages></getBatchStatisticsResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string batchId = "326003333";
            var actual = target.GetBatchStatistics(batchId);

            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        /// GetTransactionDetails - Access Denied
        /// The default setup for the sandbox testing account will get an AccessDenied error.  
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionDetailsTest_AccessDenied()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionDetailsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Error</resultCode><message><code>E00011</code><text>Access denied. You do not have permissions to call the Transaction Details API.</text></message></messages></getTransactionDetailsResponse>";
            LocalRequestObject.ResponseString = responseString;
            
            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string transId = "2210248566";
            Transaction actual = null;
            string sErr = "";

            // if choose "USELOCAL", the test should generate Access Denied error.
            try
            {
                actual = target.GetTransactionDetails(transId);
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNull(actual);
            Assert.AreEqual(sErr, "Error processing request: E00011 - Access denied. You do not have permissions to call the Transaction Details API.");
        }

        /// <summary>
        /// GetTransactionDetails - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionDetailsTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionDetailsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transaction><transId>2210248566</transId><submitTimeUTC>2014-04-07T08:53:45.063Z</submitTimeUTC><submitTimeLocal>2014-04-07T01:53:45.063</submitTimeLocal><transactionType>authCaptureTransaction</transactionType><transactionStatus>settledSuccessfully</transactionStatus><responseCode>1</responseCode><responseReasonCode>1</responseReasonCode><responseReasonDescription>Approval</responseReasonDescription><authCode>9QW0L9</authCode><AVSResponse>Y</AVSResponse><batch><batchId>3323130</batchId><settlementTimeUTC>2014-04-07T15:20:19.703Z</settlementTimeUTC><settlementTimeLocal>2014-04-07T08:20:19.703</settlementTimeLocal><settlementState>settledSuccessfully</settlementState></batch><authAmount>1.31</authAmount><settleAmount>1.31</settleAmount><taxExempt>false</taxExempt><payment><creditCard><cardNumber>XXXX1111</cardNumber><expirationDate>XXXX</expirationDate><cardType>Visa</cardType></creditCard></payment><customer><type>individual</type><email>suzhu@visa.com</email></customer><billTo><firstName>Sue</firstName><lastName>Zhu</lastName></billTo><recurringBilling>true</recurringBilling></transaction></getTransactionDetailsResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string transId = "2210248566";
            Transaction actual = null;
            string sErr = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.GetTransactionDetails(transId);
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.AuthorizationAmount, (decimal)1.31);
            Assert.AreEqual(actual.BatchSettlementState, "settledSuccessfully");
            Assert.AreEqual(actual.CardType, "Visa");
            Assert.AreEqual(actual.ResponseReason, "Approval");
            Assert.AreEqual(actual.SettleAmount, (decimal)1.31);
            Assert.AreEqual(actual.Status, "settledSuccessfully");
            Assert.AreEqual(actual.TransactionType, "authCaptureTransaction");
            Assert.AreEqual(actual.TransactionID, transId);
        }

        /// <summary>
        /// GetTransactionList - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionListTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2209067941</transId><submitTimeUTC>2014-03-21T23:16:25Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:16:25</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>3.99</settleAmount></transaction><transaction><transId>2209067934</transId><submitTimeUTC>2014-03-21T23:16:07Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:16:07</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>2.99</settleAmount></transaction><transaction><transId>2209067927</transId><submitTimeUTC>2014-03-21T23:15:44Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:15:44</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.99</settleAmount></transaction><transaction><transId>2209060796</transId><submitTimeUTC>2014-03-21T21:20:18Z</submitTimeUTC><submitTimeLocal>2014-03-21T14:20:18</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.99</settleAmount></transaction></transactions></getTransactionListResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            List<Transaction> actual = null;
            string batchId = "3282059";
            string sErr = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.GetTransactionList(batchId);
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Count, 4);
            Assert.AreEqual(actual[0].TransactionID, "2209067941");
            Assert.AreEqual(actual[1].TransactionID, "2209067934");
            Assert.AreEqual(actual[2].TransactionID, "2209067927");
            Assert.AreEqual(actual[3].TransactionID, "2209060796");
        }

        /// <summary>
        /// GetTransactionList DateRange - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionListTest_DateRange()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string[] responseStrings = new string[]
                {
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><getSettledBatchListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><batchList><batch><batchId>3321516</batchId><settlementTimeUTC>2014-04-06T15:20:51Z</settlementTimeUTC><settlementTimeLocal>2014-04-06T08:20:51</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod></batch><batch><batchId>3323130</batchId><settlementTimeUTC>2014-04-07T15:20:19Z</settlementTimeUTC><settlementTimeLocal>2014-04-07T08:20:19</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod></batch></batchList></getSettledBatchListResponse>",
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2210220767</transId><submitTimeUTC>2014-04-06T08:48:39Z</submitTimeUTC><submitTimeLocal>2014-04-06T01:48:39</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><firstName>Sue</firstName><lastName>Zhu</lastName><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.31</settleAmount></transaction></transactions></getTransactionListResponse>",
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2210248566</transId><submitTimeUTC>2014-04-07T08:53:45Z</submitTimeUTC><submitTimeLocal>2014-04-07T01:53:45</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><firstName>Sue</firstName><lastName>Zhu</lastName><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.31</settleAmount></transaction></transactions></getTransactionListResponse>"
                };
            LocalRequestObject.ResponseStrings = responseStrings;
            LocalRequestObject.ResponseStringCount = 0;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            List<Transaction> actual = null;
            string sErr = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.GetTransactionList(DateTime.Parse("4/6/2014"), DateTime.Parse("4/8/2014"));
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Count, 2);
            Assert.AreEqual(actual[0].TransactionID, "2210220767");
            Assert.AreEqual(actual[1].TransactionID, "2210248566");
        }

        /// <summary>
        /// GetUnsettledTransactionList - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetUnsettledTransactionListTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getUnsettledTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2208803820</transId><submitTimeUTC>2014-03-19T23:40:29Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:29</submitTimeLocal><transactionStatus>authorizedPendingCapture</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>21.12</settleAmount></transaction><transaction><transId>2208803814</transId><submitTimeUTC>2014-03-19T23:40:19Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:19</submitTimeLocal><transactionStatus>authorizedPendingCapture</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>26.12</settleAmount></transaction><transaction><transId>2208803809</transId><submitTimeUTC>2014-03-19T23:40:12Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:12</submitTimeLocal><transactionStatus>voided</transactionStatus><accountType>eCheck</accountType><accountNumber>XXXX3456</accountNumber><settleAmount>0.00</settleAmount></transaction><transaction><transId>2208803807</transId><submitTimeUTC>2014-03-19T23:40:11Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:11</submitTimeLocal><transactionStatus>underReview</transactionStatus><accountType>eCheck</accountType><accountNumber>XXXX3456</accountNumber><settleAmount>15.15</settleAmount></transaction><transaction><transId>2208803806</transId><submitTimeUTC>2014-03-19T23:40:07Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:07</submitTimeLocal><transactionStatus>capturedPendingSettlement</transactionStatus><accountType>eCheck</accountType><accountNumber>XXXX3456</accountNumber><settleAmount>15.13</settleAmount></transaction></transactions></getUnsettledTransactionListResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            List<Transaction> actual = null;
            string sErr = "";

            try
            {
                actual = target.GetUnsettledTransactionList();
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Count, 5);
            Assert.AreEqual(actual[0].TransactionID, "2208803820");
            Assert.AreEqual(actual[1].TransactionID, "2208803814");
            Assert.AreEqual(actual[2].TransactionID, "2208803809");
            Assert.AreEqual(actual[3].TransactionID, "2208803807");
            Assert.AreEqual(actual[4].TransactionID, "2208803806");
        }
    }
}
