namespace AuthorizeNet.Api.Controllers.SampleTest
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;

    [TestFixture]
    public class ArbSubscriptionSampleTest : ApiCoreTestBase {

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
	    public new void SetUp() {
		    base.SetUp();
	    }

	    [TearDown]
	    public new void TearDown() {
		    base.TearDown();
	    }

        [Test]
        public void SampleCodeGetSubscriptionList()
        {
            LogHelper.info(Logger, "Sample GetSubscriptionList");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a subscription
            var createRequest = new ARBCreateSubscriptionRequest
            {
                refId = RefId,
                subscription = ArbSubscriptionOne,
            };

            var createController = new ARBCreateSubscriptionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();
            Assert.IsNotNull(createResponse.subscriptionId);
            LogHelper.info(Logger, "Created Subscription: {0}", createResponse.subscriptionId);
            var subscriptionId = createResponse.subscriptionId;

            //get subscription details
		    var getRequest = new ARBGetSubscriptionStatusRequest
		        {
		            refId = RefId,
		            subscriptionId = subscriptionId
		        };
            var getController = new ARBGetSubscriptionStatusController(getRequest);
            var getResponse = getController.ExecuteWithApiResponse();
		    Assert.IsNotNull(getResponse.status);
		    Logger.info(String.Format("Subscription Status: {0}", getResponse.status));

            //get subscription list that contains only the subscription created above.
	        var listRequest = new ARBGetSubscriptionListRequest
	            {
	                refId = RefId,
	                searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive,
		            sorting = new ARBGetSubscriptionListSorting
		                {
		                    orderDescending = true,
		                    orderBy = ARBGetSubscriptionListOrderFieldEnum.createTimeStampUTC,
		                },
		            paging = new Paging
	                    {
	                        limit = 500, 
                            offset = 1,
	                    },
	            };
            var listController = new ARBGetSubscriptionListController(listRequest);
            var listResponse = listController.ExecuteWithApiResponse();
            LogHelper.info(Logger, "Subscription Count: {0}", listResponse.totalNumInResultSet);
            Assert.IsTrue(0 < listResponse.totalNumInResultSet);

            //validation of list
            var subscriptionsArray = listResponse.subscriptionDetails;
            foreach (var aSubscription in subscriptionsArray)
            {
                Assert.IsTrue(0 < aSubscription.id);
                LogHelper.info(Logger, "Subscription Id: {0}, Status:{1}, PaymentMethod: {2}, Amount: {3}, Account:{4}",
                        aSubscription.id, aSubscription.status, aSubscription.paymentMethod, aSubscription.amount, aSubscription.accountNumber);
            }

            //cancel subscription
            var cancelRequest = new ARBCancelSubscriptionRequest
            {
                merchantAuthentication = CustomMerchantAuthenticationType,
                refId = RefId,
                subscriptionId = subscriptionId
            };
            var cancelController = new ARBCancelSubscriptionController(cancelRequest);
            var cancelResponse = cancelController.ExecuteWithApiResponse(TestEnvironment);
            Assert.IsNotNull(cancelResponse.messages);
            Logger.info(String.Format("Subscription Cancelled: {0}", subscriptionId));
        }

        [Test]
        public void ARBGetSubscriptionSampleTest()
        {
            LogHelper.info(Logger, "Sample GetSubscriptionList");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a subscription
            var createRequest = new ARBCreateSubscriptionRequest
            {
                refId = RefId,
                subscription = ArbSubscriptionOne,
            };

            var createController = new ARBCreateSubscriptionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();
            Assert.IsNotNull(createResponse.subscriptionId);
            LogHelper.info(Logger, "Created Subscription: {0}", createResponse.subscriptionId);
            var subscriptionId = createResponse.subscriptionId;

            //get subscription details
            var getRequest = new ARBGetSubscriptionRequest
            {
                refId = RefId,
                subscriptionId = subscriptionId
            };
            var getController = new ARBGetSubscriptionController(getRequest);
            var getResponse = getController.ExecuteWithApiResponse();
            Assert.IsNotNull(getResponse.subscription);
            Logger.info(String.Format("Subscription Name : {0}", getResponse.subscription.name));
            Assert.AreEqual(ArbSubscriptionOne.name, getResponse.subscription.name);
            Assert.AreEqual(ArbSubscriptionOne.amountSpecified, getResponse.subscription.amountSpecified);

            //cancel subscription
            var cancelRequest = new ARBCancelSubscriptionRequest
            {
                merchantAuthentication = CustomMerchantAuthenticationType,
                refId = RefId,
                subscriptionId = subscriptionId
            };
            var cancelController = new ARBCancelSubscriptionController(cancelRequest);
            var cancelResponse = cancelController.ExecuteWithApiResponse(TestEnvironment);
            Assert.IsNotNull(cancelResponse.messages);
            Logger.info(String.Format("Subscription Cancelled: {0}", subscriptionId));
        }
    }
}
