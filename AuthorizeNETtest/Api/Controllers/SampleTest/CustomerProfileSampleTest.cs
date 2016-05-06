namespace AuthorizeNETtest.Api.Controllers.SampleTest
{
    using System;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Api.Controllers.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;
    
    [TestFixture]
    class CustomerProfileSampleTest: ApiCoreTestBase {

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

        private customerPaymentProfileType getCustomerPaymentProfileObject()
        {
            var CreditCardOne = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "2032-10"
            };

            var PaymentOne = new paymentType
            {
                Item = CreditCardOne
            };

            var CustomerPaymentProfile = new customerPaymentProfileType
            {
                customerType = customerTypeEnum.individual,
                payment = PaymentOne
            };

            return CustomerPaymentProfile;
        }

        [Test]
        public void GetCustomerPaymentProfileListSampleTest()
        {
            LogHelper.info(Logger, "Sample getCustomerPaymentProfileList");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            CustomerProfileType.paymentProfiles = new customerPaymentProfileType[] { getCustomerPaymentProfileObject() };
            var createRequest = new createCustomerProfileRequest
            {
                refId = RefId,
                profile = CustomerProfileType
            };

            //create a customer profile
            var createController = new createCustomerProfileController(createRequest);
            var createResponse = createController.ExecuteWithApiResponse();
            Assert.NotNull(createResponse);
            LogHelper.info(Logger, "Created Customer profile : {0}", createResponse.customerProfileId);

            var getProfileListRequest = new getCustomerPaymentProfileListRequest
            {
                refId = RefId,
                searchType = CustomerPaymentProfileSearchTypeEnum.cardsExpiringInMonth,
                month = "2032-10"
            };

            bool found = false;
            //setup retry loop to allow for delays in replication
            for (int counter = 0; counter < 5; counter++)
            {
				//get customer profile list
                var getProfileController = new getCustomerPaymentProfileListController(getProfileListRequest);
                var getProfileListResponse = getProfileController.ExecuteWithApiResponse();

                for (int profile = 0; profile < getProfileListResponse.paymentProfiles.Length; profile++)
                {
                    var profileId = Convert.ToString(getProfileListResponse.paymentProfiles[profile].customerPaymentProfileId);
                    if (profileId.Equals(createResponse.customerPaymentProfileIdList[0]))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    break;

                System.Threading.Thread.Sleep(10000);
            }

            Assert.IsTrue(found);
            
			//delete the created customer profile
			var deleteRequest = new deleteCustomerProfileRequest
            {
                refId = RefId,
                customerProfileId = createResponse.customerProfileId
            };
            var deleteController = new deleteCustomerProfileController(deleteRequest);
            var deleteResponse = deleteController.ExecuteWithApiResponse();
            Assert.IsNotNull(deleteResponse);
        }
    }
}
