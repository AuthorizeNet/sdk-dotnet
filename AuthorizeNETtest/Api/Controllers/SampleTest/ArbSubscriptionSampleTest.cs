namespace AuthorizeNet.Api.Controllers.SampleTest
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ArbSubscriptionSampleTest : ApiCoreTestBase {

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
	    public new void SetUp() {
		    base.SetUp();
	    }

	    [TestCleanup]
	    public new void TearDown() {
		    base.TearDown();
	    }

        [TestMethod, Ignore]
        public void SampleCodeGetSubscriptionList()
        {
            LogHelper.info(Logger, "Sample GetSubscriptionList");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CnpMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a subscription
            var createRequest = new ARBCreateSubscriptionRequest
            {
                refId = RefId,
                subscription = ArbSubscriptionOne,
            };
            //create 
            var createController = new ARBCreateSubscriptionController(createRequest);
            //separate execute and getResponse calls
            createController.Execute();
            var createResponse = createController.GetApiResponse();
            Assert.IsNotNull(createResponse.subscriptionId);
            LogHelper.info(Logger, "Created Subscription: {0}", createResponse.subscriptionId);
            var subscriptionId = createResponse.subscriptionId;

            //get a subscription
		    var getRequest = new ARBGetSubscriptionStatusRequest
		        {
		            refId = RefId,
		            subscriptionId = subscriptionId
		        };
            var getController = new ARBGetSubscriptionStatusController(getRequest);
            //execute and getResponse calls together
            var getResponse = getController.ExecuteWithApiResponse();
		    Assert.IsNotNull(getResponse.status);
		    Logger.info(String.Format("Subscription Status: {0}", getResponse.status));

            //get subscription list
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

            //cancel subscription
            var cancelRequest = new ARBCancelSubscriptionRequest
            {
                merchantAuthentication = CnpMerchantAuthenticationType,
                refId = RefId,
                subscriptionId = subscriptionId
            };
            //explicitly setting up the merchant id and environment 
            var cancelController = new ARBCancelSubscriptionController(cancelRequest);
            var cancelResponse = cancelController.ExecuteWithApiResponse(TestEnvironment);
            Assert.IsNotNull(cancelResponse.messages);
            Logger.info(String.Format("Subscription Cancelled: {0}", subscriptionId));

            //validation of list
            var subscriptionsArray = listResponse.subscriptionDetails;
            foreach (var aSubscription in subscriptionsArray)
            {
                Assert.IsTrue(0 < aSubscription.id);
                LogHelper.info(Logger, "Subscription Id: {0}, Status:{1}, PaymentMethod: {2}, Amount: {3}, Account:{4}",
                        aSubscription.id, aSubscription.status, aSubscription.paymentMethod, aSubscription.amount, aSubscription.accountNumber);
            }
        }
    }
}
