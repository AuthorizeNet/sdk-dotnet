namespace AuthorizeNet.Api.Controllers.SampleTest
{
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

            //setup some transaction to use
            var transactionId = GetTransactionId();
            var createRequest = new createCustomerProfileFromTransactionRequest
            {
                refId = RefId,
                transId = transactionId.ToString(CultureInfo.InvariantCulture),
            };
            //create 
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
    }
}
