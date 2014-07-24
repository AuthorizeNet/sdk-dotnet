namespace AuthorizeNet.ApiCore.Controllers.Test
{
    using System;
    using System.Collections.Generic;
    using AuthorizeNet.ApiCore.Contracts.V1;
    using AuthorizeNet.ApiCore.Controllers.Bases;
    using AuthorizeNet.Test;
    using AuthorizeNet.Util;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    // ReSharper disable FieldCanBeMadeReadOnly.Local
    // ReSharper disable NotAccessedField.Local
#pragma warning disable 169
#pragma warning disable 649
    [TestClass]
    public abstract class ApiCoreTestBase {

	    protected static Log Logger = LogFactory.getLog(typeof(ApiCoreTestBase));
	
	    protected static IDictionary<String, String> ErrorMessages ;
	
	    protected static AuthorizeNet.Environment TestEnvironment = AuthorizeNet.Environment.SANDBOX;
        //protected static AuthorizeNet.Environment TestEnvironment = AuthorizeNet.Environment.HOSTED_VM;

        static Merchant _cnpMerchant;
	    static Merchant _cpMerchant ;
	    static readonly String CnpApiLoginIdKey ;
	    static readonly String CnpTransactionKey ;
	    static String _cnpMd5HashKey ;
	    static readonly String CpApiLoginIdKey ;
	    static readonly String CpTransactionKey ;
	    static String _cpMd5HashKey ;
	
	    DateTime _pastDate;
	    DateTime _nowDate;
	    DateTime _futureDate;
	    String _nowString ;
	    DateTime _now = DateTime.UtcNow;

	    protected string RefId ;
	    int _counter;
	    String _counterStr ;

	    protected merchantAuthenticationType CnpMerchantAuthenticationType ;
        protected merchantAuthenticationType CpMerchantAuthenticationType;

        protected ARBSubscriptionType ArbSubscriptionOne;

        protected ARBSubscriptionType ArbSubscriptionTwo;
        protected bankAccountType BankAccountOne;
        protected creditCardTrackType TrackDataOne;
        protected creditCardType CreditCardOne;
        protected customerAddressType CustomerAddressOne;
        protected customerDataType CustomerDataOne;
        protected customerPaymentProfileType CustomerPaymentProfileOne;
        protected customerProfileType CustomerProfileType;
        protected customerType CustomerOne;
        protected customerType CustomerTwo;
        protected driversLicenseType DriversLicenseOne; 
        protected encryptedTrackDataType EncryptedTrackDataOne;
        protected nameAndAddressType NameAndAddressTypeOne;
        protected nameAndAddressType NameAndAddressTypeTwo;
        protected orderType OrderType;
        protected paymentScheduleType PaymentScheduleTypeOne;
        protected paymentType PaymentOne;
        protected payPalType PayPalOne;
	
	    private readonly Random _random = new Random();
	    static ApiCoreTestBase() {
		    //getPropertyFromNames get the value from properties file or environment
		    CnpApiLoginIdKey = UnitTestData.GetPropertyFromNames(Constants.ENV_API_LOGINID, Constants.PROP_API_LOGINID);
		    CnpTransactionKey = UnitTestData.GetPropertyFromNames(Constants.ENV_TRANSACTION_KEY, Constants.PROP_TRANSACTION_KEY);
		    _cnpMd5HashKey = null;
		    CpApiLoginIdKey = UnitTestData.GetPropertyFromNames(Constants.ENV_CP_API_LOGINID, Constants.PROP_CP_API_LOGINID);
		    CpTransactionKey = UnitTestData.GetPropertyFromNames(Constants.ENV_CP_TRANSACTION_KEY, Constants.PROP_CP_TRANSACTION_KEY);
		    _cpMd5HashKey = UnitTestData.GetPropertyFromNames(Constants.ENV_MD5_HASHKEY, Constants.PROP_MD5_HASHKEY);

		    if ((null == CnpApiLoginIdKey) ||
			    (null == CnpTransactionKey) ||
			    (null == CpApiLoginIdKey) ||
			    (null == CpTransactionKey))
		    {
			    throw new ArgumentException("LoginId and/or TransactionKey have not been set.");
		    }

		    _cnpMerchant = Merchant.CreateMerchant( TestEnvironment, CnpApiLoginIdKey, CnpTransactionKey);
		    _cpMerchant = Merchant.CreateMerchant( TestEnvironment, CpApiLoginIdKey, CpTransactionKey);

		    ErrorMessages = new Dictionary<string, string>();
	    }

	    [ClassInitialize]
        public static void SetUpBeforeClass(TestContext context)
        {
		    ErrorMessages.Add("E00003", "");
		    ErrorMessages.Add("E00027", "");
		    ErrorMessages.Add("E00040", "");
		    ErrorMessages.Add("E00090", "PaymentProfile cannot be sent with payment data." );
		    ErrorMessages.Add("E00091", "PaymentProfileId cannot be sent with payment data.");		
		    ErrorMessages.Add("E00092", "ShippingProfileId cannot be sent with ShipTo data.");		
		    ErrorMessages.Add("E00093", "PaymentProfile cannot be sent with billing data.");		
		    ErrorMessages.Add("E00095", "ShippingProfileId is not provided within Customer Profile.");		
	    }

	    [ClassCleanup]
        public static void TearDownAfterClass()
        {
	    }

        public static String DateFormat = "yyyy-MM-dd'T'HH:mm:ss";

        [TestInitialize]
        public void SetUp()
        {
            //initialize counter
            _counter = _random.Next(1, (int) (Math.Pow(2, 24)));
            _counterStr = GetRandomString("");

            _now = DateTime.UtcNow;
            _nowString = _now.ToString(DateFormat);

            _pastDate = _now.AddMonths(-1);
            _nowDate = _now;
            _futureDate = _now.AddMonths(1);

            CnpMerchantAuthenticationType = new merchantAuthenticationType
                {
                    name = CnpApiLoginIdKey,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = CnpTransactionKey,
                };

            CpMerchantAuthenticationType = new merchantAuthenticationType
                {
                    name = CpApiLoginIdKey,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = CpTransactionKey,
                };

            //		merchantAuthenticationType.setSessionToken(GetRandomString("SessionToken"));
            //		merchantAuthenticationType.setPassword(GetRandomString("Password"));
            //	    merchantAuthenticationType.setMobileDeviceId(GetRandomString("MobileDevice"));

            //	    ImpersonationAuthenticationType impersonationAuthenticationType = new ImpersonationAuthenticationType();
            //	    impersonationAuthenticationType.setPartnerLoginId(CnpApiLoginIdKey);
            //	    impersonationAuthenticationType.setPartnerTransactionKey(CnpTransactionKey);
            //	    merchantAuthenticationType.setImpersonationAuthentication(impersonationAuthenticationType);

            CustomerProfileType = new customerProfileType
                {
                    merchantCustomerId = GetRandomString("Customer"),
                    description = GetRandomString("CustomerDescription"),
                    email = _counterStr + ".customerProfileType@test.anet.net",
                };

            //make sure these elements are initialized by calling get as it uses lazy initialization
            var paymentProfiles = CustomerProfileType.paymentProfiles;
            var addresses = CustomerProfileType.shipToList;

            CreditCardOne = new creditCardType
                {
                    cardNumber = "4111111111111111",
                    expirationDate = "2038-12",
                };
            //		creditCardOne.setCardCode("");

            BankAccountOne = new bankAccountType
                {
                    accountType = bankAccountTypeEnum.savings,
                    routingNumber = "125000000",
                    accountNumber = GetRandomString("A/C#"),
                    nameOnAccount = GetRandomString("A/CName"),
                    echeckType = echeckTypeEnum.WEB,
                    bankName = GetRandomString("Bank"),
                    checkNumber = _counterStr,
                };

            TrackDataOne = new creditCardTrackType
                {
                    ItemElementName = ItemChoiceType1.track1,
                    Item = GetRandomString("Track1"),
                    //trackDataOne.setTrack2(GetRandomString("Track2"));
                };

            EncryptedTrackDataOne = new encryptedTrackDataType
                {
                    FormOfPayment = new KeyBlock(),
                };
            //keyBlock.setValue(value);

            PayPalOne = new payPalType
                {
                    successUrl = GetRandomString("http://success.anet.net"),
                    cancelUrl = GetRandomString("http://cancel.anet.net"),
                    paypalLc = GetRandomString("Lc"),
                    paypalHdrImg = GetRandomString("Hdr"),
                    paypalPayflowcolor = GetRandomString("flowClr"),
                    payerID = GetRandomString("PayerId"),
                };

            PaymentOne = new paymentType
                {
                    Item = CreditCardOne
                };
            //paymentOne.setBankAccount(bankAccountOne);
            //paymentOne.setTrackData(trackDataOne);
            //paymentOne.setEncryptedTrackData(encryptedTrackDataOne);
            //paymentOne.setPayPal( payPalOne);

            //		driversLicenseOne = new DriversLicenseType();
            //		driversLicenseOne.setNumber(GetRandomString("DLNumber"));
            //		driversLicenseOne.setState(GetRandomString("WA"));
            //		driversLicenseOne.setDateOfBirth(nowString);

            CustomerAddressOne = new customerAddressType
                {
                    firstName = GetRandomString("FName"),
                    lastName = GetRandomString("LName"),
                    company = GetRandomString("Company"),
                    address = GetRandomString("StreetAdd"),
                    city = "Bellevue",
                    state = "WA",
                    zip = "98000",
                    country = "USA",
                    phoneNumber = FormatToPhone(_counter),
                    faxNumber = FormatToPhone(_counter + 1),
                };

            CustomerPaymentProfileOne = new customerPaymentProfileType
                {
                    customerType = customerTypeEnum.individual,
                    payment = PaymentOne,
                };
            //	    customerPaymentProfileOne.setBillTo(customerAddressOne);
            //	    customerPaymentProfileOne.setDriversLicense(driversLicenseOne);
            //	    customerPaymentProfileOne.setTaxId(GetRandomString("XX"));


            CustomerOne = new customerType
                {
                    type = customerTypeEnum.individual,
                    id = GetRandomString("Id"),
                    email = _counterStr + ".customerOne@test.anet.net",
                    phoneNumber = FormatToPhone(_counter),
                    faxNumber = FormatToPhone(_counter + 1),
                    driversLicense = DriversLicenseOne,
                    taxId = "911011011",
                };

            CustomerTwo = new customerType();

            var interval = new paymentScheduleTypeInterval
                {
                    length = 1,
                    unit = ARBSubscriptionUnitEnum.months,
                };

            OrderType = new orderType()
                {
                    //TODO ADD VALIDATION ON INVOICE LENGTH
                    invoiceNumber = GetRandomString("Inv:"),
                    description = GetRandomString("Description"),
                };

            NameAndAddressTypeOne = new nameAndAddressType
                {
                    firstName = GetRandomString("FName"),
                    lastName = GetRandomString("LName"),
                    company = GetRandomString("Company"),
                    address = GetRandomString("Address"),
                    city = GetRandomString("City"),
                    state = GetRandomString("State"),
                    zip = "98004",
                    country = "USA",
                };

            NameAndAddressTypeTwo = new nameAndAddressType
                {
                    firstName = GetRandomString("FName"),
                    lastName = GetRandomString("LName"),
                    company = GetRandomString("Company"),
                    address = GetRandomString("Address"),
                    city = GetRandomString("City"),
                    state = GetRandomString("State"),
                    zip = "98004",
                    country = "USA",
                };

            PaymentScheduleTypeOne = new paymentScheduleType
                {
                    interval = interval,
                    startDate = _nowDate,
                    totalOccurrences = 5,
                    trialOccurrences = 0, 
                };

            ArbSubscriptionOne = new ARBSubscriptionType
                {
                    amount = SetValidSubscriptionAmount(_counter),
                    billTo = NameAndAddressTypeOne,
                    customer = CustomerOne,
                    name = GetRandomString("Name"),
                    order = OrderType,
                    payment = PaymentOne,
                    paymentSchedule = PaymentScheduleTypeOne,
                    shipTo = NameAndAddressTypeOne,
                    trialAmount= SetValidSubscriptionAmount(0),
                };

            CustomerDataOne = new customerDataType
                {
                    driversLicense = CustomerOne.driversLicense,
                    email = CustomerOne.email,
                    id = CustomerOne.id,
                    taxId = CustomerOne.taxId,
                    type = CustomerOne.type,
                };
		
	        RefId = _counterStr;
	    }

	    [TestCleanup]
	    public void TearDown() {
	    }

        string GetRandomString(string title) {
		    return String.Format("{0}{1}", title, _counter);
	    }
	
	    public String FormatToPhone(int number) {
		    var formattedNumber = string.Format( "{0:0000000000}", number);
		    return 	formattedNumber.Substring(0, 3)+"-"+
				    formattedNumber.Substring(3, 3)+"-"+
				    formattedNumber.Substring(6, 4);
	    }

        public decimal SetValidTaxAmount(decimal amount)
        {
            return (amount * TaxRate);
	    }

        public decimal SetValidTransactionAmount(int number)
        {
		    return setValidAmount(number, MaxTransactionAmount);
	    }

        public decimal SetValidSubscriptionAmount(int number)
        {
		    return setValidAmount(number, MaxSubscriptionAmount);
	    }
	
	    private decimal setValidAmount(int number, int maxAmount) {
            return new decimal(number > maxAmount ? (number % maxAmount) : number);
	    }

	    static ANetApiResponse _errorResponse;

	    protected ANetApiResponse GetErrorResponse() {
		    return _errorResponse;
	    }

        private const int MaxSubscriptionAmount = 1000; //214747;
        private const int MaxTransactionAmount = 10000; //214747;
        private const decimal TaxRate = 0.10m;

        protected static TS ExecuteTestRequestWithSuccess<TQ, TS, TT>(TQ request, AuthorizeNet.Environment execEnvironment = null)
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : ApiOperationBase<TQ, TS> 
        {
            TS response = ExecuteTestRequest<TQ, TS, TT>( true, request, execEnvironment);
		
		    return response;
	    }

        protected static TS ExecuteTestRequestWithFailure<TQ, TS, TT>(TQ request, AuthorizeNet.Environment execEnvironment = null)
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : ApiOperationBase<TQ, TS> 
        {
            TS response = ExecuteTestRequest<TQ, TS, TT>(false, request, execEnvironment);
		
		    return response;
	    }

        private static TS ExecuteTestRequest<TQ, TS, TT>(bool successExpected, TQ request, AuthorizeNet.Environment execEnvironment = null) 
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : ApiOperationBase<TQ, TS> 
        {

		    LogHelper.debug( Logger, "Created {0} Request: '{1}'", request.GetType(), request);
		
		    TS response = null;
		    TT controller = null;
		    _errorResponse = null;		
		    var controllerClass = typeof (TT);
		    try {
                var parameters = new object[] {request} ;
                var controllerObject = Activator.CreateInstance(controllerClass, parameters);
                if ( controllerObject is TT)
		        {
		            controller = (TT) controllerObject;
		        }
		        if (null != controller)
		        {
		            ANetApiResponse baseResponse = controller.ExecuteWithApiResponse(execEnvironment);
		            LogHelper.info(Logger, "{0} ResultCode: {1}", controllerClass, controller.GetResultCode());
		            LogHelper.info(Logger, "{0} Results:    {1}", controllerClass, controller.GetResults());
		            response = (TS) baseResponse;
		        }
		        else
		        {
                    LogHelper.error(Logger, "Unable to instantiate Controller: '{0}'", controllerClass);
		        }
		    } catch (Exception e) {
			    LogHelper.error(Logger, "Exception : '{0}' during {1}", e.Message, controllerClass);
		    }
		    if ( successExpected)
		    {
			    ProcessFailureResult<TQ, TS, TT>( true, controller, response);		
			    ValidateSuccess<TQ, TS, TT>( controller, response);
		    } else {
			    ValidateFailure<TQ, TS, TT>( controller, response);
		    }
		    if (null == response && null != controller && null != controller.GetErrorResponse())
		    {
			    _errorResponse = controller.GetErrorResponse();
		    }
		
		    return response;
	    }

        protected static void ProcessFailureResult<TQ, TS, TT>(bool fail, TT controller, TS response) 
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : ApiOperationBase<TQ, TS> 
        {
		    //in case there are errors, log the error messages
		    if ( messageTypeEnum.Ok != controller.GetResultCode())
		    {
			    foreach ( var aMessage in controller.GetResults()) {
				    LogHelper.info(Logger, "Controller Messages: '{0}' ", aMessage);
			    }
			    DisplayResponse(response, "Failure Messsages");
			    var errorResponse = controller.GetErrorResponse();
			    DisplayResponse(errorResponse, "Error Response Messages");
			    if ( fail)
			    {
				    Assert.Fail("Request failed.");
			    }
		    }
	    }

	    protected static void ValidateSuccess<TQ, TS, TT>( TT controller, TS response)
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : ApiOperationBase<TQ, TS> 
        {
		    Assert.AreEqual( messageTypeEnum.Ok, controller.GetResultCode());
		    Assert.IsNull(controller.GetErrorResponse());
		    Assert.IsNotNull(response);
		    DisplayResponse( response, "Success Messages");
	    }

	    protected static void ValidateFailure<TQ, TS, TT>( TT controller, TS response)
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : ApiOperationBase<TQ, TS> 
        {
		    Assert.AreEqual( messageTypeEnum.Error, controller.GetResultCode());
		    //TODO Until error response is fixed
		    //Assert.assertNotNull(controller.getErrorResponse());
		    //Assert.assertNull(response);
            ProcessFailureResult<TQ, TS, TT>(false, controller, response);
	    }
	
	    private static void DisplayResponse(ANetApiResponse response, String source) {
		    LogHelper.info(Logger, "Source '{0}' ", source);
		    if (null != response) {
			    var messageType = response.messages;
			    if ( null != messageType) {
				    LogHelper.info(Logger, "MessageCode: '{0}' ", messageType.resultCode.ToString());
				    foreach ( var aMessage in messageType.message) {
					    LogHelper.info(Logger, "Message: '{0}':'{1}' ", aMessage.code, aMessage.text);
				    }
			    }
		    }
	    }

	    protected void ValidateErrorCode(messagesType messagesType, string errorCode)
	    {
		    var firstError = GetFirstErrorMessage( messagesType);
		    if (null != firstError)
		    {
			    Assert.AreEqual( errorCode, firstError.code);
			    if ( ErrorMessages.ContainsKey(errorCode))
			    {
				    string message = ErrorMessages[errorCode];
				    if ( !(string.IsNullOrEmpty(message)))
				    {
					    Assert.AreEqual( message, firstError.text);
				    }
			    }
		    }
	    }
	
	    protected static string GetFirstErrorCode(messagesType messagesType)
	    {
		    var errorMessage = GetFirstErrorMessage( messagesType);
		    return ( (null != errorMessage) ? errorMessage.code : null); 
	    }

	    protected static string GetFirstErrorText(messagesType messagesType)
	    {
		    var errorMessage = GetFirstErrorMessage( messagesType);
		    return ( (null != errorMessage) ? errorMessage.text : null); 
	    }
	
	    protected static messagesTypeMessage GetFirstErrorMessage(messagesType messagesType)
	    {
		    messagesTypeMessage errorMessage = null;
		    if (  null != messagesType.message)
		    {
			    foreach( var aMessage in messagesType.message)
			    {
				    errorMessage = aMessage;
				    break;
			    }
		    }
		
		    return errorMessage;
		
	    }
    }
#pragma warning restore 649
#pragma warning restore 169
// ReSharper restore NotAccessedField.Local
// ReSharper restore FieldCanBeMadeReadOnly.Local
}