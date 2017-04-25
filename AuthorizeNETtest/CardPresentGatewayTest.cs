using AuthorizeNet;
using NUnit.Framework;
using System;
using System.Configuration;

namespace AuthorizeNETtest
{
    /// <summary>
    ///This is a test class for CardPresentGatewayTest and is intended to contain all CardPresent Unit Tests
    ///</summary>
    [TestFixture()]
    public class CardPresentGatewayTest : BaseTest
    {
        /// <summary>
        /// Auth Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_Auth_Approved()
        {
            //check ApiLoginid / TransactionKey
            string sError = CheckApiLoginTransactionKey();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1.0|1|1|This transaction has been approved.|N8IV1Z|Y||2207395117|4BA6F435F8046E347710457856F3BAD1||||||||||||XXXX1111|Visa";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new CardPresentResponse(responseString.Split('|'));

            CardPresentGateway target = new CardPresentGateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CardPresentAuthorizationRequest((decimal)30.11, "4111111111111111", "02", "20");
            string description = "CP Auth transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);
            
            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }
        /// <summary>
        /// Capture Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_Capture_Approved()
        {
            //check ApiLoginid / TransactionKey
            string sError = CheckApiLoginTransactionKey();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)30.12;
            string authCode = SendAuthOnly(amount + 1, false);
            Assert.IsTrue(authCode.Trim().Length > 0);

            //start testing
            string responseString = "1.0|1|1|This transaction has been approved.||P||2207702802|9FE994E47A8F0F44552C5CA59D09BE79||||||||||||XXXX1111|Visa";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new CardPresentResponse(responseString.Split('|'));

            CardPresentGateway target = new CardPresentGateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CardPresentCaptureOnly(authCode, "4111111111111111", "0224", amount);
            string description = "CP Capture transaction approved testing";

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
        /// PriorAuthCap Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_PriorAuthCap_Approved()
        {
            //check ApiLoginid / TransactionKey
            string sError = CheckApiLoginTransactionKey();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = getValidAmount();
            string transID = SendAuthOnly(amount + 1, true);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "1.0|1|1|This transaction has been approved.||P||2207397444|37887CB02372C5074386923E7E33BB3C||||||||||||XXXX1111|Visa";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new CardPresentResponse(responseString.Split('|'));

            CardPresentGateway target = new CardPresentGateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CardPresentPriorAuthCapture(transID, amount);
            string description = "CP PriorAuthCap transaction approved testing";

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
        /// PriorAuthCap Transaction LessAmount - Failed
        /// </summary>
        [Test()]
        public void SendTest_PriorAuthCap_LessAmount_Failed()
        {
            //check ApiLoginid / TransactionKey
            string sError = CheckApiLoginTransactionKey();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = getValidAmount();
            string transID = SendAuthOnly(amount - 1, true);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "1.0|3|47|The amount requested for settlement cannot be greater than the original amount authorized.||P||0|723B86547E0A4F9D9A2293081DA46A70|||||||||||||Visa";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new CardPresentResponse(responseString.Split('|'));

            CardPresentGateway target = new CardPresentGateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CardPresentPriorAuthCapture(transID, amount);
            string description = "CP PriorAuthCap transaction approved testing";

            IGatewayResponse actual = target.Send(request, description);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) == 0);
        }

        private string SendAuthOnly(decimal amount, bool returnTransID)
        {
            string responseString = "1.0|1|1|This transaction has been approved.|N8IV1Z|Y||2207395117|4BA6F435F8046E347710457856F3BAD1||||||||||||XXXX1111|Visa";
            LocalRequestObject.ResponseString = responseString;

            CardPresentGateway target = new CardPresentGateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CardPresentAuthorizationRequest(amount, "4111111111111111", "02", "20");
            string description = "CP Auth transaction approved testing";

            IGatewayResponse response = target.Send(request, description);
            if (response.Approved)
            {
                if (returnTransID)
                {
                    return response.TransactionID;
                }
                else
                {
                    return response.AuthorizationCode;
                }
            }
            else
            {
                return "";
            }
        }
    }
}
