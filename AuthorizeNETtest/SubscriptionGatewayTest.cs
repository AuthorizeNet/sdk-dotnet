using AuthorizeNet;
using NUnit.Framework;
using System;
using AuthorizeNet.Utility;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for SubscriptionGatewayTest and is intended to contain all Subscription Unit Tests
    /// </summary>
    [TestFixture]
    public class SubscriptionGatewayTest : BaseTest
    {
        static string _sMonthlySubscriptionId;
        decimal _mAmount;
        SubscriptionGateway _mTarget;
	    
        /// <summary>
        /// Setup tests by creating a subscription. This setup self also test the subscription creation.
        /// </summary>
        [TestFixtureSetUp]
        public void CreateSubscription()
        {
            var random = new AnetRandom();
            var counter = random.Next(1, (int)(Math.Pow(2, 24)));
            var amount = ComputeRandomAmount();
            var email = string.Format("user.{0}@authorize.net", counter);

            //check ApiLoginid / TransactionKey
            var sError = CheckApiLoginTransactionKey();
            Assert.IsTrue(sError == "", sError);

            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBCreateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><subscriptionId>2010573</subscriptionId></ARBCreateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var target = new SubscriptionGateway(ApiLogin, TransactionKey);

            var billToAddress = new Address { First = "SomeOneCool", Last = "MoreCoolPerson" };
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly(email, "ARB Subscription Test", amount, 1);
            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 3;
            subscription.CardExpirationYear = Convert.ToInt32(DateTime.Now.AddYears(3).ToString("yyyy"));
            subscription.BillingAddress = billToAddress;

            ISubscriptionRequest actual = target.CreateSubscription(subscription);

            Assert.NotNull(actual);
            Assert.AreEqual(subscription.Amount, actual.Amount);
            Assert.AreEqual(subscription.CardNumber, actual.CardNumber);
            Assert.AreEqual(subscription.SubscriptionName, actual.SubscriptionName);

            _sMonthlySubscriptionId = actual.SubscriptionID;
            Assert.IsTrue(0 < _sMonthlySubscriptionId.Trim().Length);
            Assert.IsTrue(0 < long.Parse(_sMonthlySubscriptionId));
        }

        [SetUp]
        public void SetUp()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBUpdateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></ARBUpdateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            _mTarget = new SubscriptionGateway(ApiLogin, TransactionKey);
            _mAmount = ComputeRandomAmount();
        }
	
        [TestFixtureTearDown]
        public void CancelSubscription()
        {
            if (_sMonthlySubscriptionId != null) {
                // cancel the subscription
            }
        }
	
        /// <summary>
        /// CreateSubscription eCheck- success
        /// </summary>
        [Test]
        public void CreateSubscriptionTest_eCheck()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBCreateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><subscriptionId>2074569</subscriptionId></ARBCreateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var target = new SubscriptionGateway(ApiLogin, TransactionKey);

            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Subscription Test eCheck", _mAmount,
                                                                                  12);
            subscription.eCheckBankAccount = new BankAccount
                {
                    accountTypeSpecified = true,
                    accountType = BankAccountType.Checking,
                    routingNumber = "125000024",
                    accountNumber = "123456",
                    nameOnAccount = "Sue Zhu",
                    echeckTypeSpecified = true,
                    echeckType = EcheckType.WEB
                };

            var billToAddress = new Address {First = "Sue", Last = "Zhu"};
            subscription.BillingAddress = billToAddress;

            ISubscriptionRequest actual = target.CreateSubscription(subscription);

            Assert.AreEqual(subscription.Amount, actual.Amount);
            Assert.AreEqual(subscription.eCheckBankAccount.accountNumber, actual.eCheckBankAccount.accountNumber);
            Assert.AreEqual(subscription.SubscriptionName, actual.SubscriptionName);

            Assert.IsTrue(actual.SubscriptionID.Trim().Length > 0);
            Assert.IsTrue(0 < long.Parse(actual.SubscriptionID));
        }

        /// <summary>
        /// CreateSubscription with Zero Trial Amount - success
        /// </summary>
        [Test]
        public void CreateSubscriptionTest_zeroTrial()
        {
            var random = new AnetRandom();
            var counter = random.Next(1, (int)(Math.Pow(2, 24)));
            var amount = ComputeRandomAmount();
            var email = string.Format("user.{0}@authorize.net", counter);

            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ARBCreateSubscriptionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><subscriptionId>2074569</subscriptionId></ARBCreateSubscriptionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var target = new SubscriptionGateway(ApiLogin, TransactionKey);

            var billToAddress = new Address { First = "SomeOneCool", Last = "MoreCoolPerson" };
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly(email, "ARB Subscription Test", amount, 10);
            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 3;
            subscription.CardExpirationYear = Convert.ToInt32(DateTime.Now.AddYears(3).ToString("yyyy"));
            subscription.BillingAddress = billToAddress;

            //setting Trial amount/ Trial Ocurances to 0 
            subscription.SetTrialPeriod(3, 0M);

            ISubscriptionRequest actual = null;
            actual = target.CreateSubscription(subscription);
            Assert.NotNull(actual);
        }

        /// <summary>
        /// UpdateSubscription - success
        /// </summary>
        [Test]
        public void UpdateSubscriptionTest()
        {
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test",
                                                                                  _mAmount, 12);
            subscription.SubscriptionID = _sMonthlySubscriptionId;

            bool actual = _mTarget.UpdateSubscription(subscription);
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdateSubscription SingleDigitMonth - success
        /// </summary>
        [Test]
        public void UpdateSubscriptionTest_SingleDigitMonth()
        {
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test",
                                                                                  _mAmount, 12);
            subscription.SubscriptionID = _sMonthlySubscriptionId;

            subscription.CardNumber = "4111111111111111";
            subscription.CardExpirationMonth = 4;
            subscription.CardExpirationYear = Convert.ToInt32(DateTime.Now.AddYears(3).ToString("yyyy"));

            bool actual = _mTarget.UpdateSubscription(subscription);
            Assert.IsTrue(actual);
        }

        private static decimal ComputeRandomAmount()
        {
            var random = new AnetRandom();
            var counter = random.Next(1, (int) (Math.Pow(2, 24)));
            const int maxSubscriptionAmount = 1000; //214747;
            var amount = new decimal(counter > maxSubscriptionAmount ? (counter%maxSubscriptionAmount) : counter);
            return amount;
        }

        /// <summary>
        /// UpdateSubscription Occurence Amount Changes - success
        /// </summary>
        [Test]
        public void UpdateSubscriptionTest_Occurence_Amount()
        {
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test",
                                                                                  _mAmount, 12);
            subscription.SubscriptionID = _sMonthlySubscriptionId;
            subscription.BillingCycles = 15;

            bool actual = _mTarget.UpdateSubscription(subscription);
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdateSubscription Occurence Amount Changes - success
        /// </summary>
        [Test]
        public void UpdateSubscriptionTest_Description_Invoice()
        {
            ISubscriptionRequest subscription = SubscriptionRequest.CreateMonthly("suzhu@visa.com",
                                                                                  "ARB Update Subscription Test Descriptn and Invoice",
                                                                                  _mAmount, 12);
            subscription.SubscriptionID = _sMonthlySubscriptionId;
            subscription.Invoice = "INV12345";
            subscription.Description = "update Description and Invoice";

            bool actual = _mTarget.UpdateSubscription(subscription);
            Assert.IsTrue(actual);
        }
    }
}
