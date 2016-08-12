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

        [Test]
        public void SampleCodeCreateTransaction()
        {
            LogHelper.info(Logger, "Sample createTransaction");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                    amount = SetValidTransactionAmount(Counter),
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

            //create controller, execute request and get response
            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();

            //Test response
            Assert.IsNotNull(createResponse.transactionResponse);
            LogHelper.info(Logger, "Response: {0}", createResponse);
            DisplayResponse(createResponse, "Create Transaction Response");
            LogHelper.info(Logger, "Created Transaction: {0}", createResponse.transactionResponse);
            Assert.IsNotNull(createResponse.transactionResponse.transId);
            long transId;
            Assert.IsTrue( long.TryParse(createResponse.transactionResponse.transId, out transId));
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

        /// <summary>
        /// This sample demonstrates charging a profile using the CreateTransaction API method
        /// See API example here http://developer.authorize.net/api/reference/#payment-transactions-charge-a-customer-profile
        /// </summary>
        [Test]
        public void SampleCodeCreateTransactionUsingProfile()
        {
            LogHelper.info(Logger, "Sample createTransaction using Profile");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            // Use CIM to create the profile we're going to charge
            var customerProfileId = "0";
            var paymentProfileId = "0";
            Assert.IsTrue(createProfile(out customerProfileId, out paymentProfileId));

            //create a customer payment profile
            customerProfilePaymentType profileToCharge = new customerProfilePaymentType();
            profileToCharge.customerProfileId = customerProfileId;
            profileToCharge.paymentProfile = new paymentProfile { paymentProfileId = paymentProfileId};

            var transactionRequestType = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = SetValidTransactionAmount(Counter),
                profile = profileToCharge
            };
            var createRequest = new createTransactionRequest
            {
                refId = RefId,
                transactionRequest = transactionRequestType,
            };
            //create controller, execute and get response
            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();

            //test response
            Assert.IsNotNull(createResponse.transactionResponse);
            LogHelper.info(Logger, "Response: {0}", createResponse);
            DisplayResponse(createResponse, "Create Transaction Response");
            LogHelper.info(Logger, "Created Transaction: {0}", createResponse.transactionResponse);
            Assert.IsNotNull(createResponse.transactionResponse.transId);
            long transId;
            Assert.IsTrue(long.TryParse(createResponse.transactionResponse.transId, out transId));
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

        //Create Customer Profile and Customer Payment Profile, returning their IDs.
        private Boolean createProfile(out String customerProfileId, out String paymentProfileId)
        {

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            var rnd = new AnetRandom(DateTime.Now.Millisecond);
            string custIndx = rnd.Next(99999).ToString();

            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };
            var paymentType = new paymentType {Item = creditCard};

            var paymentProfile = new customerPaymentProfileType{ payment = paymentType };

            var createRequest = new createCustomerProfileRequest
            {
                profile = new customerProfileType{
                                    merchantCustomerId = "TSTCSTER"+custIndx,
                                    paymentProfiles = new customerPaymentProfileType[]{ paymentProfile }
                                    }   
            };

            //create profiles and get response
            var createController = new createCustomerProfileController(createRequest);
            var createResponse = createController.ExecuteWithApiResponse();

            //validate response
            if (messageTypeEnum.Ok != createResponse.messages.resultCode)
            {
                customerProfileId = "0";
                paymentProfileId = "0";
                return false;
            }
            else
            {
                Assert.NotNull(createResponse.customerProfileId);
                Assert.NotNull(createResponse.customerPaymentProfileIdList);
                Assert.AreNotEqual(0, createResponse.customerPaymentProfileIdList.Length);

                customerProfileId = createResponse.customerProfileId;
                paymentProfileId = createResponse.customerPaymentProfileIdList[0];

                return true;
            }
        }

        [Test]
        public void SampleCodeCreateTransactionWithCreditCard()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data for transaction
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
            Assert.AreEqual("1", response.transactionResponse.messages[0].code);
        }

        [Test]
        [Ignore("To run this test, use your ApplePay ApiLoginIdKey and TransactionKey to configure this test below.")]
        public void SampleCodeCreateTransactionWithApplePay()
        {
            // The test setup.
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType
                {
                    name = "????",  // your ApplyPay ApiLoginIdKey
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = "????"   // your ApplyPay TransactionKey
                };
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
            Assert.AreEqual("1", response.transactionResponse.messages[0].code);
        }

        [Test]
        [Ignore("To run this test, use dataKey, dataValue, callId details and Merchant ApiLoginIdKey and TransactionKey to configure this test below.")]
        public void SampleCodeCreateTransactionWithVisaCheckOut()
        {
            // The test setup.
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = "5KP3u95bQpv",
                ItemElementName = ItemChoiceType.transactionKey,
                Item = "346HZ32z3fP4hTG2",
            };

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var opaqueDataType = new opaqueDataType
            {
                dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
                dataKey = "NQzcMISSxLX789w+CGX+tXi3lKntO1dpZbZaREOUprVRByJkg1xnpc2Wx9aT5/BLOxQmHqmIsjjy+tF6HqKKGwovvXjIS3fE3y3tBRNbz8D7y6vYMup+AWbEvZqDEBSi",
                dataValue = "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"
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = opaqueDataType };
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                payment = paymentType,
                amount = transactionAmount,
                callId = "1482912778237697701"
            };
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();

            //validate
            Assert.AreEqual("1", response.transactionResponse.messages[0].code);
        }

        [Test]
        public void SampleCodeCreateTransactionWithPayPal()
        {
            /*
             * Please enable the PayPal feature of your ANet merchant account.
             */ 

            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX; //TestEnvironment;

            //set up data for transaction
            var transactionAmount = SetValidTransactionAmount(Counter) / 100;
            var payPalData = new payPalType 
            {
                paypalLc = "IT",
                paypalPayflowcolor = "FFFF00",
                successUrl = PayPalOne.successUrl,
                cancelUrl = PayPalOne.cancelUrl,
            };

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

            //validate. The code 2000 is: Need the payer's consent.
            Assert.AreEqual("2000", response.transactionResponse.messages[0].code);
        }

        [Test]
        [Ignore("Requires user to specify settled transaction")]
        public void SampleCodeCreateCreditRequestForSettledTransaction()
        {
            var rnd = new AnetRandom(DateTime.Now.Millisecond);


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
            Assert.AreEqual(messageTypeEnum.Ok, gtdResp.messages.resultCode);

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

        //Tests execution of credit without a linked transaction.
        [Test]
        public void SampleCodeCreateUnlinkedCredit()
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            decimal txnAmount = SetValidTransactionAmount(Counter) / 100;

            //Set payment info for credit
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };
            var paymentType = new paymentType { Item = creditCard };

            //Create credit request
            transactionRequestType txnType = new transactionRequestType
            {
                amount = txnAmount,
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
        public void SampleCodeCreateTransactionPriorAuthCapture()
        {
            //Common code to set for all requests
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //set up data based on transaction
            var transactionAmount = SetValidTransactionAmount(Counter);
            var creditCard = new creditCardType { cardNumber = "4111111111111111", expirationDate = "0622" };

            //Build auth only transaction request.
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
                cardNumber = ((creditCardMaskedType)(getDetailsResp.transaction.payment.Item)).cardNumber.TrimStart(new char[] { 'X' }),
                expirationDate = "XXXX",
            };

            var capPayment = new paymentType { Item = capCC };

            var capTransactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),
                refTransId = getDetailsResp.transaction.transId,
                authCode = getDetailsResp.transaction.authCode,
            };

            request = new createTransactionRequest { transactionRequest = capTransactionRequest };
            controller = new createTransactionController(request);
            controller.Execute();
            var capResponse = controller.GetApiResponse();

            //validate
            Assert.AreEqual("1", capResponse.transactionResponse.messages[0].code);
        }

        [Test]
        public void TransactionRequest_HandleError()
        {
            LogHelper.info(Logger, "CreateProfileWithCreateTransactionRequestTest");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = SetValidTransactionAmount(Counter),
                payment = new paymentType { Item = new creditCardType { cardNumber = "0111111111111111", expirationDate = "122035" } },
                order = OrderType,
                customer = CustomerDataOne,
                billTo = CustomerAddressOne,
                shipTo = CustomerAddressOne,
            };
            var createRequest = new createTransactionRequest
            {
                refId = RefId,
                transactionRequest = transactionRequestType,
            };
            //create controller, execute and get response
            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();

            //Validate error code where request is submitted properly, but request fails.
            Assert.AreEqual("6", createResponse.transactionResponse.errors[0].errorCode);

            //Validate error code where submission of request fails.
            ((creditCardType)transactionRequestType.payment.Item).cardNumber = "01";
            createController = new createTransactionController(createRequest);
            createController.Execute();

            if (createController.GetApiResponse() == null)
            {
                var errorResponse = createController.GetErrorResponse();
                Assert.AreEqual("E00003", errorResponse.messages.message[0].code);
            }
        }
    }
}
