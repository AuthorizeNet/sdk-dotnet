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
    public class CreateECheckTransactionSampleTest : ApiCoreTestBase
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
        public void CreateTransactionWithECheck_AuthCapture()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var echeck = new bankAccountType { accountNumber = "123456", accountType = bankAccountTypeEnum.checking, checkNumber = "1234", bankName = "Bank of Seattle", routingNumber = "125000024", echeckType = echeckTypeEnum.WEB, nameOnAccount = "Joe Customer" };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = echeck };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                payment = paymentType,
                amount = transactionAmount,
            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //validate
            Assert.NotNull(response);
            Assert.NotNull(response.messages);
            Assert.NotNull(response.transactionResponse);
            Assert.AreEqual(messageTypeEnum.Ok, response.messages.resultCode);
            Assert.False(string.IsNullOrEmpty(response.transactionResponse.transId));
        }

        [Test]
        public void CreateTransactionWithECheckAuth_Only()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var echeck = new bankAccountType { accountNumber = "123456", accountType = bankAccountTypeEnum.checking, checkNumber = "1234", bankName = "Bank of Seattle", routingNumber = "125000024", echeckType = echeckTypeEnum.WEB, nameOnAccount = "Joe Customer" };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = echeck };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                payment = paymentType,
                amount = transactionAmount,
            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //validate
            Assert.NotNull(response);
            Assert.NotNull(response.messages);
            Assert.NotNull(response.transactionResponse);
            Assert.AreEqual(messageTypeEnum.Ok, response.messages.resultCode);
            Assert.False(string.IsNullOrEmpty(response.transactionResponse.transId));
        }




        [Test]
        public void CreateCustomerProfileFromECheckTransaction()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            string customerIndx = rnd.Next(99999).ToString();

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var echeck = new bankAccountType { accountNumber = "123456", accountType = bankAccountTypeEnum.checking, checkNumber = "1234", bankName = "Bank of Seattle", routingNumber = "125000024", echeckType = echeckTypeEnum.WEB, nameOnAccount = "Joe Customer" };

            //Create and submit transaction with customer info to create profile from.
            var paymentType = new paymentType { Item = echeck };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                payment = paymentType,
                amount = transactionAmount,
                customer = new customerDataType
                {
                    email = string.Format("Customer{0}@visa.com", customerIndx),
                    taxId = string.Format("{0}{1}{2}", rnd.Next(999).ToString("000"), rnd.Next(99).ToString("00"), rnd.Next(9999).ToString("0000"))
                },
                billTo = new customerAddressType
                {
                    firstName = "New",
                    lastName = string.Format("Customer{0}", customerIndx),
                    company = "New Company",
                    address = "1234 Sample St NE",
                    city = "Bellevue",
                    state = "WA",
                    zip = "98001"

                },

                shipTo = new customerAddressType
                {
                    firstName = "New",
                    lastName = string.Format("Customer{0}", customerIndx),
                    company = "New Company",
                    address = "1234 Sample St NE",
                    city = "Bellevue",
                    state = "WA",
                    zip = "98001"

                }

            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //Verify that transaction was accepted and save the transaction ID
            Assert.AreEqual(messageTypeEnum.Ok, response.messages.resultCode);
            string txnID = response.transactionResponse.transId;


            //Build and submit request to create Customer Profile based on the accepted transaction
            createCustomerProfileFromTransactionRequest profileFromTransReq = new createCustomerProfileFromTransactionRequest();
            profileFromTransReq.transId = txnID;

            createCustomerProfileFromTransactionController profileFromTrxnController = new createCustomerProfileFromTransactionController(profileFromTransReq);
            profileFromTrxnController.Execute();
            createCustomerProfileResponse createProfResp = profileFromTrxnController.GetApiResponse();
            Assert.AreEqual(messageTypeEnum.Ok, createProfResp.messages.resultCode);

            //Get customer profile and verify that profile data matches the data submitted with the transaction
            getCustomerProfileRequest profileReq = new getCustomerProfileRequest
            {
                customerProfileId = createProfResp.customerProfileId
            };

            getCustomerProfileController getCustContr = new getCustomerProfileController(profileReq);
            getCustContr.Execute();
            var getCustResp = getCustContr.GetApiResponse();

            //Validate customer profile
            Assert.AreEqual(createProfResp.customerProfileId, getCustResp.profile.customerProfileId);
            Assert.AreEqual(transactionRequest.customer.email, getCustResp.profile.email);

            string maskedTaxID = "XXXX" + transactionRequest.customer.taxId.Substring(transactionRequest.customer.taxId.Length - 4);
            Assert.AreEqual(maskedTaxID, getCustResp.profile.paymentProfiles[0].taxId);
            Assert.AreEqual(createProfResp.customerPaymentProfileIdList[0], getCustResp.profile.paymentProfiles[0].customerPaymentProfileId);//payment profile ID

            string originalAccountNumber = ((bankAccountType)transactionRequest.payment.Item).accountNumber;
            string maskedAccountNumber = string.Format("XXXX{0}", originalAccountNumber.Substring(originalAccountNumber.Length - 4));
            Assert.AreEqual(maskedAccountNumber, ((bankAccountMaskedType)getCustResp.profile.paymentProfiles[0].payment.Item).accountNumber);//payment card number
            Assert.AreEqual(transactionRequest.billTo.firstName, getCustResp.profile.paymentProfiles[0].billTo.firstName);//billto first name
            Assert.AreEqual(transactionRequest.billTo.lastName, getCustResp.profile.paymentProfiles[0].billTo.lastName);//billto last name
            Assert.AreEqual(transactionRequest.billTo.address, getCustResp.profile.paymentProfiles[0].billTo.address);//billto address
            Assert.AreEqual(transactionRequest.billTo.city, getCustResp.profile.paymentProfiles[0].billTo.city);//billto address//billto city
            Assert.AreEqual(transactionRequest.billTo.state, getCustResp.profile.paymentProfiles[0].billTo.state);//billto address//billto state
            Assert.AreEqual(transactionRequest.billTo.zip, getCustResp.profile.paymentProfiles[0].billTo.zip);//billto address//billto zip

            Assert.AreEqual(transactionRequest.shipTo.firstName, getCustResp.profile.shipToList[0].firstName);//shipto first name
            Assert.AreEqual(transactionRequest.shipTo.lastName, getCustResp.profile.shipToList[0].lastName);//shipto last name
            Assert.AreEqual(transactionRequest.shipTo.address, getCustResp.profile.shipToList[0].address);//shipto address
            Assert.AreEqual(transactionRequest.shipTo.city, getCustResp.profile.shipToList[0].city);//shipto address//billto city
            Assert.AreEqual(transactionRequest.shipTo.state, getCustResp.profile.shipToList[0].state);//shipto address//billto state
            Assert.AreEqual(transactionRequest.shipTo.zip, getCustResp.profile.shipToList[0].zip);//shipto address//billto zip
        }

        [Test]
        public void CreateCreditRequestForSettledECheckTransaction()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);


            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;


            // Find a settled credit card transaction and set txnToCredit to its transaction ID
            string txnToCredit = "Not Set";

            if (txnToCredit == "Not Set")
            {
                Assert.Fail("This test requires that you set txnToCredit to the transaction ID of a settled eCheck card transaction");
            }


            //get details of the specified transaction
            decimal txnAmount = 0m;
            string txnCardNo = string.Empty;

            var gtdReq = new getTransactionDetailsRequest { transId = txnToCredit };
            var gtdCont = new getTransactionDetailsController(gtdReq);
            gtdCont.Execute();
            var gtdResp = gtdCont.GetApiResponse();

            //Test the transaction before continuing
            Assert.IsNotNull(gtdResp.transaction);
            Assert.AreEqual(gtdResp.messages.resultCode, messageTypeEnum.Ok);
            Assert.AreEqual(gtdResp.transaction.transactionStatus, "settledSuccessfully");

            txnAmount = gtdResp.transaction.settleAmount;
            txnCardNo = ((AuthorizeNet.Api.Contracts.V1.creditCardMaskedType)(gtdResp.transaction.payment.Item)).cardNumber;



            //Create payment type that matches transaction to credit
            var creditCard = new creditCardType { cardNumber = txnCardNo.TrimStart(new char[] { 'X' }), expirationDate = "XXXX" };
            var paymentType = new paymentType { Item = creditCard };


            //Create credit request
            transactionRequestType txnType = new transactionRequestType
            {
                amount = txnAmount,
                refTransId = txnToCredit,
                transactionType = transactionTypeEnum.refundTransaction.ToString(),
                payment = paymentType,
            };

            createTransactionRequest creditReq = new createTransactionRequest { transactionRequest = txnType };
            createTransactionController creditCont = new createTransactionController(creditReq);
            creditCont.Execute();
            createTransactionResponse creditResp = creditCont.GetApiResponse();

            Assert.IsNotNull(creditResp);
            Assert.AreEqual(creditResp.messages.resultCode, messageTypeEnum.Ok);
        }
    }
}
