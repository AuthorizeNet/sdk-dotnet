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