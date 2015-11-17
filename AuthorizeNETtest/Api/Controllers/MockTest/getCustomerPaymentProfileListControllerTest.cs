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
    public class getCustomerPaymentProfileListTest : ApiCoreTestBase 
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

        string GetRandomString(string title)
        {
            return String.Format("{0}{1}", title, Counter);
        }

        [Test]
	    public void MockgetCustomerPaymentProfileListTest()
	    {
		    //define all mocked objects as final
            var mockController = GetMockController<getCustomerPaymentProfileListRequest, getCustomerPaymentProfileListResponse>();
            var mockRequest = new getCustomerPaymentProfileListRequest
                {
                    merchantAuthentication = new merchantAuthenticationType() {name = "mocktest", Item = "mockKey", ItemElementName = ItemChoiceType.transactionKey},
                    searchType = CustomerPaymentProfileSearchTypeEnum.cardsExpiringInMonth,
                    month = "2020-12"
                };

            var BankAccountMaskedType = new bankAccountMaskedType()
            {
                accountType = bankAccountTypeEnum.savings,
                accountTypeSpecified = true,
                routingNumber = "1234",
                accountNumber = "1234",
                nameOnAccount = "Test",
                echeckType = echeckTypeEnum.ARC
            };

            var PaymentMaskedType = new paymentMaskedType()
            {
                Item = BankAccountMaskedType
            };

            var CustomerAddress = new customerAddressType
            {
                firstName = GetRandomString("FName"),
                lastName = GetRandomString("LName"),
                company = GetRandomString("Company"),
                address = GetRandomString("StreetAdd"),
                city = "Bellevue",
                state = "WA",
                zip = "98000",
                country = "USA",
                phoneNumber = FormatToPhone(Counter),
                faxNumber = FormatToPhone(Counter + 1),
            };

            var paymentProfile = new customerPaymentProfileListItemType()
            {
                customerPaymentProfileId = 1234,
                customerProfileId = 1234,
                billTo = CustomerAddress,
                payment = PaymentMaskedType
            };

            var PaymentProfiles = new List<customerPaymentProfileListItemType> { paymentProfile };

            var mockResponse = new getCustomerPaymentProfileListResponse
            {
                refId = "1234",
                sessionToken = "sessiontoken",
                totalNumInResultSet = PaymentProfiles.Count,
                paymentProfiles = PaymentProfiles.ToArray()
            };

		    var errorResponse = new ANetApiResponse();
		    var results = new List<String>();
            const messageTypeEnum messageTypeOk = messageTypeEnum.Ok;

            SetMockControllerExpectations<getCustomerPaymentProfileListRequest, getCustomerPaymentProfileListResponse, getCustomerPaymentProfileListController>(
                mockController.MockObject, mockRequest, mockResponse, errorResponse, results, messageTypeOk);
            mockController.MockObject.Execute(AuthorizeNet.Environment.CUSTOM);
            //mockController.MockObject.Execute();
            // or var controllerResponse = mockController.MockObject.ExecuteWithApiResponse(AuthorizeNet.Environment.CUSTOM);
            var controllerResponse = mockController.MockObject.GetApiResponse();
            Assert.IsNotNull(controllerResponse);

		    Assert.IsNotNull(controllerResponse.totalNumInResultSet);
            Assert.IsNotNull(controllerResponse.paymentProfiles);

            LogHelper.info(Logger, "getCustomerPaymentProfileList: Details:{0}", controllerResponse.paymentProfiles);
	    }
    }
}
