namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Util;

    [TestFixture]
    public class CreateTransactionTest : ApiCoreTestBase
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
        public void CreateProfileWithCreateTransactionRequestTest()
        {
            LogHelper.info(Logger, "CreateProfileWithCreateTransactionRequestTest");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType()
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = SetValidTransactionAmount(Counter),
                payment = PaymentOne,
                order = OrderType,
                customer = CustomerDataOne,
                billTo = CustomerAddressOne,
                shipTo = CustomerAddressOne,
                profile = new customerProfilePaymentType
                    {
                        createProfile = true,
                        createProfileSpecified = true,
                    },
            };
            var createRequest = new createTransactionRequest
            {
                refId = RefId,
                transactionRequest = transactionRequestType,
            };
            //create 
            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();
            Assert.IsNotNull(createResponse.transactionResponse);
            LogHelper.info(Logger, "Response: {0}", createResponse);
            DisplayResponse(createResponse, "Create Transaction Response");
            LogHelper.info(Logger, "Created Transaction: {0}", createResponse.transactionResponse);
            Assert.IsNotNull(createResponse.transactionResponse.transId);
            long transId;
            Assert.IsTrue(Int64.TryParse(createResponse.transactionResponse.transId, out transId));
            if (0 == transId)
            {
                ValidateFailure<createTransactionRequest, createTransactionResponse, createTransactionController>(createController, createResponse);
                Assert.IsNotNull(createResponse.transactionResponse.errors);
                foreach (var error in createResponse.transactionResponse.errors)
                {
                    LogHelper.info(Logger, "Error-> Code:{0}, Text:{1}", error.errorCode, error.errorText);
                }
            }
            else
            {
                Assert.AreNotEqual(0, transId);
                ValidateSuccess<createTransactionRequest, createTransactionResponse, createTransactionController>(createController, createResponse);
            }
            var profileResponse = createResponse.profileResponse;
            Assert.IsNotNull(profileResponse);
            Assert.IsNotNull(profileResponse.customerProfileId);
            Assert.IsNotNull(profileResponse.customerPaymentProfileIdList);
            Assert.IsNotNull(profileResponse.customerShippingAddressIdList);
            Assert.AreNotEqual("0", profileResponse.customerProfileId);

            Assert.AreEqual(1, profileResponse.customerPaymentProfileIdList.Length);
            Assert.AreNotEqual("0", profileResponse.customerPaymentProfileIdList[0]);

            Assert.AreEqual(1, profileResponse.customerShippingAddressIdList.Length);
            Assert.AreNotEqual("0", profileResponse.customerShippingAddressIdList[0]);
        }
    }
}
