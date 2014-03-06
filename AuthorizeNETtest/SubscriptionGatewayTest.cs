using AuthorizeNet;
using AuthorizeNet.APICore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for SubscriptionGatewayTest and is intended to contain all Subscription Unit Tests
    /// </summary>
    [TestClass()]
    public class SubscriptionGatewayTest : BaseTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes

        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
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
        /// CreateSubscription - success
        /// </summary>
        [TestMethod()]
        public void CreateSubscriptionTest()
        {
            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBCreateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><subscriptionId>2010573</subscriptionId></ARBCreateSubscriptionResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            SubscriptionGateway target = new SubscriptionGateway(apiLogin, transactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Subscrition Test", (decimal) 1.31,
                                                                                  12);
            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 3;
            subscription.CardExpirationYear = 16;

            Address billToAddress = new Address();
            billToAddress.First = "Sue";
            billToAddress.Last = "Zhu";
            subscription.BillingAddress = billToAddress;

            ISubscriptionRequest actual = null;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.CreateSubscription(subscription);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(subscription.Amount, actual.Amount);
            Assert.AreEqual(subscription.CardNumber, actual.CardNumber);
            Assert.AreEqual(subscription.SubscriptionName, actual.SubscriptionName);

            Assert.IsTrue(actual.SubscriptionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.SubscriptionID) == 2010573);
        }

        /// <summary>
        /// UpdateSubscription - success
        /// </summary>
        [TestMethod()]
        public void UpdateSubscriptionTest()
        {
            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            SubscriptionGateway target = new SubscriptionGateway(apiLogin, transactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscrition Test",
                                                                                  (decimal) 1.32, 12);
            subscription.SubscriptionID = "2010573";

            bool actual = false;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdateSubscription(subscription);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdateSubscription SingleDigitMonth - success
        /// </summary>
        [TestMethod()]
        public void UpdateSubscriptionTest_SingleDigitMonth()
        {
            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            SubscriptionGateway target = new SubscriptionGateway(apiLogin, transactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscrition Test",
                                                                                  (decimal) 1.32, 12);
            subscription.SubscriptionID = "2010573";

            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 4;
            subscription.CardExpirationYear = 16;

            bool actual = false;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdateSubscription(subscription);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdateSubscription Occurence Amount Changes - success
        /// </summary>
        [TestMethod()]
        public void UpdateSubscriptionTest_Occurence_Amount()
        {
            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            FakeRequestObject.ResponseString = responseString;

            string apiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            string transactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            SubscriptionGateway target = new SubscriptionGateway(apiLogin, transactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscrition Test",
                                                                                  (decimal) 1.33, 12);
            subscription.SubscriptionID = "2010573";

            subscription.BillingCycles = 15;

            bool actual = false;

            // if choose "USEFAKE", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdateSubscription(subscription);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }
    }
}
