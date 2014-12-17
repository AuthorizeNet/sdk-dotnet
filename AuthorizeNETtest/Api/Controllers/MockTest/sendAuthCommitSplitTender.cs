namespace AuthorizeNet.Api.Controllers.MockTest
{
    using System;
    using System.Collections.Generic;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;
    using V1 = AuthorizeNet.Api.Contracts.V1;

    [TestFixture]
    public class sendAuthCommitSplitTender : ApiCoreTestBase
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
        public void MockAuthCapPartialTender()
        {
            //define all mocked objects as final

            var mockAuthCapController = GetMockController<createTransactionRequest, createTransactionResponse>();

            var creditCard1 = new creditCardType { cardNumber = "4111111111118881", expirationDate = "0622" };
            var creditCard2 = new creditCardType { cardNumber = "4111111111111112", expirationDate = "0620" };

            var mockPartialRequest = new createTransactionRequest
            {
                merchantAuthentication = new merchantAuthenticationType { name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey },
                transactionRequest = new transactionRequestType
                {
                    amount = 10.00m,
                    payment = new paymentType { Item = creditCard1 },
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString()
                }
            };

            var mockPartialRequestExpectedResponse = new createTransactionResponse
            {
                transactionResponse = new transactionResponse
                {
                    accountNumber = "XXXX8881",
                    authCode = "BZKI0z",
                    accountType = "Visa",
                    messages = new transactionResponseMessage[] { new transactionResponseMessage { description = "The amount of this request was only partially approved on the given prepaid card. An additional payment is required to fulfill the balance of this transaction." } },
                    avsResultCode = "Y",
                    cavvResultCode = "2",
                    transId = "2149212584",
                    transHash = "31F1FD7B5EC563542F6FAF2CF65D8B65",
                    testRequest = "0",
                    splitTenderId = "117673",
                    prePaidCard = new transactionResponsePrePaidCard { requestedAmount = "10.00", approvedAmount = "1.23" }
                }
            };

            var AuthCapErrorResp = new ANetApiResponse();
            var AuthCapResults = new List<string>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<createTransactionRequest, createTransactionResponse, createTransactionController>(
                mockAuthCapController.MockObject, mockPartialRequest, mockPartialRequestExpectedResponse, AuthCapErrorResp, AuthCapResults, messageTypeEnum.Ok);

            mockAuthCapController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);

            var mockPartialAuthResponse = mockAuthCapController.MockObject.GetApiResponse();
            Assert.IsNotNull(mockPartialAuthResponse);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.messages[0].description, mockPartialRequestExpectedResponse.transactionResponse.messages[0].description);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.splitTenderId, mockPartialRequestExpectedResponse.transactionResponse.splitTenderId);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.prePaidCard.approvedAmount, mockPartialRequestExpectedResponse.transactionResponse.prePaidCard.approvedAmount);


            //Create second request to put ballance on second card
            var mockCompletingRequest = new createTransactionRequest
            {
                merchantAuthentication = new merchantAuthenticationType { name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey },
                transactionRequest = new transactionRequestType
                {
                    payment = new paymentType { Item = creditCard2 },
                    amount = decimal.Parse(mockPartialAuthResponse.transactionResponse.prePaidCard.requestedAmount) - decimal.Parse(mockPartialAuthResponse.transactionResponse.prePaidCard.approvedAmount),
                    splitTenderId = mockPartialAuthResponse.transactionResponse.splitTenderId,
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString()
                }
            };

            var completingTransactionExpectedResp = new createTransactionResponse
            {
                transactionResponse = new transactionResponse
                {
                    accountNumber = "XXXX1112",
                    authCode = "XMRBGL",
                    accountType = "Visa",
                    messages = new transactionResponseMessage[] { new transactionResponseMessage { description = "This transaction has been approved." } },
                    avsResultCode = "Y",
                    cavvResultCode = "",
                    transId = "2149212586",
                    transHash = "2EFBAF977FA627E3805895B66662038D",
                    testRequest = "0",
                    splitTenderId = "117673",
                    prePaidCard = new transactionResponsePrePaidCard { requestedAmount = "8.77", approvedAmount = "8.77" },
                    splitTenderPayments = new transactionResponseSplitTenderPayment[]
                    {
                        new transactionResponseSplitTenderPayment{transId= "2149212584", responseCode = "4", responseToCustomer = "295", authCode = "BZKI0Z",
                            accountNumber = "XXXX8881", accountType = "Visa", requestedAmount = "10.00", approvedAmount = "1.23"},
                    }
                }
            };

            //reset controller
            mockAuthCapController = GetMockController<createTransactionRequest, createTransactionResponse>();

            SetMockControllerExpectations<createTransactionRequest, createTransactionResponse, createTransactionController>(
                mockAuthCapController.MockObject, mockCompletingRequest, completingTransactionExpectedResp, AuthCapErrorResp, AuthCapResults, messageTypeEnum.Ok);

            mockAuthCapController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            mockPartialAuthResponse = mockAuthCapController.MockObject.GetApiResponse();
            Assert.IsNotNull(mockPartialAuthResponse);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.messages[0].description, completingTransactionExpectedResp.transactionResponse.messages[0].description);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.splitTenderId, completingTransactionExpectedResp.transactionResponse.splitTenderId);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.prePaidCard.approvedAmount, completingTransactionExpectedResp.transactionResponse.prePaidCard.approvedAmount);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.splitTenderPayments[0].accountNumber, completingTransactionExpectedResp.transactionResponse.splitTenderPayments[0].accountNumber);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.splitTenderPayments[0].authCode, completingTransactionExpectedResp.transactionResponse.splitTenderPayments[0].authCode);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.splitTenderPayments[0].approvedAmount, completingTransactionExpectedResp.transactionResponse.splitTenderPayments[0].approvedAmount);
            Assert.AreEqual(mockPartialAuthResponse.transactionResponse.splitTenderPayments[0].accountType, completingTransactionExpectedResp.transactionResponse.splitTenderPayments[0].accountType);
        }
    }
}
