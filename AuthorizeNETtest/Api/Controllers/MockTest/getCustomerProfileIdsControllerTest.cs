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
    public class getCustomerProfileIdsTest : ApiCoreTestBase 
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
	    public void MockgetCustomerProfileIdsTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<getCustomerProfileIdsRequest, getCustomerProfileIdsResponse>();
            var mockRequest = new getCustomerProfileIdsRequest
                {
                    merchantAuthentication = new merchantAuthenticationType {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                };
            var mockResponse = new getCustomerProfileIdsResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    ids = new [] {"1234"},
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<getCustomerProfileIdsRequest, getCustomerProfileIdsResponse, getCustomerProfileIdsController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.ids);
            LogHelper.info(Logger, "getCustomerProfileIds: Details:{0}", controllerResponse.ids);
	    }
    }
}
