namespace AuthorizeNet.ApiCore.Controllerss.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AuthorizeNet.ApiCore.Contracts.V1;
    using AuthorizeNet.ApiCore.Controllers;
    using AuthorizeNet.ApiCore.Controllers.Bases;
    using AuthorizeNet.Util;

    [TestClass]
    public class ArbSubscription : ApiCoreTestBase {

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

	    [TestMethod]
	    public void TestGetSubscriptionList() {

            //var subscriptionId = "2096852";

		    var subscriptionId = CreateSubscription( CnpMerchantAuthenticationType);
		    var newStatus = GetSubscription( CnpMerchantAuthenticationType, subscriptionId);
		    Assert.AreEqual(ARBSubscriptionStatusEnum.active, newStatus);

		    LogHelper.info(Logger, "Getting Subscription List for SubscriptionId: %s", subscriptionId);

		    var listRequest = SetupSubscriptionListRequest(CnpMerchantAuthenticationType);
            var listResponse = ExecuteTestRequestWithSuccess<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse, ARBGetSubscriptionListController>(listRequest, TestEnvironment);

		    LogHelper.info( Logger, "Subscription Count: {0}", listResponse.totalNumInResultSet);		
		    Assert.IsTrue( 0 < listResponse.totalNumInResultSet);
		    var subscriptionsArray = listResponse.subscriptionDetails;
		    Assert.IsNotNull( subscriptionsArray);

	        int subsId;
	        var found = false;
            Int32.TryParse(subscriptionId, out subsId);
		
		    foreach ( var aSubscription in subscriptionsArray) {
			    Assert.IsTrue( 0 < aSubscription.id);
			    LogHelper.info( Logger, "Subscription Id: {0}, Status:{1}, PaymentMethod: {2}, Amount: {3}, Account:{4}", 
					    aSubscription.id, aSubscription.status, aSubscription.paymentMethod, aSubscription.amount, aSubscription.accountNumber);
			    if ( subsId == aSubscription.id)
			    {
			        found = true;
			    }
		    }

		    CancelSubscription(CnpMerchantAuthenticationType, subscriptionId);
		    Assert.IsTrue(found);
            
		    //validate the status of subscription to make sure it is in-activated
		    var cancelStatus = GetSubscription(CnpMerchantAuthenticationType, subscriptionId);
		    Assert.AreEqual(ARBSubscriptionStatusEnum.canceled, cancelStatus);
            
	    }

	    [TestMethod]
	    public void TestSubscription() {
		    //cache the result
		    var subscriptionId = CreateSubscription(CnpMerchantAuthenticationType);
		    GetSubscription(CnpMerchantAuthenticationType, subscriptionId);
		    CancelSubscription(CnpMerchantAuthenticationType, subscriptionId);
	    }

	    private ARBGetSubscriptionListRequest SetupSubscriptionListRequest(merchantAuthenticationType merchantAuthentication) {
		
		    var sorting = new ARBGetSubscriptionListSorting
		        {
		            orderDescending = true,
		            orderBy = ARBGetSubscriptionListOrderFieldEnum.createTimeStampUTC,
		        };
	        var paging = new Paging
	            {
	                limit = 500, 
                    offset = 1,
	            };

	        var listRequest = new ARBGetSubscriptionListRequest
	            {
	                merchantAuthentication = merchantAuthentication,
	                refId = RefId,
	                searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive,
		            sorting = sorting,
		            paging = paging,
	            };

		    return listRequest;
	    }
        
	    private void CancelSubscription(merchantAuthenticationType merchantAuthentication, String subscriptionId) {
		    //cancel the subscription
		    var cancelRequest = new ARBCancelSubscriptionRequest
		        {
		            merchantAuthentication = merchantAuthentication,
		            refId = RefId,
		            subscriptionId = subscriptionId
		        };
	        var cancelResponse = ExecuteTestRequestWithSuccess<ARBCancelSubscriptionRequest, ARBCancelSubscriptionResponse, ARBCancelSubscriptionController>(cancelRequest, TestEnvironment);
		    Assert.IsNotNull(cancelResponse.messages);
		    Logger.info(String.Format("Subscription Cancelled: {0}", subscriptionId));
	    }

	    private ARBSubscriptionStatusEnum GetSubscription(merchantAuthenticationType merchantAuthentication, String subscriptionId) {
		    //get a subscription
		    var getRequest = new ARBGetSubscriptionStatusRequest
		        {
		            merchantAuthentication = merchantAuthentication,
		            refId = RefId,
		            subscriptionId = subscriptionId
		        };
	        var getResponse = ExecuteTestRequestWithSuccess<ARBGetSubscriptionStatusRequest, ARBGetSubscriptionStatusResponse, ARBGetSubscriptionStatusController>(getRequest, TestEnvironment);
		    Assert.IsNotNull(getResponse.status);
		    Logger.info(String.Format("Subscription Status: {0}", getResponse.status));
		    return getResponse.status;
	    }
        
	    private string CreateSubscription( merchantAuthenticationType merchantAuthentication) {
		    //create a new subscription
            //RequestFactoryWithSpecified.paymentType(ArbSubscriptionOne.payment);
            //RequestFactoryWithSpecified.paymentScheduleType(ArbSubscriptionOne.paymentSchedule);
            //RequestFactoryWithSpecified.ARBSubscriptionType(ArbSubscriptionOne);
		    var createRequest = new ARBCreateSubscriptionRequest
		        {
		            merchantAuthentication = merchantAuthentication,
		            refId = RefId,
		            subscription = ArbSubscriptionOne,
		        };
	        var createResponse = ExecuteTestRequestWithSuccess<ARBCreateSubscriptionRequest, ARBCreateSubscriptionResponse, ARBCreateSubscriptionController>(createRequest, TestEnvironment);
		    Assert.IsNotNull(createResponse.subscriptionId);
		    LogHelper.info( Logger, "Created Subscription: {0}", createResponse.subscriptionId);

		    return createResponse.subscriptionId;
	    }


        [TestMethod]
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
            var createResponse = ExecuteTestRequestWithSuccess<ARBCreateSubscriptionRequest, ARBCreateSubscriptionResponse, ARBCreateSubscriptionController>(createRequest);
            Assert.IsNotNull(createResponse.subscriptionId);
            LogHelper.info(Logger, "Created Subscription: {0}", createResponse.subscriptionId);
            var subscriptionId = createResponse.subscriptionId;

            //get a subscription
		    var getRequest = new ARBGetSubscriptionStatusRequest
		        {
		            refId = RefId,
		            subscriptionId = subscriptionId
		        };
	        var getResponse = ExecuteTestRequestWithSuccess<ARBGetSubscriptionStatusRequest, ARBGetSubscriptionStatusResponse, ARBGetSubscriptionStatusController>(getRequest);
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
            var listResponse = ExecuteTestRequestWithSuccess<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse, ARBGetSubscriptionListController>(listRequest);
            LogHelper.info(Logger, "Subscription Count: {0}", listResponse.totalNumInResultSet);
            Assert.IsTrue(0 < listResponse.totalNumInResultSet);
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
                refId = RefId,
                subscriptionId = subscriptionId
            };
            var cancelResponse = ExecuteTestRequestWithSuccess<ARBCancelSubscriptionRequest, ARBCancelSubscriptionResponse, ARBCancelSubscriptionController>(cancelRequest);
            Assert.IsNotNull(cancelResponse.messages);
            Logger.info(String.Format("Subscription Cancelled: {0}", subscriptionId));
        }
    }
}
