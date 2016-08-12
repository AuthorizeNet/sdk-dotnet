using AuthorizeNet.Utility;

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
    public class ARBGetSubscriptionTest : ApiCoreTestBase 
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
	    public void MockARBGetSubscriptionTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<ARBGetSubscriptionRequest, ARBGetSubscriptionResponse>();
            var mockRequest = new ARBGetSubscriptionRequest
                {
                    merchantAuthentication = new merchantAuthenticationType() {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                    subscriptionId = "1234"
                };

            var customerPaymentProfileMaskedType = new customerPaymentProfileMaskedType
                {
                    customerPaymentProfileId = "1234",
                };

            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            var SubscriptionMaskedType = new ARBSubscriptionMaskedType()
            {
                name = "Test",
                paymentSchedule = new paymentScheduleType
                {
                    interval = new paymentScheduleTypeInterval
                    {
                        length = 1,
                        unit = ARBSubscriptionUnitEnum.months,
                    },
                    startDate = DateTime.UtcNow,
                    totalOccurrences = 12
                },
                amount = 9.99M,
                amountSpecified = true,
                trialAmount = 100,
                trialAmountSpecified = true,
                status = ARBSubscriptionStatusEnum.active,
                statusSpecified = true,
                profile = new subscriptionCustomerProfileType()
                {
                    paymentProfile = customerPaymentProfileMaskedType,

                },
                order = new orderType { description = string.Format("member monthly {0}", rnd.Next(99999)) }               
            };

            var mockResponse = new ARBGetSubscriptionResponse
                {
                    refId = "1234",
                    sessionToken = "sessiontoken",
                    subscription = SubscriptionMaskedType
                };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<ARBGetSubscriptionRequest, ARBGetSubscriptionResponse, ARBGetSubscriptionController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

            Assert.IsNotNull(controllerResponse.subscription);
            LogHelper.info(Logger, "ARBGetSubscription: Details:{0}", controllerResponse.subscription);
	    }
    }
}
