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

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getBatchStatisticsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><batch><batchId>3260033</batchId><settlementTimeUTC>2014-03-14T15:20:05Z</settlementTimeUTC><settlementTimeLocal>2014-03-14T08:20:05</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product><statistics><statistic><accountType>Visa</accountType><chargeAmount>6.98</chargeAmount><chargeCount>2</chargeCount><refundAmount>0.00</refundAmount><refundCount>0</refundCount><voidCount>0</voidCount><declineCount>0</declineCount><errorCount>0</errorCount></statistic></statistics></batch></getBatchStatisticsResponse>";
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
            Assert.AreEqual(actual[0].MarketType, "eCommerce");
            Assert.AreEqual(actual[0].Product, "Card Not Present");
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
        /// GetBatchStatistics - Success
        /// </summary>
        [TestMethod()]
        public void Reporting_GetSettledBatchListTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getSettledBatchListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><batchList><batch><batchId>3319724</batchId><settlementTimeUTC>2014-04-05T15:17:40Z</settlementTimeUTC><settlementTimeLocal>2014-04-05T08:17:40</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3321516</batchId><settlementTimeUTC>2014-04-06T15:20:51Z</settlementTimeUTC><settlementTimeLocal>2014-04-06T08:20:51</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3323130</batchId><settlementTimeUTC>2014-04-07T15:20:19Z</settlementTimeUTC><settlementTimeLocal>2014-04-07T08:20:19</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3332321</batchId><settlementTimeUTC>2014-04-10T15:19:25Z</settlementTimeUTC><settlementTimeLocal>2014-04-10T08:19:25</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3338386</batchId><settlementTimeUTC>2014-04-12T15:15:33Z</settlementTimeUTC><settlementTimeLocal>2014-04-12T08:15:33</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3351463</batchId><settlementTimeUTC>2014-04-17T15:16:33Z</settlementTimeUTC><settlementTimeLocal>2014-04-17T08:16:33</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3357414</batchId><settlementTimeUTC>2014-04-19T15:18:22Z</settlementTimeUTC><settlementTimeLocal>2014-04-19T08:18:22</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3378742</batchId><settlementTimeUTC>2014-04-29T21:32:25Z</settlementTimeUTC><settlementTimeLocal>2014-04-29T14:32:25</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch><batch><batchId>3392423</batchId><settlementTimeUTC>2014-05-03T16:19:54Z</settlementTimeUTC><settlementTimeLocal>2014-05-03T09:19:54</settlementTimeLocal><settlementState>settledSuccessfully</settlementState><paymentMethod>creditCard</paymentMethod><marketType>eCommerce</marketType><product>Card Not Present</product></batch></batchList></getSettledBatchListResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);

            List<Batch> actual = null;
            string sErr = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error.
            try
            {
                actual = target.GetSettledBatchList();
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Count, 9);
            Assert.AreEqual(actual[0].ID, "3319724");
            Assert.AreEqual(actual[0].MarketType, "eCommerce");
            Assert.AreEqual(actual[0].Product, "Card Not Present");

            Assert.AreEqual(actual[1].ID, "3321516");
            Assert.AreEqual(actual[2].ID, "3323130");
            Assert.AreEqual(actual[3].ID, "3332321");
            Assert.AreEqual(actual[4].ID, "3338386");
            Assert.AreEqual(actual[5].ID, "3351463");
            Assert.AreEqual(actual[6].ID, "3357414");
            Assert.AreEqual(actual[7].ID, "3378742");
            Assert.AreEqual(actual[8].ID, "3392423");
        }

        /// <summary>
        /// GetBatchStatistics - No Record
        /// </summary>
        [TestMethod()]
        public void Reporting_GetSettledBatchListTest_NoRecord()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getSettledBatchListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00004</code><text>No records found.</text></message></messages></getSettledBatchListResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);

            List<Batch> actual = null;
            string sErr = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error.
            try
            {
                actual = target.GetSettledBatchList(DateTime.UtcNow, DateTime.UtcNow.AddDays(2));
            }
            catch (Exception e)
            {
                sErr = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.Count, 0);
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

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionDetailsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transaction><transId>2209067941</transId><submitTimeUTC>2014-03-21T23:16:25.797Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:16:25.797</submitTimeLocal><transactionType>authCaptureTransaction</transactionType><transactionStatus>settledSuccessfully</transactionStatus><responseCode>1</responseCode><responseReasonCode>1</responseReasonCode><responseReasonDescription>Approval</responseReasonDescription><authCode>UUV1S1</authCode><AVSResponse>Y</AVSResponse><cardCodeResponse>P</cardCodeResponse><batch><batchId>3282059</batchId><settlementTimeUTC>2014-03-22T15:21:44.343Z</settlementTimeUTC><settlementTimeLocal>2014-03-22T08:21:44.343</settlementTimeLocal><settlementState>settledSuccessfully</settlementState></batch><authAmount>3.99</authAmount><settleAmount>3.99</settleAmount><taxExempt>false</taxExempt><payment><creditCard><cardNumber>XXXX1111</cardNumber><expirationDate>XXXX</expirationDate><cardType>Visa</cardType></creditCard></payment><recurringBilling>false</recurringBilling><customerIP>10.1.186.51</customerIP><product>Card Not Present</product><marketType>eCommerce</marketType></transaction></getTransactionDetailsResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string transId = "2209067941";
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
            Assert.AreEqual(actual.AuthorizationAmount, (decimal)3.99);
            Assert.AreEqual(actual.BatchSettlementState, "settledSuccessfully");
            Assert.AreEqual(actual.CardType, "Visa");
            Assert.AreEqual(actual.ResponseReason, "Approval");
            Assert.AreEqual(actual.SettleAmount, (decimal)3.99);
            Assert.AreEqual(actual.Status, "settledSuccessfully");
            Assert.AreEqual(actual.TransactionType, "authCaptureTransaction");
            Assert.AreEqual(actual.TransactionID, transId);
            Assert.AreEqual(actual.MarketType, "eCommerce");
            Assert.AreEqual(actual.Product, "Card Not Present");
            Assert.IsNull(actual.eCheckBankAccount);
        }

        /// <summary>
        /// GetTransactionDetails WithSubscription - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionDetailsTest_WithSubscription()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionDetailsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transaction><transId>2210248566</transId><submitTimeUTC>2014-04-07T08:53:45.063Z</submitTimeUTC><submitTimeLocal>2014-04-07T01:53:45.063</submitTimeLocal><transactionType>authCaptureTransaction</transactionType><transactionStatus>settledSuccessfully</transactionStatus><responseCode>1</responseCode><responseReasonCode>1</responseReasonCode><subscription><id>2017665</id><payNum>2</payNum></subscription><responseReasonDescription>Approval</responseReasonDescription><authCode>9QW0L9</authCode><AVSResponse>Y</AVSResponse><batch><batchId>3323130</batchId><settlementTimeUTC>2014-04-07T15:20:19.703Z</settlementTimeUTC><settlementTimeLocal>2014-04-07T08:20:19.703</settlementTimeLocal><settlementState>settledSuccessfully</settlementState></batch><authAmount>1.31</authAmount><settleAmount>1.31</settleAmount><taxExempt>false</taxExempt><payment><creditCard><cardNumber>XXXX1111</cardNumber><expirationDate>XXXX</expirationDate><cardType>Visa</cardType></creditCard></payment><customer><type>individual</type><email>suzhu@visa.com</email></customer><billTo><firstName>Sue</firstName><lastName>Zhu</lastName></billTo><recurringBilling>true</recurringBilling></transaction></getTransactionDetailsResponse>";
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
            Assert.IsNotNull(actual.Subscription);
            Assert.AreEqual(actual.Subscription.ID, 2017665); 
            Assert.AreEqual(actual.Subscription.PayNum, 2);
            Assert.IsNull(actual.eCheckBankAccount);
        }

        /// <summary>
        /// GetTransactionDetails WithReturnedItems for eCheck - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionDetailsTest_WithReturnedItems()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <getTransactionDetailsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"> <messages> <resultCode>Ok</resultCode> <message> <code>I00001</code> <text>Successful.</text> </message> </messages> <transaction> <transId>2148382212</transId> <submitTimeUTC>2012-01-12T02:21:53.42Z</submitTimeUTC> <submitTimeLocal>2012-01-11T20:21:53.42</submitTimeLocal> <transactionType>authCaptureTransaction</transactionType> <transactionStatus>settledSuccessfully</transactionStatus> <responseCode>1</responseCode> <responseReasonCode>1</responseReasonCode> <responseReasonDescription>Approval</responseReasonDescription> <AVSResponse>P</AVSResponse> <batch> <batchId>10097820</batchId> <settlementTimeUTC>2012-01-12T02:21:55.237Z</settlementTimeUTC> <settlementTimeLocal>2012-01-11T20:21:55.237</settlementTimeLocal> <settlementState>settledSuccessfully</settlementState> </batch> <order> <invoiceNumber>QaInv01waegphqvlcyj</invoiceNumber> </order> <authAmount>12.10</authAmount> <settleAmount>12.10</settleAmount> <taxExempt>false</taxExempt> <payment> <bankAccount> <routingNumber>XXXX0204</routingNumber> <accountNumber>XXXX2152</accountNumber> <nameOnAccount>Account Owner</nameOnAccount> <echeckType>TEL</echeckType> </bankAccount> </payment> <recurringBilling>false</recurringBilling> <returnedItems> <returnedItem> <id>2148382213</id> <dateUTC>2012-01-12T02:22:15Z</dateUTC> <dateLocal>2012-01-11T20:22:15</dateLocal> <code>R03</code> <description>No account/unable to locate account</description> </returnedItem> </returnedItems> </transaction> </getTransactionDetailsResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string transId = "2148382212";
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
            Assert.AreEqual(actual.AuthorizationAmount, (decimal)12.10);
            Assert.AreEqual(actual.BatchSettlementState, "settledSuccessfully");
            Assert.IsNull(actual.CardType);
            Assert.IsNotNull(actual.eCheckBankAccount);
            Assert.AreEqual(actual.eCheckBankAccount.echeckType, EcheckType.TEL);

            Assert.AreEqual(actual.ResponseReason, "Approval");
            Assert.AreEqual(actual.SettleAmount, (decimal)12.10);
            Assert.AreEqual(actual.Status, "settledSuccessfully");
            Assert.AreEqual(actual.TransactionType, "authCaptureTransaction");
            Assert.AreEqual(actual.TransactionID, transId);


            Assert.AreEqual(actual.HasReturnedItems, NullableBooleanEnum.True);
            Assert.IsNotNull(actual.ReturnedItems);
            Assert.AreEqual(actual.ReturnedItems.Length, 1);
            Assert.AreEqual(actual.ReturnedItems[0].id, "2148382213");
            Assert.AreEqual(actual.ReturnedItems[0].code, "R03");
            Assert.AreEqual(actual.ReturnedItems[0].description, "No account/unable to locate account");
        }

        /// <summary>
        /// GetTransactionDetails WithSolutionID - Success
        /// The default setup for the sandbox testing account will get an AccessDenied error. 
        /// The "Transaction Details API" permission needs to be granded to the account 
        /// by logging to https://sandbox.authorize.net/.  
        /// Then going to Account / Settings and clicking on the Transaction Details API link.
        /// </summary>
        [TestMethod()]
        public void Reporting_GetTransactionDetailsTest_WithSolutionID()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?> <getTransactionDetailsResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"> <messages> <resultCode>Ok</resultCode> <message> <code>I00001</code> <text>Successful.</text> </message> </messages> <transaction> <transId>2148855368</transId> <submitTimeUTC>2014-04-26T00:23:36.98Z</submitTimeUTC> <submitTimeLocal>2014-04-25T19:23:36.98</submitTimeLocal> <transactionType>authCaptureTransaction</transactionType> <transactionStatus>settledSuccessfully</transactionStatus> <responseCode>1</responseCode> <responseReasonCode>1</responseReasonCode> <responseReasonDescription>Approval</responseReasonDescription> <AVSResponse>P</AVSResponse> <batch> <batchId>10151391</batchId> <settlementTimeUTC>2014-04-26T00:24:00.99Z</settlementTimeUTC> <settlementTimeLocal>2014-04-25T19:24:00.99</settlementTimeLocal> <settlementState>settledSuccessfully</settlementState> </batch> <order> <invoiceNumber>QaInv01sjijwtbpomd</invoiceNumber> </order> <authAmount>1.00</authAmount> <settleAmount>1.00</settleAmount> <taxExempt>false</taxExempt> <payment> <bankAccount> <routingNumber>XXXX0505</routingNumber> <accountNumber>XXXX7120</accountNumber> <nameOnAccount>Account Owner</nameOnAccount> <echeckType>WEB</echeckType> </bankAccount> </payment> <recurringBilling>false</recurringBilling> <solution> <id>A1000002</id> <name>Miva Merchant 5.5</name> </solution> </transaction> </getTransactionDetailsResponse>";
            LocalRequestObject.ResponseString = responseString;

            ReportingGateway target = new ReportingGateway(ApiLogin, TransactionKey);
            string transId = "2148855368";
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
            Assert.AreEqual(actual.AuthorizationAmount, (decimal)1.00);
            Assert.AreEqual(actual.BatchSettlementState, "settledSuccessfully");
            Assert.IsNull(actual.CardType);
            Assert.IsNotNull(actual.eCheckBankAccount);
            Assert.AreEqual(actual.eCheckBankAccount.echeckType, EcheckType.WEB);

            Assert.AreEqual(actual.ResponseReason, "Approval");
            Assert.AreEqual(actual.SettleAmount, (decimal)1.00);
            Assert.AreEqual(actual.Status, "settledSuccessfully");
            Assert.AreEqual(actual.TransactionType, "authCaptureTransaction");
            Assert.AreEqual(actual.TransactionID, transId);


            Assert.IsNotNull(actual.Solution);
            Assert.AreEqual(actual.Solution.id, "A1000002");
            Assert.AreEqual(actual.Solution.name, "Miva Merchant 5.5"); 
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

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2209067941</transId><submitTimeUTC>2014-03-21T23:16:25Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:16:25</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>3.99</settleAmount><marketType>eCommerce</marketType><product>Card Not Present</product></transaction><transaction><transId>2209067934</transId><submitTimeUTC>2014-03-21T23:16:07Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:16:07</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>2.99</settleAmount><marketType>eCommerce</marketType><product>Card Not Present</product></transaction><transaction><transId>2209067927</transId><submitTimeUTC>2014-03-21T23:15:44Z</submitTimeUTC><submitTimeLocal>2014-03-21T16:15:44</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.99</settleAmount><marketType>eCommerce</marketType><product>Card Not Present</product></transaction><transaction><transId>2209060796</transId><submitTimeUTC>2014-03-21T21:20:18Z</submitTimeUTC><submitTimeLocal>2014-03-21T14:20:18</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.99</settleAmount><marketType>eCommerce</marketType><product>Card Not Present</product></transaction></transactions></getTransactionListResponse>";
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
            Assert.AreEqual(actual[0].DateSubmitted, DateTime.Parse("3/21/2014 11:16:25 PM"));
            Assert.AreEqual(actual[0].Status, "settledSuccessfully");
            Assert.AreEqual(actual[0].CardType, "Visa");
            Assert.AreEqual(actual[0].CardNumber, "XXXX1111");
            Assert.AreEqual(actual[0].SettleAmount, (decimal)3.99);
            Assert.AreEqual(actual[0].MarketType, "eCommerce");
            Assert.AreEqual(actual[0].Product, "Card Not Present");
            Assert.IsNull(actual[0].MobileDeviceID);
            Assert.IsNull(actual[0].Subscription);
            Assert.AreEqual(actual[0].HasReturnedItems, NullableBooleanEnum.Null);

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
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2210220767</transId><submitTimeUTC>2014-04-06T08:48:39Z</submitTimeUTC><submitTimeLocal>2014-04-06T01:48:39</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><firstName>Sue</firstName><lastName>Zhu</lastName><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.31</settleAmount><subscription><id>2016601</id><payNum>2</payNum></subscription></transaction></transactions></getTransactionListResponse>",
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?><getTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2210248566</transId><submitTimeUTC>2014-04-07T08:53:45Z</submitTimeUTC><submitTimeLocal>2014-04-07T01:53:45</submitTimeLocal><transactionStatus>settledSuccessfully</transactionStatus><firstName>Sue</firstName><lastName>Zhu</lastName><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>1.31</settleAmount><subscription><id>2017665</id><payNum>2</payNum></subscription></transaction></transactions></getTransactionListResponse>"
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

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getUnsettledTransactionListResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><transactions><transaction><transId>2208803820</transId><submitTimeUTC>2014-03-19T23:40:29Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:29</submitTimeLocal><transactionStatus>authorizedPendingCapture</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>21.12</settleAmount><marketType>eCommerce</marketType><product>Card Not Present</product></transaction><transaction><transId>2208803814</transId><submitTimeUTC>2014-03-19T23:40:19Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:19</submitTimeLocal><transactionStatus>authorizedPendingCapture</transactionStatus><accountType>Visa</accountType><accountNumber>XXXX1111</accountNumber><settleAmount>26.12</settleAmount><marketType>eCommerce</marketType><product>Card Not Present</product></transaction><transaction><transId>2208803809</transId><submitTimeUTC>2014-03-19T23:40:12Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:12</submitTimeLocal><transactionStatus>voided</transactionStatus><accountType>eCheck</accountType><accountNumber>XXXX3456</accountNumber><settleAmount>0.00</settleAmount><hasReturnedItems>false</hasReturnedItems></transaction><transaction><transId>2208803807</transId><submitTimeUTC>2014-03-19T23:40:11Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:11</submitTimeLocal><transactionStatus>underReview</transactionStatus><accountType>eCheck</accountType><accountNumber>XXXX3456</accountNumber><settleAmount>15.15</settleAmount><hasReturnedItems>true</hasReturnedItems></transaction><transaction><transId>2208803806</transId><submitTimeUTC>2014-03-19T23:40:07Z</submitTimeUTC><submitTimeLocal>2014-03-19T16:40:07</submitTimeLocal><transactionStatus>capturedPendingSettlement</transactionStatus><accountType>eCheck</accountType><accountNumber>XXXX3456</accountNumber><settleAmount>15.13</settleAmount><hasReturnedItems>false</hasReturnedItems></transaction></transactions></getUnsettledTransactionListResponse>";
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
            Assert.AreEqual(actual[0].MarketType, "eCommerce");
            Assert.AreEqual(actual[0].Product, "Card Not Present");
            Assert.IsNull(actual[0].MobileDeviceID);
            Assert.IsNull(actual[0].Subscription);
            Assert.AreEqual(actual[0].HasReturnedItems, NullableBooleanEnum.Null);

            Assert.AreEqual(actual[1].TransactionID, "2208803814");

            Assert.AreEqual(actual[2].TransactionID, "2208803809");
            Assert.IsNull(actual[2].MarketType);
            Assert.IsNull(actual[2].Product);
            Assert.IsNull(actual[2].MobileDeviceID);
            Assert.IsNull(actual[2].Subscription);
            Assert.AreEqual(actual[2].HasReturnedItems, NullableBooleanEnum.False);

            Assert.AreEqual(actual[3].TransactionID, "2208803807");
            Assert.IsNull(actual[3].MarketType);
            Assert.IsNull(actual[3].Product);
            Assert.IsNull(actual[3].MobileDeviceID);
            Assert.IsNull(actual[3].Subscription);
            Assert.AreEqual(actual[3].HasReturnedItems, NullableBooleanEnum.True);

            Assert.AreEqual(actual[4].TransactionID, "2208803806");
        }
    }
}
