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
    public class CreateTransactionSampleTest : ApiCoreTestBase
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

        [Test, Ignore]
        public void SampleCodecreateTransaction()
        {
            LogHelper.info(Logger, "Sample createTransaction");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType()
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                    //amount = SetValidTransactionAmount(Counter),
                    amount = 10000000000000000000,
                    payment = PaymentOne,
                    order =  OrderType,
                    customer =  CustomerDataOne,
                    billTo =  CustomerAddressOne,

                };
            var createRequest = new createTransactionRequest
            {
                refId = RefId,
                transactionRequest = transactionRequestType,
            };
            //create 
            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();
            Assert.IsNotNull(createResponse.transactionResponse);
            LogHelper.info(Logger, "Response: {0}", createResponse);
            DisplayResponse(createResponse, "Create Transaction Response");
            LogHelper.info(Logger, "Created Transaction: {0}", createResponse.transactionResponse);
            Assert.IsNotNull(createResponse.transactionResponse.transId);
            int transId;
            Assert.IsTrue( Int32.TryParse(createResponse.transactionResponse.transId, out transId));
            if (0 == transId)
            {
                ValidateFailure<createTransactionRequest, createTransactionResponse, createTransactionController>(createController, createResponse);
                Assert.IsNotNull(createResponse.transactionResponse.errors);
                foreach (var error in createResponse.transactionResponse.errors)
                {
                    LogHelper.info(Logger, "Error-> Code:{0}, Text:{1}", error.errorCode, error.errorText);
                }
            }
            else
            {
                Assert.AreNotEqual(0, transId);
                ValidateSuccess<createTransactionRequest, createTransactionResponse, createTransactionController>(createController, createResponse);
            }
        }

        [Test]//, Ignore]
        public void CreateTransactionWithCreditCard()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };

            //standard api call to retrieve response
            var paymentType = new paymentType {Item = creditCard};
            var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),
                    payment = paymentType,
                    amount = transactionAmount,
                };
            var request = new createTransactionRequest {transactionRequest = transactionRequest};
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //validate
            Assert.NotNull(response);
            Assert.NotNull(response.messages);
            Assert.NotNull(response.transactionResponse);
            Assert.AreEqual(messageTypeEnum.Ok, response.messages.resultCode);
            Assert.False( string.IsNullOrEmpty(response.transactionResponse.transId));
        }

        [Test]
        public void CreateTransactionWithECheck()
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

        [Test]//, Ignore]
        public void CreateTransactionWithApplePay()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var opaqueData = new opaqueDataType
                {
                    dataDescriptor = "COMMON.APPLE.INAPP.PAYMENT",
                    dataValue =
                        "eyJkYXRhIjoiQkRQTldTdE1tR2V3UVVXR2c0bzdFXC9qKzFjcTFUNzhxeVU4NGI2N2l0amNZSTh3UFlBT2hzaGpoWlBycWRVcjRYd1BNYmo0emNHTWR5KysxSDJWa1BPWStCT01GMjV1YjE5Y1g0bkN2a1hVVU9UakRsbEIxVGdTcjhKSFp4Z3A5ckNnc1NVZ2JCZ0tmNjBYS3V0WGY2YWpcL284WkliS25yS1E4U2gwb3VMQUtsb1VNbit2UHU0K0E3V0tycXJhdXo5SnZPUXA2dmhJcStIS2pVY1VOQ0lUUHlGaG1PRXRxK0grdzB2UmExQ0U2V2hGQk5uQ0hxenpXS2NrQlwvMG5xTFpSVFliRjBwK3Z5QmlWYVdIZWdoRVJmSHhSdGJ6cGVjelJQUHVGc2ZwSFZzNDhvUExDXC9rXC8xTU5kNDdrelwvcEhEY1JcL0R5NmFVTStsTmZvaWx5XC9RSk4rdFMzbTBIZk90SVNBUHFPbVhlbXZyNnhKQ2pDWmxDdXcwQzltWHpcL29iSHBvZnVJRVM4cjljcUdHc1VBUERwdzdnNjQybTRQendLRitIQnVZVW5lV0RCTlNEMnU2amJBRzMiLCJ2ZXJzaW9uIjoiRUNfdjEiLCJoZWFkZXIiOnsiYXBwbGljYXRpb25EYXRhIjoiOTRlZTA1OTMzNWU1ODdlNTAxY2M0YmY5MDYxM2UwODE0ZjAwYTdiMDhiYzdjNjQ4ZmQ4NjVhMmFmNmEyMmNjMiIsInRyYW5zYWN0aW9uSWQiOiJjMWNhZjVhZTcyZjAwMzlhODJiYWQ5MmI4MjgzNjM3MzRmODViZjJmOWNhZGYxOTNkMWJhZDlkZGNiNjBhNzk1IiwiZXBoZW1lcmFsUHVibGljS2V5IjoiTUlJQlN6Q0NBUU1HQnlxR1NNNDlBZ0V3Z2ZjQ0FRRXdMQVlIS29aSXpqMEJBUUloQVBcL1wvXC9cLzhBQUFBQkFBQUFBQUFBQUFBQUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL01Gc0VJUFwvXC9cL1wvOEFBQUFCQUFBQUFBQUFBQUFBQUFBQVwvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cLzhCQ0JheGpYWXFqcVQ1N1BydlZWMm1JYThaUjBHc014VHNQWTd6ancrSjlKZ1N3TVZBTVNkTmdpRzV3U1RhbVo0NFJPZEpyZUJuMzZRQkVFRWF4ZlI4dUVzUWtmNHZPYmxZNlJBOG5jRGZZRXQ2ek9nOUtFNVJkaVl3cFpQNDBMaVwvaHBcL200N242MHA4RDU0V0s4NHpWMnN4WHM3THRrQm9ONzlSOVFJaEFQXC9cL1wvXC84QUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cLys4NXZxdHB4ZWVoUE81eXNMOFl5VlJBZ0VCQTBJQUJHbStnc2wwUFpGVFwva0RkVVNreHd5Zm84SnB3VFFRekJtOWxKSm5tVGw0REdVdkFENEdzZUdqXC9wc2hCWjBLM1RldXFEdFwvdERMYkUrOFwvbTB5Q21veHc9IiwicHVibGljS2V5SGFzaCI6IlwvYmI5Q05DMzZ1QmhlSEZQYm1vaEI3T28xT3NYMkora0pxdjQ4ek9WVmlRPSJ9LCJzaWduYXR1cmUiOiJNSUlEUWdZSktvWklodmNOQVFjQ29JSURNekNDQXk4Q0FRRXhDekFKQmdVckRnTUNHZ1VBTUFzR0NTcUdTSWIzRFFFSEFhQ0NBaXN3Z2dJbk1JSUJsS0FEQWdFQ0FoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQXdKekVsTUNNR0ExVUVBeDRjQUdNQWFBQnRBR0VBYVFCQUFIWUFhUUJ6QUdFQUxnQmpBRzhBYlRBZUZ3MHhOREF4TURFd05qQXdNREJhRncweU5EQXhNREV3TmpBd01EQmFNQ2N4SlRBakJnTlZCQU1lSEFCakFHZ0FiUUJoQUdrQVFBQjJBR2tBY3dCaEFDNEFZd0J2QUcwd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFOQzgra2d0Z212V0YxT3pqZ0ROcmpURUJSdW9cLzVNS3ZsTTE0NnBBZjdHeDQxYmxFOXc0ZklYSkFEN0ZmTzdRS2pJWFlOdDM5ckx5eTd4RHdiXC81SWtaTTYwVFoyaUkxcGo1NVVjOGZkNGZ6T3BrM2Z0WmFRR1hOTFlwdEcxZDlWN0lTODJPdXA5TU1vMUJQVnJYVFBITmNzTTk5RVBVblBxZGJlR2M4N20wckFnTUJBQUdqWERCYU1GZ0dBMVVkQVFSUk1FK0FFSFpXUHJXdEpkN1laNDMxaENnN1lGU2hLVEFuTVNVd0l3WURWUVFESGh3QVl3Qm9BRzBBWVFCcEFFQUFkZ0JwQUhNQVlRQXVBR01BYndCdGdoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQURnWUVBYlVLWUNrdUlLUzlRUTJtRmNNWVJFSW0ybCtYZzhcL0pYditHQlZRSmtPS29zY1k0aU5ERkFcL2JRbG9nZjlMTFU4NFRId05SbnN2VjNQcnY3UlRZODFncTBkdEM4elljQWFBa0NISUkzeXFNbko0QU91NkVPVzlrSmsyMzJnU0U3V2xDdEhiZkxTS2Z1U2dRWDhLWFFZdVpMazJScjYzTjhBcFhzWHdCTDNjSjB4Z2VBd2dkMENBUUV3T3pBbk1TVXdJd1lEVlFRREhod0FZd0JvQUcwQVlRQnBBRUFBZGdCcEFITUFZUUF1QUdNQWJ3QnRBaEJjbCtQZjMrVTRwazEzblZEOW53UVFNQWtHQlNzT0F3SWFCUUF3RFFZSktvWklodmNOQVFFQkJRQUVnWUJhSzNFbE9zdGJIOFdvb3NlREFCZitKZ1wvMTI5SmNJYXdtN2M2VnhuN1phc05iQXEzdEF0OFB0eSt1UUNnc3NYcVprTEE3a3oyR3pNb2xOdHY5d1ltdTlVandhcjFQSFlTK0JcL29Hbm96NTkxd2phZ1hXUnowbk1vNXkzTzFLelgwZDhDUkhBVmE4OFNyVjFhNUpJaVJldjNvU3RJcXd2NXh1WmxkYWc2VHI4dz09In0="
                };
            //standard api call to retrieve response
            var paymentType = new paymentType { Item = opaqueData };

            extendedAmountType transTax = new extendedAmountType();
            transTax.amount = (decimal)12.50;
            transTax.name = "Tax Name";
            transTax.description = "Tax Item";

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                payment = paymentType,
                amount = transactionAmount,
                tax = transTax,
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


        [Test]//, Ignore]
        public void CreateTransactionWithPayPal()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX; //TestEnvironment;

            //set up data based on transaction
            var transactionAmount = new decimal(80.93);
            var payPalData = new payPalType {paypalLc = "IT", paypalPayflowcolor = "FFFF00", cancelUrl = PayPalOne.cancelUrl, successUrl= PayPalOne.successUrl};

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = payPalData };
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
            Assert.False(string.IsNullOrEmpty(response.transactionResponse.transHash));
            Assert.NotNull(response.transactionResponse.secureAcceptance);
            Assert.False(string.IsNullOrEmpty(response.transactionResponse.secureAcceptance.SecureAcceptanceUrl));
        }

        [Test]
        public void CreateCustomerProfileFromTransaction()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            string customerIndx = rnd.Next(99999).ToString();

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };

            //Create and submit transaction with customer info to create profile from.
            var paymentType = new paymentType { Item = creditCard };
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

            string maskedTaxID = "XXXX" + transactionRequest.customer.taxId.Substring(transactionRequest.customer.taxId.Length -4);
            Assert.AreEqual(maskedTaxID, getCustResp.profile.paymentProfiles[0].taxId);
            Assert.AreEqual(createProfResp.customerPaymentProfileIdList[0], getCustResp.profile.paymentProfiles[0].customerPaymentProfileId);//payment profile ID

            string originalCardNumber = ((creditCardSimpleType)transactionRequest.payment.Item).cardNumber.ToString();
            string maskedCardNumber = string.Format("XXXX{0}",originalCardNumber.Substring(originalCardNumber.Length - 4));
            Assert.AreEqual(maskedCardNumber, ((creditCardMaskedType)getCustResp.profile.paymentProfiles[0].payment.Item).cardNumber);//payment card number
            Assert.AreEqual(transactionRequest.billTo.firstName, getCustResp.profile.paymentProfiles[0].billTo.firstName);//billto first name
            Assert.AreEqual(transactionRequest.billTo.lastName, getCustResp.profile.paymentProfiles[0].billTo.lastName);//billto last name
            Assert.AreEqual(transactionRequest.billTo.address, getCustResp.profile.paymentProfiles[0].billTo.address);//billto address
            Assert.AreEqual(transactionRequest.billTo.city, getCustResp.profile.paymentProfiles[0].billTo.city);//billto address//billto city
            Assert.AreEqual(transactionRequest.billTo.state, getCustResp.profile.paymentProfiles[0].billTo.state);//billto address//billto state
            Assert.AreEqual(transactionRequest.billTo.zip, getCustResp.profile.paymentProfiles[0].billTo.zip);//billto address//billto zip
        }

        [Test]
        public void CreateCreditRequestForSettledTransaction()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);


            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            
            // Find a settled credit card transaction and set txnToCredit to its transaction ID
            string txnToCredit = "Not Set";

            if (txnToCredit == "Not Set")
            {
                Assert.Fail("This test requires that you set txnToCredit to the transaction ID of a settled credit card transaction");
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
            var creditCard = new creditCardType { cardNumber = txnCardNo.TrimStart(new char[]{'X'}), expirationDate = "XXXX"};
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

        [Test]
        public void CreateUnlinkedCredit()
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            Random rnd = new Random(DateTime.Now.Millisecond);
            decimal txnAmount = (decimal)rnd.Next(9999) / 100;

            //Create and submit transaction with customer info to create profile from.
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };
            var paymentType = new paymentType { Item = creditCard };

            //Create credit request
            transactionRequestType txnType = new transactionRequestType
            {
                //debug
                amount = txnAmount,
                transactionType = transactionTypeEnum.refundTransaction.ToString(),
                payment = paymentType,
            };


            createTransactionRequest creditReq = new createTransactionRequest { transactionRequest = txnType };
            createTransactionController creditCont = new createTransactionController(creditReq);
            creditCont.Execute();
            createTransactionResponse creditResp = creditCont.GetApiResponse();

            Assert.AreEqual(creditResp.messages.resultCode, messageTypeEnum.Ok);
        }

        [Test]
        public void CreateTransactionPriorAuthCapture()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };
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

            //Get transaction details
            var getDetailsReq = new getTransactionDetailsRequest
            {
                transId = response.transactionResponse.transId
            };
            var getDetailsCont = new getTransactionDetailsController(getDetailsReq);
            getDetailsCont.Execute();
            var getDetailsResp = getDetailsCont.GetApiResponse();


            //Build and execute the capture request.
            var capCC = new creditCardType
            {
                cardNumber = ((creditCardMaskedType)(getDetailsResp.transaction.payment.Item)).cardNumber.TrimStart(new char[]{'X'}),
                expirationDate = "XXXX",
            };

            var capPayment = new paymentType { Item = capCC };

            var capTransactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),
                refTransId = getDetailsResp.transaction.transId,
                authCode = getDetailsResp.transaction.authCode,
                //amount = getDetailsResp.transaction.authAmount,
                //payment = capPayment

            };

            request = new createTransactionRequest { transactionRequest = capTransactionRequest };
            controller = new createTransactionController(request);
            controller.Execute();
            var capResponse = controller.GetApiResponse();

            Assert.NotNull(response);
            Assert.NotNull(response.messages);
            Assert.NotNull(response.transactionResponse);
            Assert.AreEqual(messageTypeEnum.Ok, response.messages.resultCode);
            Assert.False(string.IsNullOrEmpty(response.transactionResponse.transId));
        }

        [Test]
        public void CreateTransactionFromProfile()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            Random rnd = new Random(DateTime.Now.Millisecond);

            string profileRandom = rnd.Next(9999).ToString();

            //Create profile to use in transaction creation
            var profileShipTo = new customerAddressType{
                address = profileRandom + " First St NE",
                city = "Bellevue",
                state = "WA",
                zip = "98007",
                company = "Sample Co " + profileRandom,
                country = "USA",
                firstName = "Sample",
                lastName = "Name",
                phoneNumber = "425 123 4567",
            };

            var paymentProfile = new customerPaymentProfileType{ billTo = profileShipTo, customerType = customerTypeEnum.individual, 
                payment = new paymentType{ Item = new creditCardType{ cardNumber = "4111111111111111", expirationDate = "0622" }},
            };

            var createProfileReq = new createCustomerProfileRequest
            {
                profile = new customerProfileType
                    {
                        description = "SampleProfile " + profileRandom,
                        email = "SampleEmail" + profileRandom + "@Visa.com",
                        shipToList = new customerAddressType[] { profileShipTo },
                        paymentProfiles = new customerPaymentProfileType[] { paymentProfile }
                    }
            };

            var createProfileCont = new createCustomerProfileController(createProfileReq);
            createProfileCont.Execute();
            var createProfileResp = createProfileCont.GetApiResponse();

            //Verify creation of profile
            Assert.AreEqual(createProfileResp.messages.resultCode, messageTypeEnum.Ok);
            Assert.IsNotNull(createProfileResp.customerProfileId);
            Assert.IsNotNull(createProfileResp.customerPaymentProfileIdList);
            Assert.IsNotNull(createProfileResp.customerShippingAddressIdList);

            //Get profile using getCustomerProfileRequest
            var getCustReq = new getCustomerProfileRequest{ customerProfileId = createProfileResp.customerProfileId};
            var getCustCont = new getCustomerProfileController(getCustReq);
            getCustCont.Execute();
            var getCustResp = getCustCont.GetApiResponse();


            //Create Transaction
            //Create instance of customer payment profile using the profile IDs from the profile we loaded above.
            var custPaymentProfile = new AuthorizeNet.Api.Contracts.V1.customerProfilePaymentType { customerProfileId = getCustResp.profile.customerProfileId, paymentProfile = new paymentProfile { paymentProfileId = getCustResp.profile.paymentProfiles[0].customerPaymentProfileId } };

            var testTxn = new transactionRequestType 
            { 
                profile = custPaymentProfile, 
                amount = (decimal)rnd.Next(9999) / 100, 
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString()  
            };

            var txnControler = new createTransactionController(new createTransactionRequest { transactionRequest = testTxn });
            txnControler.Execute();
            var txnControlerResp = txnControler.GetApiResponse();

            //verify transaction succeeded.
            Assert.IsNotNull(txnControlerResp.transactionResponse.messages[0].description);
            Assert.AreEqual(txnControlerResp.transactionResponse.messages[0].description, "This transaction has been approved.");

        }
    }
}
