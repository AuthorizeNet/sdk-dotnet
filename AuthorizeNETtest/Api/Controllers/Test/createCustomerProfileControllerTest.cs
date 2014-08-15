namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using System.Collections.Generic;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class createCustomerProfileTest : ApiCoreTestBase 
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
	    public void MockcreateCustomerProfileTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<createCustomerProfileRequest, createCustomerProfileResponse>();
            var mockRequest = new createCustomerProfileRequest
                {
                    merchantAuthentication = new merchantAuthenticationType {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                };
            var mockResponse = new createCustomerProfileResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    customerProfileId = "1234",
                    customerPaymentProfileIdList = new [] {"12345"},
                    customerShippingAddressIdList = new[] { "12345" },
                    validationDirectResponseList = new string[] {},
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<createCustomerProfileRequest, createCustomerProfileResponse, createCustomerProfileController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.customerProfileId);
            LogHelper.info(Logger, "createCustomerProfile: Details:{0}", controllerResponse.customerProfileId);
	    }
    }
}
