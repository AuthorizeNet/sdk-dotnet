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
    }
}
