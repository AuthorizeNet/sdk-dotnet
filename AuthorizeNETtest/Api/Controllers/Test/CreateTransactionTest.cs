namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Util;

    [TestClass]
    public class CreateTransactionTest : ApiCoreTestBase
    {

        [ClassInitialize]
        public new static void SetUpBeforeClass(TestContext context)
        {
            ApiCoreTestBase.SetUpBeforeClass(context);
        }

        [ClassCleanup]
        public new static void TearDownAfterClass()
        {
            ApiCoreTestBase.TearDownAfterClass();
        }

        [TestInitialize]
        public new void SetUp()
        {
            base.SetUp();
        }

        [TestCleanup]
        public new void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void SampleCodecreateTransaction()
        {
            LogHelper.info(Logger, "Sample createTransaction");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CnpMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType()
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                    //amount = SetValidTransactionAmount(Counter),
                    amount = 10000000000000000000,
                    payment = PaymentOne,
                    order =  OrderType,
                    customer =  CustomerDataOne,
                    billTo =  CustomerAddressOne,

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
            int transId;
            Assert.IsTrue( Int32.TryParse(createResponse.transactionResponse.transId, out transId));
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
        }

        [TestMethod]
	    public void MockcreateTransactionTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<createTransactionRequest, createTransactionResponse>();
            var mockRequest = new createTransactionRequest
                {
                    merchantAuthentication = new merchantAuthenticationType() {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                };
            var messages = new messagesType();
            var transactionResponse = new transactionResponse();


            var mockResponse = new createTransactionResponse
                {
                    messages = messages,
                    transactionResponse = transactionResponse,
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<createTransactionRequest, createTransactionResponse, createTransactionController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

		    Assert.IsNotNull(controllerResponse.transactionResponse);
		    LogHelper.info(Logger, "createTransaction: Details:{0}", controllerResponse.transactionResponse);
	    }
    }
}
