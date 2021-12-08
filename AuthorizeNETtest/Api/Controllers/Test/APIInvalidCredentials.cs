using System;
using NUnit.Framework;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Util;

namespace AuthorizeNet.Api.Controllers.Test
{
	[TestFixture]
	public class CredentialsTest : ApiCoreTestBase
	{

		[SetUp]
		public new static void SetUpBeforeClass()
		{
			ApiCoreTestBase.SetUpBeforeClass();
		}

		[TearDown]
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
			LogHelper.Info(Logger, "CreateProfileWithCreateTransactionRequestTest");

			var badCredentials = new merchantAuthenticationType { name = "mbld_api_-NPA5n9k", Item = "123123", ItemElementName = ItemChoiceType.transactionKey };
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = badCredentials;
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

			//create request
			var getCpReq = new getCustomerProfileRequest
			{
				customerProfileId = "1234"
			};

			var getCpCont = new GetCustomerProfileController(getCpReq);
			getCpCont.Execute();
			getCustomerProfileResponse getCpResp = getCpCont.GetApiResponse();

			Assert.AreEqual("E00007", ((ANetApiResponse)(getCpResp)).messages.message[0].code);
			ValidateErrorCode(((ANetApiResponse)(getCpResp)).messages, "E00007");
		}

		[Test]
		public void IllFormedCredentialsTest()
		{
			LogHelper.Info(Logger, "CreateProfileWithCreateTransactionRequestTest");

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
				var getCpCont = new GetCustomerProfileController(getCpReq);
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
