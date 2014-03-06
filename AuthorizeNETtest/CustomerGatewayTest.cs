using AuthorizeNet;
using AuthorizeNet.APICore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for CustomerGatewayTest and is intended to contain all CustomerGateway Unit Tests
    /// </summary>
    [TestClass()]
    public class CustomerGatewayTest : BaseTest
    {
        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void CustomerGatewayTestInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        /// CreateCustomer - success
        /// </summary>
        [TestMethod()]
        public void CreateCustomerTest()
        {
            string responseString ="<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>24231938</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);
            string email = "suzhu@visa.com";
            string description = "CreateCustomerTest Success";
            Customer expected = new Customer()
                {
                    Email = email,
                    Description = description
                };
            Customer actual = null;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.CreateCustomer(email, description);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(actual.ID.Trim().Length > 0);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ProfileID));
            Assert.IsTrue(actual.ProfileID.Trim().Length > 0);
        }

        /// <summary>
        /// UpdateCustomer - successful
        /// </summary>
        [TestMethod()]
        public void UpdateCustomerTest()
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerProfileResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            Customer customer = new Customer()
            {
                ID = "",
                ProfileID = "24231938",
                Email = "suzhu@visa.com",
                Description = "UpdateCustomerTest Success"
            };
            bool actual = false;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdateCustomer(customer);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// AddCreditCard - success
        /// </summary>
        [TestMethod()]
        public void AddCreditCardTest()
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            string profileID = "24231938";
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = 16;

            string expected = "22219473";
            string actual = "";

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AddCreditCard set validationMode - success
        /// </summary>
        [TestMethod()]
        public void AddCreditCardTest_ValidationMode()
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey, ServiceMode.Test);

            string profileID = "24231938";
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = 16;

            string expected = "22219473";
            string actual = "";

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// Minimum parameters to ensure a successful response
        /// </summary>
        [TestMethod()]
        public void UpdatePaymentProfileMinTest()
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            string profileID = "24231938";
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = "22219473";
            profile.CardNumber = "4111111111111111";
            profile.CardExpiration = "2016-02";

            bool actual = false;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(profileID, profile);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void AuthorizeAndCaptureTest()
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2C99N3,Y,2207640586,,,25.10,CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C40BBCC10984A7A95471323B34FD4FFB,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            FakeRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";
            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = (decimal) 25.10;

            IGatewayResponse actual = null;
            
            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

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
        /// AuthorizeAndCapture Transaction with Invoice, Description and PONumber - Approved
        /// </summary>
        [TestMethod()]
        public void AuthorizeAndCaptureTest_InvoiceDescriptionPONumber()
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,Q5G0UI,Y,2207641147,Invoice#123,Testing InvoiceDescriptionPONumber,25.10,CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,PO23456,BEEEB7C9F2F22B9955338A7E19427369,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            FakeRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";
            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = (decimal)25.10;
            order.InvoiceNumber = "Invoice#123";
            order.Description = "Testing InvoiceDescriptionPONumber";
            order.PONumber = "PO23456";

            IGatewayResponse actual = null;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);
            Assert.AreEqual(expected.InvoiceNumber, actual.InvoiceNumber);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }


        /// <summary>
        /// Capture Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_Capture_Approved()
        {
            //setup
            decimal amount = (decimal)25.12;
            string authCode = SendAuthOnly(amount + 1, false);
            Assert.IsTrue(authCode.Trim().Length > 0);

            //start testing
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,P,2207702175,,,25.12,CC,capture_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,5BB96CB66C1E0BCE123915E970D70166,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            FakeRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";

            IGatewayResponse actual = null;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.Capture(profileID, paymentProfileID, "", amount, authCode);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

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
        [TestMethod()]
        public void SendTest_PriorAuthCapture_Approved()
        {
            //setup
            decimal amount = (decimal)25.13;
            string transID = SendAuthOnly(amount + 1, true);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,MFSOM8,P,2207702374,,,25.13,CC,prior_auth_capture,,,,,,,,,,,,,,,,,,,,,,,,,,E0DF3A88533C1F9CBE3B55159C514513,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            FakeRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            IGatewayResponse actual = null;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.PriorAuthCapture(transID, amount);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        private string SendAuthOnly(decimal amount, bool returnTransID)
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,Y,2207702136,,,11.21,CC,auth_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C8E9860C9B9DF58A73FFD9D7A8BFB82F,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            CustomerGateway target = new CustomerGateway(apiLogin, transactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";

            IGatewayResponse response = null;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                response = target.Authorize(profileID, paymentProfileID, amount);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            if (response != null && response.Approved)
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
