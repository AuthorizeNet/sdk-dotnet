using System.Diagnostics;

namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using NUnit.Framework;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Util;

    [TestFixture]
    public class CreateTransactionTest : ApiCoreTestBase
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
        public void CreateProfileWithCreateTransactionRequestTest()
        {
            LogHelper.info(Logger, "CreateProfileWithCreateTransactionRequestTest");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                amount = SetValidTransactionAmount(Counter),
                payment = PaymentOne,
                order = OrderType,
                customer = CustomerDataOne,
                billTo = CustomerAddressOne,
                shipTo = CustomerAddressOne,
                profile = new customerProfilePaymentType
                    {
                        createProfile = true,
                        createProfileSpecified = true,
                    },
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

            //validate response
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
            var profileResponse = createResponse.profileResponse;
            Assert.IsNotNull(profileResponse);
            Assert.IsNotNull(profileResponse.customerProfileId);
            Assert.IsNotNull(profileResponse.customerPaymentProfileIdList);
            Assert.IsNotNull(profileResponse.customerShippingAddressIdList);
            Assert.AreNotEqual("0", profileResponse.customerProfileId);

            Assert.AreEqual(1, profileResponse.customerPaymentProfileIdList.Length);
            Assert.AreNotEqual("0", profileResponse.customerPaymentProfileIdList[0]);

            Assert.AreEqual(1, profileResponse.customerShippingAddressIdList.Length);
            Assert.AreNotEqual("0", profileResponse.customerShippingAddressIdList[0]);
        }
        
        /// <summary>
        /// @Zalak 
        /// For issue #62 Github Dot.net SDK
        /// </summary>
        /// <param name="transactionRequestParameter"></param>
        /// <returns></returns>
        private createTransactionController CreateTransactionRequestTest(transactionRequestType transactionRequestParameter)
        {
            if (transactionRequestParameter == null)
            {
                throw new ArgumentNullException("transactionRequestParameter");
            }
            LogHelper.info(Logger, "CreateTransactionRequestTest");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = transactionRequestParameter;
            var createRequest = new createTransactionRequest
                {
                    refId = RefId,
                    transactionRequest = transactionRequestType,
                };
            //create controller
            var createController = new createTransactionController(createRequest);
         
            return createController;
        }

        

        //@Zalak
        /// <summary>
        /// Issue number #62 github dot-net sdk
        /// </summary>
        [Test]
        public void CreateRefundWithCustomerProfileRequestTest()
        {
            LogHelper.info(Logger, "CreateRefundWithCustomerProfileRequestTest");

            //created a new transaction 
            var chargedTransactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                    amount = SetValidTransactionAmount(Counter),
                    payment = PaymentOne,
                    customer = CustomerDataOne,
                    billTo = CustomerAddressOne,
                    profile = new customerProfilePaymentType
                        {
                            createProfile = true,
                            //createProfileSpecified = true, //TODO : Update RequestFactory for Specified
                        },
                };
            
            var createController = CreateTransactionRequestTest(chargedTransactionRequest);
            if(createController == null)
                throw new ArgumentNullException("createController");

            var createResponse = createController.ExecuteWithApiResponse();
            if (createResponse == null)
            {
                throw new ArgumentNullException("createResponse");
            }

            if (createResponse.transactionResponse.transId == null)
            {
                throw new ArgumentNullException("TransId is null");
            }
            chargedTransactionRequest.refTransId = createResponse.transactionResponse.transId;

            if (createResponse.profileResponse == null)
            {
                throw new ArgumentNullException("profileResponse");
            }
            var profileResponse = createResponse.profileResponse;
            
            // creating a refund transaction request for above transaction using customer profile id and customer payment profile id
            chargedTransactionRequest.transactionType = transactionTypeEnum.refundTransaction.ToString();
           chargedTransactionRequest.profile = new customerProfilePaymentType();
           chargedTransactionRequest.profile.customerProfileId = profileResponse.customerProfileId;
            chargedTransactionRequest.profile.paymentProfile = new paymentProfile()
            {
                 paymentProfileId = profileResponse.customerPaymentProfileIdList[0],
            };
         
            chargedTransactionRequest.customer = null;
            chargedTransactionRequest.billTo = null;
            chargedTransactionRequest.payment = null;

            createController = CreateTransactionRequestTest(chargedTransactionRequest);
            createResponse = createController.ExecuteWithApiResponse();
            Assert.IsNotNull(createResponse);
            //currently the transaction is failing because the bug fix is on server end
            var errorResponse = createResponse.messages;
            Assert.AreEqual(1, errorResponse.message.Length);
            Assert.AreEqual("E00051", errorResponse.message[0].code);
            Assert.AreEqual(errorResponse.message[0].text, "The original transaction was not issued for this payment profile.");
       }

        /// <summary>
        /// @Zalak
        /// Issue #62: If shipping address is not included in request then it will be empty it will not be same as billing address
        /// </summary>
        [Test]
        public void CreateTransactionShippingAddressTest()
        {
            LogHelper.info(Logger, "CreateRefundWithCustomerProfileRequestTest");

            //created a new transaction 
            var chargedTransactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                    amount = SetValidTransactionAmount(Counter),
                    payment = PaymentOne,
                    customer = CustomerDataOne,
                    billTo = CustomerAddressOne,
                    profile = new customerProfilePaymentType
                        {
                            createProfile = true,
                            createProfileSpecified = true, 
                        },
                };
            
            var createController = CreateTransactionRequestTest(chargedTransactionRequest);
            var createResponse = createController.ExecuteWithApiResponse();
            if (createResponse == null)
                throw new ArgumentNullException("createResponse");

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
                ValidateSuccess<createTransactionRequest, createTransactionResponse, createTransactionController>(createController, createResponse);
                Assert.AreNotEqual(0, transId);
            }

            var profileResponse = createResponse.profileResponse;
            Assert.IsNotNull(profileResponse);
            Assert.IsNotNull(profileResponse.customerProfileId);
            Assert.IsNotNull(profileResponse.customerPaymentProfileIdList);
            Assert.AreEqual("",profileResponse.customerShippingAddressIdList);
            Assert.AreNotEqual("0", profileResponse.customerProfileId);

            Assert.AreEqual(1, profileResponse.customerPaymentProfileIdList.Length);
            Assert.AreNotEqual("0", profileResponse.customerPaymentProfileIdList[0]);

            Assert.AreEqual(0, profileResponse.customerShippingAddressIdList.Length);
           
        }

        [Test]
        public void CreateTransactionInvalidRequestSchemaValidationTest()
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = CustomMerchantAuthenticationType;
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var transactionRequestType = new transactionRequestType
            {
                //removing the transaction type here is important to expect the schema validation error
                //transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
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
            };

            //create controller, execute and get response 
            var createController = new createTransactionController(createRequest);
            createController.Execute();
            var createResponse = createController.GetApiResponse();

            //validate response
            Assert.IsNull(createResponse);
            var errorResponse = createController.GetErrorResponse();
            Assert.IsNotNull(errorResponse);
            Assert.IsNotNull(errorResponse.messages);
            Assert.IsNotNull(errorResponse.messages.message);
            Assert.AreEqual(messageTypeEnum.Error, errorResponse.messages.resultCode);
            Assert.AreEqual(1, errorResponse.messages.message.Length);
            Assert.AreEqual("E00003", errorResponse.messages.message[0].code);
            ValidateErrorCode(errorResponse.messages, "E00003");
        }

        [Test]
        public void DecryptPaymentDataRequestTest()
        {
            LogHelper.info(Logger, "decryptPaymentDataRequestTest");

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = "5KP3u95bQpv",
                ItemElementName = ItemChoiceType.transactionKey,
                Item = "346HZ32z3fP4hTG2",
            };
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = TestEnvironment;

            //create a transaction
            var opaqueDataType = new opaqueDataType
            {
                dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
                dataKey = "foFBEbhXljevQQasx5Q87hzj57xvUl4iBmXDdB1vs/Lm/M1uKJiF9V5QxI0A6NvAtIckMvutSl0Chz2SNoSeBuTRzK0y4IlfnfWKnJF7a1LV/bjZokTtFKINdZ+Ks9RB",
                dataValue = "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"
            };

            var decryptPaymentDataRequest = new decryptPaymentDataRequest()
            {
                opaqueData = opaqueDataType,
                callId = "1166739390571781401"
            };
            //create controller, execute and get response
            var decryptPaymentDataController = new decryptPaymentDataController(decryptPaymentDataRequest);
            decryptPaymentDataController.Execute();
            var decryptPaymentDataResponse = decryptPaymentDataController.GetApiResponse();

            //validate response
            Assert.IsNotNull(decryptPaymentDataResponse);
            LogHelper.info(Logger, "Response: {0}", decryptPaymentDataResponse);
            Assert.IsNotNull(decryptPaymentDataResponse.messages);
            Assert.IsNotNull(decryptPaymentDataResponse.messages.message);
            Assert.AreEqual(decryptPaymentDataResponse.messages.resultCode, messageTypeEnum.Ok);
            Assert.AreEqual(decryptPaymentDataResponse.billingInfo.firstName, "Authorize");
            Assert.AreEqual(decryptPaymentDataResponse.billingInfo.lastName, "Tester");
            Assert.AreEqual(decryptPaymentDataResponse.cardInfo.cardNumber, "XXXX4242");
        }
    }
}
/*

The current plan for the bug bash is postponed until arkadiy’s investigation of the echeck bug fix.
(If the at’s fix go in, we might need to wait for the brand new build in PLUM for the bug bash).

I used the api docs to get this to work for JSON, I just needed to remove the namespace line.

I’m hoping to have a new version of API Docs with static request/responses (so the paypals really help) by the 3pm bug bash.

This is what I have so far, some sample JSON and XML requests for PayPal.
I’m working on the others.

Auth Only PayPal--------------------------------------------------------

XML request
<?xml version="1.0" encoding="utf-16"?>
<createTransactionRequest xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
  <transactionRequest>
    <transactionType>authOnlyTransaction</transactionType>
    <amount>5</amount>
    <payment>
      <payPal>
        <successUrl>http://blah.com</successUrl>
        <cancelUrl>http://blah.com</cancelUrl>
      </payPal>
    </payment>
  </transactionRequest>
</createTransactionRequest>

JSON request--------------
{
  "createTransactionRequest":
  {
    "merchantAuthentication":
    {
    ,"transactionRequest":
    {
      "transactionType":"authOnlyTransaction","amount":"5","payment":
      {
        "payPal":
        {
          "successUrl":"http://blah.com","cancelUrl":"http://blah.com"}
      }
    }}
}

JSON response
{
  "transactionResponse":{
    "responseCode":"5","rawResponseCode":"0","transId":"2149186510","refTransID":"","transHash":"4B59979D6C305BF85D82D803FD2A776A","testRequest":"0","accountType":"PayPal","messages":[{
      "code":"2000","description":"Need payer consent."}
                                                                                                                                                                                       ],"secureAcceptance":{
                                                                                                                                                                                         "SecureAcceptanceUrl":"https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=EC-WHUJYBCWWQFFSPUTN"}
  }
  ,"messages":{
    "resultCode":"Ok","message":[{
      "code":"I00001","text":"Successful."}
                                ]}
}


Auth Only Continue----------------------------------------------

<?xml version="1.0" encoding="utf-8"?>
<createTransactionRequest xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns="AnetApi/xml/v1/schema/AnetApiSchema.xsd">
  <transactionRequest>
    <transactionType>authOnlyContinueTransaction</transactionType>
    <amount>5.00</amount>
    <payment>
      <payPal>
        <payerID>SNTNC44D4GXGC</payerID>
      </payPal>
    </payment>
    <refTransId>2149186511</refTransId>
  </transactionRequest>
</createTransactionRequest>

JSON request------------------------------
{
  "createTransactionRequest":
  {
    "merchantAuthentication":
    {
    ,"transactionRequest":
    {
      "transactionType":"authOnlyContinueTransaction","amount":"5.00","payment":
      {
        "payPal":
        {
          "payerID":"SNTNC44D4GXGC"}
      }
      ,"refTransId":"2149186511"}
}


JSON response-------------------------------
{
  "transactionResponse":{
    "responseCode":"1","rawResponseCode":"0","transId":"2149186511","refTransID":"2149186511","transHash":"578362630D36EDEB7F00672739422907","testRequest":"0","accountType":"PayPal","messages":[{
      "code":"1","description":"This transaction has been approved."}
                                                                                                                                                                                                 ],"secureAcceptance":{
                                                                                                                                                                                                   "PayerID":"SNTNC44D4GXGC"}
  }
  ,"messages":{
    "resultCode":"Ok","message":[{
      "code":"I00001","text":"Successful."}
                                ]}
}
*/