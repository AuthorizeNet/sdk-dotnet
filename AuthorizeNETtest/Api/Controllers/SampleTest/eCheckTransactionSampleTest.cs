using AuthorizeNet.Utility;

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

            //standard api call to retrieve api response
            var paymentType = new paymentType { Item = echeck };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                payment = paymentType,
                amount = (decimal)transactionAmount,
            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //validate
            Assert.AreEqual("1", response.transactionResponse.messages[0].code);
        }

        [Test]
        public void CreateTransactionWithECheckAuth_Only()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            decimal transactionAmount = SetValidTransactionAmount(Counter);
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
            Assert.AreEqual("1", response.transactionResponse.messages[0].code);
        }




        [Test]
        public void CreateCustomerProfileFromECheckTransaction()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);
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
                amount = (decimal)transactionAmount,
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

            //validate
            Assert.AreEqual("1", response.transactionResponse.messages[0].code);
        }

        [Test]
        [Ignore("Requires user to specify settled transaction")]
        public void CreateCreditRequestForSettledECheckTransaction()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);


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

            //validate
            Assert.AreEqual("1", creditResp.transactionResponse.messages[0].code);
        }

        [Test]
        public void CreateTransactionWithECheckCapturePriorAuth()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            var rnd = new AnetRandom(DateTime.Now.Millisecond);

            //Build and submit an Auth only transaction that can later be captured.
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

            //Get transaction details
            var getDetailsReq = new getTransactionDetailsRequest
            {
                transId = response.transactionResponse.transId
            };
            var getDetailsCont = new getTransactionDetailsController(getDetailsReq);
            getDetailsCont.Execute();
            var getDetailsResp = getDetailsCont.GetApiResponse();


            //Build and execute the capture request.
            var capECheck = new bankAccountType
            {
                accountNumber = ((AuthorizeNet.Api.Contracts.V1.bankAccountMaskedType)(getDetailsResp.transaction.payment.Item)).accountNumber.TrimStart(new char[] { 'X' }),
                routingNumber = "XXXX",
                nameOnAccount = ((AuthorizeNet.Api.Contracts.V1.bankAccountMaskedType)(getDetailsResp.transaction.payment.Item)).nameOnAccount,
                bankName = ((AuthorizeNet.Api.Contracts.V1.bankAccountMaskedType)(getDetailsResp.transaction.payment.Item)).bankName,
                echeckType = ((AuthorizeNet.Api.Contracts.V1.bankAccountMaskedType)(getDetailsResp.transaction.payment.Item)).echeckType,
            };

            var capPayment = new paymentType { Item = capECheck };



            var capTransactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),
                refTransId = getDetailsResp.transaction.transId,
            };

            request = new createTransactionRequest { transactionRequest = capTransactionRequest };
            controller = new createTransactionController(request);
            controller.Execute();
            var capResponse = controller.GetApiResponse();

            //validate
            Assert.AreEqual("1", capResponse.transactionResponse.messages[0].code);
        }
    }
}
