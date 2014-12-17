﻿using AuthorizeNet;
using NUnit.Framework;
using System.Configuration;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for GatewayTest and is intended to contain all GatewayTest Unit Tests
    ///</summary>
    [TestFixture()]
    public class GatewayTest : BaseTest
    {
        /// <summary>
        /// AuthCap Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_AuthCap_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1|1|1|This transaction has been approved.|7339F5|Y|2207176015||testing|20.10|CC|auth_capture||||||||||||||||||||||||||7639D026F54F4DF70EA3F7DE5A350929||2|||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new AuthorizationRequest("5424000000000015", "0224", (decimal)20.10, "AuthCap transaction approved testing", true);
            string description = "AuthCap transaction approved testing";
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
        /// AuthCap Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_AuthCap_Approved_CustomerIP()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1|1|1|This transaction has been approved.|7339F5|Y|2207176015||testing|20.10|CC|auth_capture||||||||||||||||||||||||||7639D026F54F4DF70EA3F7DE5A350929||2|||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new AuthorizationRequest("4111111111111111", "0224", (decimal)20.10, "AuthCap transaction approved testing", true);
            string description = "AuthCap transaction approved testing";
            request.CustId = "CID1234";
            request.CustomerIp = "CIP456789";
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
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)20.12;
            string authCode = SendAuthOnly(amount + 1, false);
            Assert.IsTrue(authCode.Trim().Length > 0);

            //start testing
            string responseString = "1|1|1|This transaction has been approved.|X297JA|P|2207700989||Capture transaction approved testing|20.12|CC|capture_only||||||||||||||||||||||||||13E5B43A154FFEDF556537BEA77BAB80|||||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CaptureRequest(authCode, "4111111111111111", "0224", amount);
            string description = "Capture transaction approved testing";

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
        /// PriorAuthCapture Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_PriorAuthCapture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)20.13;
            string transID = SendAuthOnly(amount + 1, true);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "1|1|1|This transaction has been approved.|P9A0ET|P|2207700131||PriorAuthCapture transaction approved testing|20.13|CC|prior_auth_capture||||||||||||||||||||||||||4C66E6649DF48EDEBBD917A1656CD68C|||||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new PriorAuthCaptureRequest(amount, transID);
            request.DuplicateWindow = "0";
            string description = "PriorAuthCapture transaction approved testing";

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
        /// Credit Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_Credit_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string transID = "2207700297";

            string responseString = "1|1|1|This transaction has been approved.||P|2207741772||Credit transaction approved testing|6.14|CC|credit||||||||||||suzhu@visa.com||||||||||||||574B2D5282D8A2914AEB7272AECD4B71|||||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new CreditRequest(transID, (decimal)6.14, "1111");
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
        [Test()]
        public void SendTest_UnlinkedCredit_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "1|1|1|This transaction has been approved.||P|2207179642||UnlinkedCredit transaction approved testing|20.15|CC|credit||||||||||||||||||||||||||1F01159A9561E77E4AD004FF64069B05|||||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|'));

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new UnlinkedCredit((decimal)20.15, "4111111111111111", "0224");
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
        /// UnlinkedCredit Transaction - InvalidExpirationDate
        /// </summary>
        [Test()]
        public void SendTest_UnlinkedCredit_InvalidExpirationDate()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "3|1|7|Credit card expiration date is invalid.||P|0||UnlinkedCredit transaction approved testing|20.15|CC|credit||||||||||||||||||||||||||76688C1759F2A7C3616A595012F99289|||||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;            
            IGatewayResponse expected = new GatewayResponse(responseString.Split('|')); 
         
            Gateway target = new Gateway(ApiLogin, TransactionKey, true);
            IGatewayRequest request = new UnlinkedCredit((decimal)20.15, "4111111111111111", "24");
            request.DuplicateWindow = "0";
            string description = "UnlinkedCredit transaction InvalidExpirationDate testing";

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
            string responseString = "1|1|1|This transaction has been approved.|P9A0ET|Y|2207700131||AuthOnly transaction approved testing|11.21|CC|auth_only||||||||||||||||||||||||||C4DB0F58C8BE75212AB0261BF7F1BE21||2|||||||||||XXXX1111|Visa||||||||||||||||";
            LocalRequestObject.ResponseString = responseString;

            Gateway target = new Gateway(ApiLogin, TransactionKey, true);

            IGatewayRequest request = new AuthorizationRequest("4111111111111111", "0224", amount, "AuthOnly transaction approved testing", false);
            string description = "Auth only transaction approved testing";

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
