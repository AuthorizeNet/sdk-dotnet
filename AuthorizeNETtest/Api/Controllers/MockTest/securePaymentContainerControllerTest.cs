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
    public class securePaymentContainerTest : ApiCoreTestBase 
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
	    public void MocksecurePaymentContainerTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<securePaymentContainerRequest, securePaymentContainerResponse>();
            var mockRequest = new securePaymentContainerRequest
                {
                    merchantAuthentication = new merchantAuthenticationType() {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                };

            var opaqueDataType = new opaqueDataType
            {
                dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
                dataKey = "foFBEbhXljevQQasx5Q87hzj57xvUl4iBmXDdB1vs/Lm/M1uKJiF9V5QxI0A6NvAtIckMvutSl0Chz2SNoSeBuTRzK0y4IlfnfWKnJF7a1LV/bjZokTtFKINdZ+Ks9RB",
                dataValue = "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"
            };

            var mockResponse = new securePaymentContainerResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    opaqueData = opaqueDataType
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<securePaymentContainerRequest, securePaymentContainerResponse, securePaymentContainerController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.opaqueData);

            LogHelper.info(Logger, "securePaymentContainer: Details:{0}", controllerResponse.opaqueData);
	    }
    }
}
