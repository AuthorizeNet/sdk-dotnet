namespace AuthorizeNet.Api.Controllers.MockTest
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;

    [TestFixture]
    public class createFingerPrintTest : ApiCoreTestBase 
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
	    public void MockcreateFingerPrintTest()
	    {
            var fingerPrintSupportInformation = new fingerPrintSupportInformationType
            {
                amount = SetValidTransactionAmount(Counter) / 100,
                currencyCode = "INR",
                sequence = CounterStr,
                timestamp = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
            };
            //define all mocked objects as final
            var mockController = GetMockController<createFingerPrintRequest, createFingerPrintResponse>();
            var mockRequest = new createFingerPrintRequest
                {
                    merchantAuthentication = new merchantAuthenticationType() {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                    supportInformation = fingerPrintSupportInformation,
                };
            var mockResponse = new createFingerPrintResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    supportInformation = fingerPrintSupportInformation,
                    fingerPrint = new fingerPrintType
                        {
                            sequence = fingerPrintSupportInformation.sequence,
                            timestamp = fingerPrintSupportInformation.timestamp,
                            hashValue = CounterStr,
                        },
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<createFingerPrintRequest, createFingerPrintResponse, createFingerPrintController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

		    Assert.IsNotNull(controllerResponse.fingerPrint);
            LogHelper.info(Logger, "createFingerPrint: Details:{0}", controllerResponse.fingerPrint);
	    }
    }
}
