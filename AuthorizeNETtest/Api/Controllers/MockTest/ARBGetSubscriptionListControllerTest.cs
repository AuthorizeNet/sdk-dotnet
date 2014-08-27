namespace AuthorizeNet.Api.Controllers.MockTest
{
    using System;
    using System.Collections.Generic;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ARBGetSubscriptionListTest : ApiCoreTestBase 
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
	    public void MockARBGetSubscriptionListTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse>();
            var mockRequest = new ARBGetSubscriptionListRequest
                {
                    merchantAuthentication = new merchantAuthenticationType {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                    refId = RefId,
                    searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive,
                    paging = new Paging { limit = 100, offset = 1 },
                    sorting = new ARBGetSubscriptionListSorting
                    {
                        orderBy = ARBGetSubscriptionListOrderFieldEnum.id,
                        orderDescending = false
                    },
                };
            var subscriptionDetail = new SubscriptionDetail
            {
                id = 1234,
                accountNumber = "1234",
                amount = 1234.56m,
            };
            var subscriptionDetails = new List<SubscriptionDetail> { subscriptionDetail };
            var mockResponse = new ARBGetSubscriptionListResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    subscriptionDetails = subscriptionDetails.ToArray(),
                    totalNumInResultSet = subscriptionDetails.Count,
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse, ARBGetSubscriptionListController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.subscriptionDetails);
            LogHelper.info(Logger, "ARBGetSubscriptionList: Count:{0}, Details:{1}", controllerResponse.totalNumInResultSet, controllerResponse.subscriptionDetails);
        }
    }
}
