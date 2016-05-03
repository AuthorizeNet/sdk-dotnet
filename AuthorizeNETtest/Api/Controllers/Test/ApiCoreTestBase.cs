using AuthorizeNet.Utility;

namespace AuthorizeNet.Api.Controllers.Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using AuthorizeNet.Api.Contracts.V1;
    using AuthorizeNet.Api.Controllers.Bases;
    using AuthorizeNet.Test;
    using AuthorizeNet.Util;
    using NUnit.Framework;
    using NMock;

    // ReSharper disable FieldCanBeMadeReadOnly.Local
    // ReSharper disable NotAccessedField.Local
#pragma warning disable 169
#pragma warning disable 649
    [TestFixture]
    public abstract class ApiCoreTestBase {

	    protected static readonly Log Logger = LogFactory.getLog(typeof(ApiCoreTestBase));
	
	    protected static readonly IDictionary<String, String> ErrorMessages ;

        protected static AuthorizeNet.Environment TestEnvironment = AuthorizeNet.Environment.SANDBOX;
        //protected static AuthorizeNet.Environment TestEnvironment = AuthorizeNet.Environment.HOSTED_VM;
        
	    static Merchant _merchant ;
	    static readonly String ApiLoginIdKey ;
	    static readonly String TransactionKey ;
	    static String _md5HashKey ;
	
	    DateTime _pastDate;
	    DateTime _nowDate;
	    DateTime _futureDate;
	    String _nowString ;
	    DateTime _now = DateTime.UtcNow;

	    protected string RefId ;
	    protected int Counter;
        protected String CounterStr;

        protected merchantAuthenticationType CustomMerchantAuthenticationType;

        protected ARBSubscriptionType ArbSubscriptionOne;

        //protected ARBSubscriptionType ArbSubscriptionTwo;
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

        protected MockFactory MockContext = null;
        private readonly AnetRandom _random = new AnetRandom();
	    static ApiCoreTestBase() {

            //now we support Tls only, and .net defaults to TLS
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            Logger.error(String.Format("Configuration file used: {0}, Exists:{1}", config.FilePath, config.HasFile));

		    //getPropertyFromNames get the value from properties file or environment
		    ApiLoginIdKey = UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.EnvApiLoginid, AuthorizeNet.Util.Constants.PropApiLoginid);
		    TransactionKey = UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.EnvTransactionKey, AuthorizeNet.Util.Constants.PropTransactionKey);
		    _md5HashKey = UnitTestData.GetPropertyFromNames(AuthorizeNet.Util.Constants.EnvMd5Hashkey, AuthorizeNet.Util.Constants.PropMd5Hashkey);

            //require only one cnp or cp merchant keys
            if (null != ApiLoginIdKey && null != TransactionKey)
            {
                Logger.debug("Merchant Login and transaction keys are present.");
            }
            else
		    {
			    throw new ArgumentException(
                    "LoginId and/or TransactionKey have not been set. Merchant keys are required.");
		    }

	        if (null != ApiLoginIdKey && null != TransactionKey)
	        {
	            _merchant = Merchant.CreateMerchant(TestEnvironment, ApiLoginIdKey, TransactionKey);
	        }
            if (null == _merchant)
            {
                Assert.Fail("Merchant logins have been set");
            }

	        ErrorMessages = new Dictionary<string, string>();
	    }

	    [TestFixtureSetUp]
        public static void SetUpBeforeClass()//TestContext context)
        {
            ErrorMessages.Clear();
		    ErrorMessages.Add("E00003", "");   //The message is dynamic based on the xsd violation.
            ErrorMessages.Add("E00007", "User authentication failed due to invalid authentication values.");
		    ErrorMessages.Add("E00027", "");
		    ErrorMessages.Add("E00040", "");
		    ErrorMessages.Add("E00090", "PaymentProfile cannot be sent with payment data." );
		    ErrorMessages.Add("E00091", "PaymentProfileId cannot be sent with payment data.");		
		    ErrorMessages.Add("E00092", "ShippingProfileId cannot be sent with ShipTo data.");		
		    ErrorMessages.Add("E00093", "PaymentProfile cannot be sent with billing data.");		
		    ErrorMessages.Add("E00095", "ShippingProfileId is not provided within Customer Profile.");
        }

	    [TestFixtureTearDown]
        public static void TearDownAfterClass()
        {
	    }

        public static String DateFormat = "yyyy-MM-dd'T'HH:mm:ss";

        [SetUp]
        public void SetUp()
        {
            MockContext = new MockFactory();

            //initialize counter
            Counter = _random.Next(1, (int) (Math.Pow(2, 24)));
            CounterStr = GetRandomString("");

            _now = DateTime.UtcNow;
            _nowString = _now.ToString(DateFormat);

            _pastDate = _now.AddMonths(-1);
            _nowDate = _now;
            _futureDate = _now.AddMonths(1);

            CustomMerchantAuthenticationType = new merchantAuthenticationType
                {
                    name = ApiLoginIdKey,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = TransactionKey,
                };

            //		merchantAuthenticationType.setSessionToken(GetRandomString("SessionToken"));
            //		merchantAuthenticationType.setPass_word(GetRandomString("Pass_word"));
            //	    merchantAuthenticationType.setMobileDeviceId(GetRandomString("MobileDevice"));

            //	    ImpersonationAuthenticationType impersonationAuthenticationType = new ImpersonationAuthenticationType();
            //	    impersonationAuthenticationType.setPartnerLoginId(CnpApiLoginIdKey);
            //	    impersonationAuthenticationType.setPartnerTransactionKey(CnpTransactionKey);
            //	    merchantAuthenticationType.setImpersonationAuthentication(impersonationAuthenticationType);

            CustomerProfileType = new customerProfileType
                {
                    merchantCustomerId = GetRandomString("Customer"),
                    description = GetRandomString("CustomerDescription"),
                    email = CounterStr + ".customerProfileType@test.anet.net",
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
                    checkNumber = CounterStr,
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
                    successUrl = GetRandomString("https://success.anet.net"),
                    cancelUrl = GetRandomString("https://cancel.anet.net"),
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
                    phoneNumber = FormatToPhone(Counter),
                    faxNumber = FormatToPhone(Counter + 1),
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
                    email = CounterStr + ".customerOne@test.anet.net",
                    phoneNumber = FormatToPhone(Counter),
                    faxNumber = FormatToPhone(Counter + 1),
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
                    amount = SetValidSubscriptionAmount(Counter),
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
		
	        RefId = CounterStr;
	    }

	    [TearDown]
	    public void TearDown() {
            MockContext.VerifyAllExpectationsHaveBeenMet();
	    }

        string GetRandomString(string title) {
		    return String.Format("{0}{1}", title, Counter);
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
            //updated to return a value with dollars and cents and not just whole dollars.
		    return (Decimal)setValidAmount(number, MaxTransactionAmount/100);
	    }

        public decimal SetValidSubscriptionAmount(int number)
        {
		    return setValidAmount(number, MaxSubscriptionAmount);
	    }
	
	    private decimal setValidAmount(int number, int maxAmount)
        {
            //Test that result is not larger than the specified max value
            number = (number > maxAmount) ? (number % maxAmount) : number;

            Decimal dollarsAndCents = (decimal)number / 100;

            //Test that result is not less than the global Min Value
            return dollarsAndCents = (dollarsAndCents < MinAmount) ? (MinAmount + dollarsAndCents) : dollarsAndCents;
	    }

	    static ANetApiResponse _errorResponse;

	    protected ANetApiResponse GetErrorResponse() {
		    return _errorResponse;
	    }

        private const int MaxSubscriptionAmount = 1000; //214747;
        private const int MaxTransactionAmount = 10000; //214747;
        private const int MinAmount = 1;
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
	
	    protected static void DisplayResponse(ANetApiResponse response, String source) {
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

        protected void SetMockControllerExpectations<TQ, TS, TT> (
			IApiOperation<TQ, TS> mockController,
			TQ mockRequest,
			TS mockResponse,
			ANetApiResponse errorResponse, 
			List<String> results,
            messageTypeEnum messageType)
            where TQ : ANetApiRequest
            where TS : ANetApiResponse
            where TT : IApiOperation<TQ, TS>
        {
		    var mockEnvironment = AuthorizeNet.Environment.CUSTOM;

            //using (MockContext.Unordered())
            {
                //Expect.On(mockController).Any.Method(i => i.Execute(mockEnvironment));
                Expect.On(mockController).Any.Method(i => i.Execute(mockEnvironment)).With(mockEnvironment);
                Expect.On(mockController).Any.Method(i => i.GetApiResponse()).WillReturn(mockResponse);
                //Expect.On(mockController).Between(0, 10).Method(i => i.ExecuteWithApiResponse(mockEnvironment)).WillReturn(mockResponse);
                Expect.On(mockController).Any.Method(i => i.ExecuteWithApiResponse(mockEnvironment)).With(mockEnvironment).WillReturn(mockResponse);
                Expect.On(mockController).Any.Method(i => i.GetResults()).WillReturn(results);
                Expect.On(mockController).Any.Method(i => i.GetResultCode()).WillReturn(messageType);
                Expect.On(mockController).Any.Method(i => i.GetErrorResponse()).WillReturn(errorResponse);
            }

            if (null != mockRequest && null != mockResponse)
            {
                mockResponse.refId = mockRequest.refId;
            }
            var realController = Activator.CreateInstance(typeof(TT), mockRequest);
            Assert.IsNotNull(realController);

		    LogHelper.info(Logger, "Request: {0}", mockRequest);
		    ShowProperties(mockRequest);
		    LogHelper.info(Logger, "Response: {0}", mockResponse);
		    ShowProperties(mockResponse);
	    }

	    protected Mock<IApiOperation<TQ,TS>> GetMockController<TQ, TS>()  where TQ : ANetApiRequest where TS : ANetApiResponse
	    {
            return MockContext.CreateMock<IApiOperation<TQ, TS>>();
	    }

	    public static void ShowProperties(Object bean) {  
		    if ( null == bean) { return; }

		    try
		    {
                var fieldInfos = bean.GetType().GetFields();//BindingFlags.GetProperty);
                foreach (var pd in fieldInfos)
		        {
		            var name = pd.Name;
		            var type = pd.FieldType;
 
		            if (!("class".Equals(name)) &&
                        !(bean.ToString().Equals(name)))
		            {
		                try
		                {
                            var value = pd.GetValue(bean);
                            //var value = bean.GetType().GetField(name).GetValue(bean);
                            LogHelper.info(Logger, "Field Type: '{0}', Name:'{1}', Value:'{2}'", type, name, value);
                            ProcessCollections(type, name, value);
                            //process compositions of custom classes
                            //if (null != value && 0 <= type.ToString().IndexOf("AuthorizeNet.", System.StringComparison.Ordinal))

		                    var whiteListAssembly = (type.Assembly.FullName.IndexOf("AuthorizeNET", StringComparison.Ordinal) >= 0 );

                            if (null != value &&
                                whiteListAssembly &&
                                !(value is Enum) &&
                                !value.GetType().IsPrimitive &&
                                !(value is string))
                            {
                                ShowProperties(value);
                            }

                            var propertyChanged = bean as INotifyPropertyChanged;
                            if (propertyChanged != null)
                            {
                                var changed = false;
                                propertyChanged.PropertyChanged += (s, e) => { if (e.PropertyName == name) changed = true; };
                            }
		                } catch (Exception e) {
                            LogHelper.info(Logger, "Exception during getting Field value: Type: '{0}', Name:'{1}', Message: {2}, StackTrace: {3}", type, name, e.Message, e.StackTrace);
		                }
		            }
		        }
            }
            catch (Exception e)
            {
			    LogHelper.info(Logger, "Exception during navigating properties: Message: {0}, StackTrace: {1}", e.Message, e.StackTrace);
		    }  
	    }

        public static void ProcessCollections( Type type, String name, Object value)
        {
            if (null == type) return;
            var values = value as IEnumerable;
            if (values != null &&
                !(value is string))
            {
                LogHelper.info(Logger, "Iterating on Collection: '{0}'", name);
                foreach (var aValue in values)
                {
                    ShowProperties(aValue);
                }
            }
        }

    }
#pragma warning restore 649
#pragma warning restore 169
// ReSharper restore NotAccessedField.Local
// ReSharper restore FieldCanBeMadeReadOnly.Local
}