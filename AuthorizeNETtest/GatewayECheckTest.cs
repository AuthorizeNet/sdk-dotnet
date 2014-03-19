using AuthorizeNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for GatewayECheckTest and is intended to contain all ECheck Unit Tests
    ///</summary>
    [TestClass()]
    public class GatewayECheckTest : BaseTest
    {
        /// <summary>
        /// AuthCap Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_AuthCap_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1|1|1|This transaction has been approved.||P|2207739411||AuthCap transaction approved testing|15.10|ECHECK|auth_capture||||||||||||||||||||||||||D05070D0B41BC42B614A666B05631712|||||||||||||XXXX3456|Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckRequest(EcheckType.WEB, (decimal) 15.10, "125000024", "123456", BankAccountType.Checking, "Bank of Seattle", "Sue Zhu", "1234");
            string description = "AuthCap transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);
            
            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// Auth Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_Auth_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1|1|1|This transaction has been approved.||P|2207740049||Auth transaction approved testing|15.11|ECHECK|auth_only||||||||||||||||||||||||||C1C4BF36B72CD3D6671063648096D7B7|||||||||||||XXXX3456|Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckAuthorizationRequest(EcheckType.WEB, (decimal)15.11, "125000024", "123456", BankAccountType.Checking, "Bank of Seattle", "Sue Zhu", "1234");
            string description = "Auth transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// Capture Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_Capture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1|1|1|This transaction has been approved.||P|2207740902||Capture transaction approved testing|15.12|ECHECK|capture_only||||||||||||||||||||||||||5ABF0956B17EC73A611AACF31C38E6C5|||||||||||||XXXX3456|Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckCaptureRequest("ASD123", EcheckType.WEB, (decimal)15.12, "125000024", "123456", BankAccountType.Checking, "Bank of Seattle", "Sue Zhu", "1234");
            string description = "Capture transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// PriorAuthCapture Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_PriorAuthCapture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)15.13;
            string transID = SendAuthOnly(amount + 1);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "1|1|1|This transaction has been approved.||P|2207741298||Auth transaction approved testing|15.13|ECHECK|prior_auth_capture|||||||||||||||||||||0.00|0.00|0.00|FALSE||8F5A7C40A02E09A38B2E00AAA0C0E821||||||||||||||Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckPriorAuthCaptureRequest(transID, amount);
            string description = "PriorAuthCapture transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// Credit Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_Credit_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string transID = "2207700297";

            string responseString = "1|1|1|This transaction has been approved.||P|2207741772||Credit transaction approved testing|5.14|CC|credit||||||||||||suzhu@visa.com||||||||||||||574B2D5282D8A2914AEB7272AECD4B71|||||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckCreditRequest(transID, (decimal)5.14, "3456");
            string description = "Credit transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// UnlinkedCredit Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_UnlinkedCredit_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "4|1|193|The transaction is currently under review.||P|2207750459||UnlinkedCredit transaction approved testing|15.15|ECHECK|||||||||||||||||||||||||||028371CBD0646BDA25EC4206BF2FC7A5|||||||||||||XXXX3456|Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckUnlinkedCreditRequest(EcheckType.WEB, (decimal)15.15, "125000024", "123456", BankAccountType.Checking, "Bank of Seattle", "Sue Zhu", "1234");
            request.DuplicateWindow = "0";
            string description = "UnlinkedCredit transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// Void Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_Void_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)15.16;
            string transID = SendAuthOnly(amount);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "1|1|1|This transaction has been approved.||P|2207750596||Void transaction approved testing|0.00|ECHECK|void||||||||||||||||||||||||||53BCDE1CC703090924414B0BE71F3D12||||||||||||||Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckVoidRequest(transID);
            string description = "Void transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        private string SendAuthOnly(decimal amount)
        {
            string responseString = "1|1|1|This transaction has been approved.||P|2207740049||Auth transaction approved testing|15.13|ECHECK|auth_only||||||||||||||||||||||||||C1C4BF36B72CD3D6671063648096D7B7|||||||||||||XXXX3456|Bank Account||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new EcheckAuthorizationRequest(EcheckType.WEB, amount, "125000024", "123456", BankAccountType.Checking, "Bank of Seattle", "Sue Zhu", "1234");
            string description = "Auth transaction approved testing";

            IGatewayResponse response = target.Send(request, description);

            if (response.Approved)
            {
                return response.TransactionID;
            }
            else
            {
                return "";
            }
        }
    }
}
