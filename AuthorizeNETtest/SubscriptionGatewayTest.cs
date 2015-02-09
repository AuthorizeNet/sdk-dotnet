using AuthorizeNet;
using AuthorizeNet.APICore;
using NUnit.Framework;
using System;
using System.Configuration;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for SubscriptionGatewayTest and is intended to contain all Subscription Unit Tests
    /// </summary>
    [TestFixture]
    public class SubscriptionGatewayTest : BaseTest
    {
        /// <summary>
        /// CreateSubscription - success
        /// </summary>
        [Test]
        public void CreateSubscriptionTest()
        {
            var random = new Random();
            var counter = random.Next(1, (int)(Math.Pow(2, 24)));
            const int maxSubscriptionAmount = 1000; //214747;
            var amount = new decimal(counter > maxSubscriptionAmount ? (counter % maxSubscriptionAmount) : counter);
            var email = string.Format("user.{0}@authorize.net", counter);

            //check login / password
            var sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBCreateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><subscriptionId>2010573</subscriptionId></ARBCreateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var target = new SubscriptionGateway(ApiLogin, TransactionKey);

            var billToAddress = new Address { First = "SomeOneCool", Last = "MoreCoolPerson" };
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly(email, "ARB Subscription Test", amount, 1);
            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 3;
            subscription.CardExpirationYear = 16;
            subscription.BillingAddress = billToAddress;

            ISubscriptionRequest actual = null;

            try
            {
                actual = target.CreateSubscription(subscription);
            }
            catch (Exception e)
            {
                string s = e.Message;
                Assert.Fail("Exception in processing");
            }

            Assert.NotNull(actual);
            Assert.AreEqual(subscription.Amount, actual.Amount);
            Assert.AreEqual(subscription.CardNumber, actual.CardNumber);
            Assert.AreEqual(subscription.SubscriptionName, actual.SubscriptionName);

            Assert.IsTrue(0 < actual.SubscriptionID.Trim().Length);
            Assert.IsTrue(0 < long.Parse(actual.SubscriptionID));
        }

        /// <summary>
        /// CreateSubscription eCheck- success
        /// </summary>
        [Test]
        public void CreateSubscriptionTest_eCheck()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBCreateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><subscriptionId>2074569</subscriptionId></ARBCreateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            SubscriptionGateway target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Subscription Test eCheck", (decimal)1.31,
                                                                                  12);
            subscription.eCheckBankAccount = new BankAccount()
                {
                    accountTypeSpecified = true,
                    accountType = BankAccountType.Checking,
                    routingNumber = "125000024",
                    accountNumber = "123456",
                    nameOnAccount = "Sue Zhu",
                    echeckTypeSpecified = true,
                    echeckType = EcheckType.WEB
                };

            Address billToAddress = new Address();
            billToAddress.First = "Sue";
            billToAddress.Last = "Zhu";
            subscription.BillingAddress = billToAddress;

            ISubscriptionRequest actual = null;

            // if choose "USELOCAL", the test should pass with no exception
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
            Assert.AreEqual(subscription.eCheckBankAccount.accountNumber, actual.eCheckBankAccount.accountNumber);
            Assert.AreEqual(subscription.SubscriptionName, actual.SubscriptionName);

            Assert.IsTrue(actual.SubscriptionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.SubscriptionID) == 2074569);
        }

        /// <summary>
        /// UpdateSubscription - success
        /// </summary>
        [Test]
        public void UpdateSubscriptionTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            SubscriptionGateway target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test",
                                                                                  (decimal) 1.32, 12);
            subscription.SubscriptionID = "2010573";

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
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
        [Test]
        public void UpdateSubscriptionTest_SingleDigitMonth()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            SubscriptionGateway target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test",
                                                                                  (decimal) 1.32, 12);
            subscription.SubscriptionID = "2010573";

            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 4;
            subscription.CardExpirationYear = 16;

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
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
        [Test]
        public void UpdateSubscriptionTest_Occurence_Amount()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            SubscriptionGateway target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test",
                                                                                  (decimal) 1.33, 12);
            subscription.SubscriptionID = "2010573";
            subscription.BillingCycles = 15;

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
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
        [Test]
        public void UpdateSubscriptionTest_Description_Invoice()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            SubscriptionGateway target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test Description and Invoice",
                                                                                  (decimal)1.34, 12);
            subscription.SubscriptionID = "2010573";
            subscription.Invoice = "INV12345";
            subscription.Description = "update Description and Invoice";

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
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
