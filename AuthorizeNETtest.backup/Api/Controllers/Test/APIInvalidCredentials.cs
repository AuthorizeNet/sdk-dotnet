namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Util;

    [TestFixture]
    public class CredentialsTest : ApiCoreTestBase
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
        public void InvalidCredentialsTest()
        {
            LogHelper.info(Logger, "CreateProfileWithCreateTransactionRequestTest");

            var badCredentials = new merchantAuthenticationType { name = "mbld_api_-NPA5n9k", Item = "123123", ItemElementName = ItemChoiceType.transactionKey };
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = badCredentials;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create request
            var getCpReq = new getCustomerProfileRequest
            {
                customerProfileId = "1234"
            };

            var getCpCont = new getCustomerProfileController(getCpReq);
            getCpCont.Execute();
            getCustomerProfileResponse getCpResp = getCpCont.GetApiResponse();

            Assert.AreEqual("E00007", ((AuthorizeNet.Api.Contracts.V1.ANetApiResponse)(getCpResp)).messages.message[0].code);
            ValidateErrorCode(((AuthorizeNet.Api.Contracts.V1.ANetApiResponse)(getCpResp)).messages, "E00007");
        }

        [Test]
        public void IllFormedCredentialsTest()
        {
            LogHelper.info(Logger, "CreateProfileWithCreateTransactionRequestTest");

            var badCredentials = new merchantAuthenticationType { name = "mbld_api_-NPA5n9k", Item = "123123" }; //, ItemElementName = ItemChoiceType.transactionKey };
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = badCredentials;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create request
            var getCpReq = new getCustomerProfileRequest
            {
                customerProfileId = "1234"
            };

            try
            {
                var getCpCont = new getCustomerProfileController(getCpReq);
                getCpCont.Execute();
                Assert.Fail("You should not reach here");
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception expected: " + e.Message);
            }
        }
    }
}
