namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Util;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class ArbSubscriptionTest : ApiCoreTestBase {

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
        public void TestGetSubscriptionList()
        {
            string referenceTxnId = string.Empty;

            try
            {
                referenceTxnId = GetValidTxnId(CustomMerchantAuthenticationType);
            }
            catch (NullReferenceException)
            {
                Assert.Fail("This test requires a completed transaction to create a subscription from.  Add a transaction, then try this test again.");
            }
            
		    var subscriptionId = CreateSubscription( CustomMerchantAuthenticationType, referenceTxnId);
		    var newStatus = GetSubscription( CustomMerchantAuthenticationType, subscriptionId);
		    Assert.AreEqual(ARBSubscriptionStatusEnum.active, newStatus);

		    LogHelper.info(Logger, "Getting Subscription List for SubscriptionId: {0}", subscriptionId);
            
	        int subsId;
	        var found = false;
            Int32.TryParse(subscriptionId, out subsId);

            //setup retry loop to allow for delays in replication
            for (int counter = 0; counter < 5; counter++)
            {
                var listRequest = SetupSubscriptionListRequest(CustomMerchantAuthenticationType);
                var listResponse = ExecuteTestRequestWithSuccess<ARBGetSubscriptionListRequest, ARBGetSubscriptionListResponse, ARBGetSubscriptionListController>(listRequest, TestEnvironment);

                SubscriptionDetail aSubscription = listResponse.subscriptionDetails.FirstOrDefault(a => a.id == subsId);

                if (aSubscription == null)
                {
                    System.Threading.Thread.Sleep(10000);
                }
                else
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
		    CancelSubscription(CustomMerchantAuthenticationType, subscriptionId);
            
		    //validate the status of subscription to make sure it is canceled
		    var cancelStatus = GetSubscription(CustomMerchantAuthenticationType, subscriptionId);
		    Assert.AreEqual(ARBSubscriptionStatusEnum.canceled, cancelStatus);
	    }

	    [Test]
	    public void TestSubscription() {
		    //cache the result
            string txnId = GetValidTxnId(CustomMerchantAuthenticationType);

		    var subscriptionId = CreateSubscription(CustomMerchantAuthenticationType, txnId);
		    GetSubscription(CustomMerchantAuthenticationType, subscriptionId);
		    CancelSubscription(CustomMerchantAuthenticationType, subscriptionId);
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

	    private string CreateSubscription( merchantAuthenticationType merchantAuthentication, string RefId) {
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

        private string GetValidTxnId(merchantAuthenticationType merchantAuthentication)
        {
            var getUnsettledTxnReq = new getUnsettledTransactionListRequest { merchantAuthentication = merchantAuthentication };
            var getUnsettledTxnResp = ExecuteTestRequestWithSuccess<getUnsettledTransactionListRequest, getUnsettledTransactionListResponse, getUnsettledTransactionListController>(getUnsettledTxnReq, TestEnvironment);

            return getUnsettledTxnResp.transactions[getUnsettledTxnResp.transactions.Length -1].transId;
        }
    }
}
