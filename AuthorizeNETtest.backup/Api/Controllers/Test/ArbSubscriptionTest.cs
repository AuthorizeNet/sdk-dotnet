using AuthorizeNet.Utility;

namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Api.Controllers.Test;
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
            //create a transaction
            var transactionRequestType = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = SetValidTransactionAmount(Counter),
                payment = PaymentOne,
                order = OrderType,
                customer = CustomerDataOne,
                billTo = CustomerAddressOne,

            };
            var createRequest = new createTransactionRequest
            {
                refId = RefId,
                transactionRequest = transactionRequestType,
                merchantAuthentication = CustomMerchantAuthenticationType,
            };

            var createResponse = ExecuteTestRequestWithSuccess<createTransactionRequest, createTransactionResponse, createTransactionController >(createRequest, TestEnvironment);

            var referenceTxnId = createResponse.transactionResponse.transId;

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

                if (listResponse.subscriptionDetails.Any<SubscriptionDetail>(a => a.id == subsId))
                {
                    found = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(10000);
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
            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a subscription
            var subscriptionDef = new ARBSubscriptionType
            {
                paymentSchedule = new paymentScheduleType
                {
                    interval = new paymentScheduleTypeInterval
                    {
                        length = 1,
                        unit = ARBSubscriptionUnitEnum.months,
                    },
                    startDate = DateTime.UtcNow,
                    totalOccurrences = 12,
                },


                amount = 9.99M,
                billTo = new nameAndAddressType{ firstName = "first", lastName = "last", address="123 elm st ne", city = "Bellevue", state = "Wa", zip = "98007"},

                payment = PaymentOne,

                customer = CustomerOne,

                order = new orderType { description = string.Format("member monthly {0}", rnd.Next(99999)) },
            };

            var arbRequest = new ARBCreateSubscriptionRequest { subscription = subscriptionDef };
            var arbController = new ARBCreateSubscriptionController(arbRequest);
            arbController.Execute();

            var arbCreateResponse = arbController.GetApiResponse();

            Assert.AreEqual(messageTypeEnum.Ok,arbController.GetResultCode());

	    }

        [Test]
        public void TestSubscription_ExpiredCC()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;
            //create a subscription
            var subscriptionDef = new ARBSubscriptionType
            {


                paymentSchedule = new paymentScheduleType
                {
                    interval = new paymentScheduleTypeInterval
                    {
                        length = 7,
                        unit = ARBSubscriptionUnitEnum.days
                    },
                    startDate = DateTime.UtcNow,
                    totalOccurrences = 2,
                },


                amount = 9.99M,

                billTo = new nameAndAddressType
                {
                    address = "1234 Elm St NE",
                    city = "Bellevue",
                    state = "WA",
                    zip = "98007",
                    firstName = "First",
                    lastName = "Last"
                },

                payment = new paymentType
                {
                    Item = new creditCardType
                                 {
                                     cardCode = "655",
                                     //cardNumber = "4007000",
                                     cardNumber = "4111111111111111",
                                     expirationDate = "122013",
                                 }
                },

                customer = new customerType { email = "somecustomer@test.org", id = "5", },

                order = new orderType { description = string.Format("member monthly {0}", rnd.Next(99999)) },
            };

            var arbRequest = new ARBCreateSubscriptionRequest { subscription = subscriptionDef };
            var arbController = new ARBCreateSubscriptionController(arbRequest);
            arbController.Execute();

            var arbCreateResponse = arbController.GetApiResponse();

            //If request responds with an error, walk the messages and get code and text for each message.
            if (arbController.GetResultCode() == messageTypeEnum.Error)
            {
                foreach(var msg in arbCreateResponse.messages.message)
                {
                    Console.WriteLine("Error Num = {0}, Message = {1}", msg.code, msg.text);
                }
            }

        }

        /// <summary>
        /// Repro issue ARBSubscriptionList SearchType of "cardExpiringThisMonth" doesn't work
        /// commenting the test attribute because issue is fixed.
        /// @Zalak
        /// </summary>
       // [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "SearchType cannot be null")]
        public void GetSubscriptionSearchCardExpiringThisMonthIssueTest()
        {
           var getSubscriptionList = new ARBGetSubscriptionListRequest()
                {
                   searchType = ARBGetSubscriptionListSearchTypeEnum.cardExpiringThisMonth,
                    
                };

           ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
           ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;
            var nullController = new ARBGetSubscriptionListController(getSubscriptionList);
            Assert.IsNull( nullController, "Controller should not be instantiated.");
        }

        /// <summary>
        /// Fix the issue ARBSubscriptionList SearchType of "cardExpiringThisMonth" doesn't work
        /// @Zalak
        /// </summary>
        [Test]
        public void GetSubscriptionSearchCardExpiringThisMonthFixTest()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            var createSubscription = new ARBSubscriptionType()
            {
                paymentSchedule = new paymentScheduleType
                {
                    interval = new paymentScheduleTypeInterval
                    {
                        length = 8,
                        unit = ARBSubscriptionUnitEnum.months
                    },
                    startDate = DateTime.UtcNow,
                    totalOccurrences = 3,
                },
                amount = 19.29M,

                billTo = new nameAndAddressType
                {
                    address = "1234 Elm St NE",
                    city = "Bellevue",
                    state = "WA",
                    zip = "98007",
                    firstName = "First",
                    lastName = "Last"
                },

                payment = new paymentType
                {
                    Item = new creditCardType
                    {
                        cardCode = "123",
                        cardNumber = "5105105105105100",
                        // cardNumber = "4111111111111111",
                        expirationDate = "102015",
                    }
                },

                customer = new customerType { email = "somecustomer@test.org", id = "5", },

                order = new orderType { description = string.Format("member monthly {0}", rnd.Next(99999)) },
            };
            var arbCreateSubscriptionController = CreateSubscriptionRequestTest(createSubscription);
            var arbCreateSubscriptionResponse = arbCreateSubscriptionController.ExecuteWithApiResponse();

            if (null == arbCreateSubscriptionResponse)
            {
                throw new ArgumentNullException("arbCreateSubscriptionResponse");
            }

            var getSubscriptionList = new ARBGetSubscriptionListRequest()
            {
                searchType = ARBGetSubscriptionListSearchTypeEnum.cardExpiringThisMonth,

            };

            var arbGetSubscriptionListController = new ARBGetSubscriptionListController(getSubscriptionList);
            var arbGetSubscriptionListResponse = arbGetSubscriptionListController.ExecuteWithApiResponse();

            Assert.IsNotNull(arbGetSubscriptionListResponse);
        }

        private ARBGetSubscriptionListResponse GetSubscriptionListResponse(int limitNo, int offSetNo)
        {
            var getSubscriptionList = new ARBGetSubscriptionListRequest()
            {
                searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive,
                paging = new Paging()
                {
                    limit = limitNo,
                    offset = offSetNo
                },

            };

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;
            var arbGetSubscriptionListController = new ARBGetSubscriptionListController(getSubscriptionList);
            var arbGetSubscriptionListResponse = arbGetSubscriptionListController.ExecuteWithApiResponse();
            return arbGetSubscriptionListResponse;

        }
        /// <summary>
        /// @Zalak
        /// Test case for Pagination issue reported in Jira:
        ///  C# - ARBSubscriptionList SearchType of "cardExpiringThisMonth" doesn't work  
        /// </summary>
        [Test]
        public void ARBGetSubscriptionListCheckPagination()
        {

            var arbGetSubscriptionListResponse = GetSubscriptionListResponse(1, 1);
            ARBGetSubscriptionListResponse response = null;
            int limitNo = 3;
            int offSetNo = 2;
            
            if (arbGetSubscriptionListResponse != null)
            {
                int subcriptionNumber = arbGetSubscriptionListResponse.totalNumInResultSet;
                int expectedSubscriptionNo = 0;
                int nPages = subcriptionNumber/limitNo;
                int subscriptionDetailsOnLastPage = subcriptionNumber%limitNo;
                if (offSetNo <= nPages)
                    expectedSubscriptionNo = limitNo;
                else if (offSetNo > (nPages + 1))
                    expectedSubscriptionNo = 0;
                else
                {
                    expectedSubscriptionNo = subscriptionDetailsOnLastPage;
                }
                response = GetSubscriptionListResponse(limitNo, offSetNo);
                Assert.AreEqual(expectedSubscriptionNo, response.subscriptionDetails.Count());
            }
            else
            {
                Assert.Null(arbGetSubscriptionListResponse);
            }   
            
        }


        private ARBCreateSubscriptionController CreateSubscriptionRequestTest(ARBSubscriptionType subscriptionRequestParameter)
        {
            if (subscriptionRequestParameter == null)
            {
                throw new ArgumentNullException("subscriptionRequestParameter");
            }
            LogHelper.info(Logger, "CreateSubscriptionRequestTest");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            var arbRequest = new ARBCreateSubscriptionRequest { subscription = subscriptionRequestParameter };
            var arbController = new ARBCreateSubscriptionController(arbRequest);
            
            return arbController;
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

        private string CreateSubscription( merchantAuthenticationType merchantAuthentication, string RefId) 
        {
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

        [Test]
        public void TestSubscription_serialization_error()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a subscription
            var subscriptionDef = new ARBSubscriptionType
            {
                paymentSchedule = new paymentScheduleType
                {
                    interval = new paymentScheduleTypeInterval
                    {
                        length = 1,
                        unit = ARBSubscriptionUnitEnum.months,
                    },
                    startDate = DateTime.UtcNow,
                    totalOccurrences = 12,
                },


                amount = 9.99M,
                billTo = new customerAddressType { firstName = "first", lastName = "last" },

                payment = PaymentOne,

                customer = CustomerOne,

                order = new orderType { description = string.Format("member monthly {0}", rnd.Next(99999)) },
            };

            var arbRequest = new ARBCreateSubscriptionRequest { subscription = subscriptionDef };
            var arbController = new ARBCreateSubscriptionController(arbRequest);
            arbController.Execute();

            if (arbController.GetResultCode() == messageTypeEnum.Error)
            {
                var errorResp = arbController.GetErrorResponse();
                Console.WriteLine("{0}: {1}", errorResp.messages.message[0].code, errorResp.messages.message[0].text);
            }

        }
    }
}
