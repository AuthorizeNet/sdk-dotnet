namespace AuthorizeNet.Api.Controllers.MockTest
{
    using System;
    using System.Collections.Generic;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;

    [TestFixture]
    public class getUnsettledTransactionListTest : ApiCoreTestBase 
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
	    public void MockgetUnsettledTransactionListTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<getUnsettledTransactionListRequest, getUnsettledTransactionListResponse>();
            var mockRequest = new getUnsettledTransactionListRequest
                {
                    merchantAuthentication = new merchantAuthenticationType {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                };
            var transactionSummaryType = new transactionSummaryType[]
                {
                    new transactionSummaryType
                        {
                            accountNumber = "1234",
                        }
                };
            var mockResponse = new getUnsettledTransactionListResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    transactions = transactionSummaryType,
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<getUnsettledTransactionListRequest, getUnsettledTransactionListResponse, getUnsettledTransactionListController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.transactions);
            LogHelper.info(Logger, "getUnsettledTransactionList: Details:{0}", controllerResponse.transactions);
	    }
    }
}
