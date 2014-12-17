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
    public class getSettledBatchListTest : ApiCoreTestBase 
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
	    public void MockgetSettledBatchListTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<getSettledBatchListRequest, getSettledBatchListResponse>();
            var mockRequest = new getSettledBatchListRequest
                {
                    merchantAuthentication = new merchantAuthenticationType {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                };
            var batchDetailsType = new batchDetailsType[]
                {
                    new batchDetailsType
	                    {
                            batchId = "1234",
	                    }
                };
            var mockResponse = new getSettledBatchListResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    batchList = batchDetailsType,
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<getSettledBatchListRequest, getSettledBatchListResponse, getSettledBatchListController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.batchList);
            LogHelper.info(Logger, "getSettledBatchList: Details:{0}", controllerResponse.batchList);
	    }
    }
}
