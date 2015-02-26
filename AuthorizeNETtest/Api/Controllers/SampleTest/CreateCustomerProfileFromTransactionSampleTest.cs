namespace AuthorizeNet.Api.Controllers.SampleTest
{
    using System;
    using System.Globalization;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;

    [TestFixture]
    public class CreateCustomerProfileFromTransactionSampleTest : ApiCoreTestBase
    {

        [TestFixtureSetUp]
        public new static void SetUpBeforeClass()
        {
            ApiCoreTestBase.SetUpBeforeClass();
        }

        [TestFixtureTearDown]
        public new static void TearDownAfterClass()
        {
            ApiCoreTestBase.TearDownAfterClass();
        }

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();
        }

        [TearDown]
        public new void TearDown()
        {
            base.TearDown();
        }

        [Test]
        public void SampleCodeCreateCustomerProfileFromTransaction()
        {
            LogHelper.info(Logger, "Sample createCustomerProfileFromTransaction");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //setup transaction to use
            var transactionId = GetTransactionId();
            var createRequest = new createCustomerProfileFromTransactionRequest
            {
                refId = RefId,
                transId = transactionId.ToString(CultureInfo.InvariantCulture),
            };
            //execute and get response
            var createController = new createCustomerProfileFromTransactionController(createRequest);
            var createResponse = createController.ExecuteWithApiResponse();

            //validate
            Assert.NotNull(createResponse);
            Assert.NotNull(createResponse.messages);
            Assert.AreEqual(messageTypeEnum.Ok, createResponse.messages.resultCode);
            Assert.NotNull(createResponse.customerProfileId);
            Assert.NotNull(createResponse.customerPaymentProfileIdList);
            Assert.AreNotEqual(0, createResponse.customerPaymentProfileIdList.Length);

            long customerProfileId;
            long.TryParse(createResponse.customerProfileId, out customerProfileId);
            Assert.AreNotEqual(0, customerProfileId);

            long customerPaymentProfileId;
            long.TryParse(createResponse.customerPaymentProfileIdList[0], out customerPaymentProfileId);
            Assert.AreNotEqual(0, customerPaymentProfileId);
            //if shipping profile is added, shipping profile id will be retrieved too
        }

        private long GetTransactionId()
        {
            //Creates a credit card transaction and returns the transactions ID.

            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };
            var aCustomer = new customerDataType { email = string.Format( "{0}@b.bla", Counter)};

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                payment = paymentType,
                amount = transactionAmount,
                customer = aCustomer,
            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //validate
            Assert.NotNull(response);
            Assert.NotNull(response.messages);
            Assert.NotNull(response.transactionResponse);
            Assert.AreEqual(messageTypeEnum.Ok, response.messages.resultCode);
            Assert.False(string.IsNullOrEmpty(response.transactionResponse.transId));
            long transactionId;
            long.TryParse(response.transactionResponse.transId, out transactionId);
            Assert.AreNotEqual(0, transactionId);

            return transactionId;
        }

        [Test]
        public void CreateTransactionFromProfile()
        {
            //Creates a customer profile and customer payment profile
            //Then uses those profiles to create a transaction request

            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            Random rnd = new Random(DateTime.Now.Millisecond);

            string profileRandom = rnd.Next(9999).ToString();

            //Create profile to use in transaction creation
            var profileShipTo = new customerAddressType
            {
                address = profileRandom + " First St NE",
                city = "Bellevue",
                state = "WA",
                zip = "98007",
                company = "Sample Co " + profileRandom,
                country = "USA",
                firstName = "Sample" + profileRandom,
                lastName = "Name" + profileRandom,
                phoneNumber = "425 123 4567",
            };

            var paymentProfile = new customerPaymentProfileType
            {
                billTo = profileShipTo,
                customerType = customerTypeEnum.individual,
                payment = new paymentType { Item = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" } },
            };

            var createProfileReq = new createCustomerProfileRequest
            {
                profile = new customerProfileType
                {
                    description = "SampleProfile " + profileRandom,
                    email = "SampleEmail" + profileRandom + "@Visa.com",
                    shipToList = new customerAddressType[] { profileShipTo },
                    paymentProfiles = new customerPaymentProfileType[] { paymentProfile }
                }
            };

            var createProfileCont = new createCustomerProfileController(createProfileReq);
            createProfileCont.Execute();
            var createProfileResp = createProfileCont.GetApiResponse();

            //Get profile using getCustomerProfileRequest
            var getCustReq = new getCustomerProfileRequest { customerProfileId = createProfileResp.customerProfileId };
            var getCustCont = new getCustomerProfileController(getCustReq);
            getCustCont.Execute();
            var getCustResp = getCustCont.GetApiResponse();


            //Create Transaction
            //Create instance of customer payment profile using the profile IDs from the profile we loaded above.
            var custPaymentProfile = new AuthorizeNet.Api.Contracts.V1.customerProfilePaymentType { customerProfileId = getCustResp.profile.customerProfileId, paymentProfile = new paymentProfile { paymentProfileId = getCustResp.profile.paymentProfiles[0].customerPaymentProfileId } };

            var testTxn = new transactionRequestType
            {
                profile = custPaymentProfile,
                amount = (decimal)rnd.Next(9999) / 100,
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString()
            };

            var txnControler = new createTransactionController(new createTransactionRequest { transactionRequest = testTxn });
            txnControler.Execute();
            var txnControlerResp = txnControler.GetApiResponse();

            //verify transaction succeeded.
            Assert.AreEqual("1", txnControlerResp.transactionResponse.messages[0].code);

        }
    }
}
