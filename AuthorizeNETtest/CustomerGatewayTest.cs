using AuthorizeNet;
using AuthorizeNet.APICore;
using NUnit.Framework;
using System;
using System.IO;
using AuthorizeNet.Utility;

namespace AuthorizeNETtest
{
    using System.Text;

    /// <summary>
    /// This is a test class for CustomerGatewayTest and is intended to contain all CustomerGateway Unit Tests
    /// </summary>
    [TestFixture]
    public class CustomerGatewayTest : BaseTest
    {
        private CustomerGateway _target;

        [TestFixtureSetUp]
        public void CreateSubscription()
        {
            //check ApiLoginid / TransactionKey
            var sError = CheckApiLoginTransactionKey();
            Assert.IsTrue(sError == "", sError);

            _target = new CustomerGateway(ApiLogin, TransactionKey);
        }

        /// <summary>
        /// CreateCustomer - success
        /// </summary>
        [Test]
        public void CreateCustomerTest()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>24231938</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";

            var actual = CreateCustomer(email, description);

            Assert.IsNotNull(actual);
            Assert.AreEqual(email, actual.Email);
            Assert.AreEqual(description, actual.Description);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ID));
            Assert.Greater(actual.ID.Trim().Length,  0);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ProfileID));
            Assert.Greater(actual.ProfileID.Trim().Length,  0);
        }

        private Customer CreateCustomer(string email, string description, string custId = "")
        {
            Customer customer = null;
            try
            {
                customer = _target.CreateCustomer(email, description, custId);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.CreateCustomer() failed: " + e.Message);
            }
	    Assert.IsNotNull(customer);
            return customer;
        }

        /// <summary>
        /// CreateCustomer - success
        /// </summary>
        [Test]
        public void CreateCustomerTest_CustomerID()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>27092230</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "CreateCustomerTest Success";
            const string customerId = "CustomerId 1234";

            var actual = CreateCustomer(email, description, customerId);

            Assert.IsNotNull(actual);
            Assert.AreEqual(email, actual.Email);
            Assert.AreEqual(description, actual.Description);
            Assert.AreEqual(customerId, actual.ID);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ProfileID));
            Assert.Greater(actual.ProfileID.Trim().Length,  0);
        }

        /// <summary>
        /// UpdateCustomer - successful
        /// </summary>
        [Test]
        public void UpdateCustomerTest()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            var newEmail = Path.GetRandomFileName() + "@visa.com";
            const string newDescription = "Update the Customer";

            var updateCustomer = new Customer
                {
                    ID = "",
                    ProfileID = customer.ProfileID,
                    Email = newEmail,
                    Description = newDescription
                };
	    
            var actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.UpdateCustomer(updateCustomer);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.UpdateCustomer() failed: " + e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// AddCreditCard - success
        /// </summary>
        [Test]
        public void AddCreditCardTest()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            const string cardNumber = "4111111111111111";
            const int expirationMonth = 1;
            const int expirationYear = 2030;

            var custPaymentProfileId = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                custPaymentProfileId = _target.AddCreditCard(customer.ProfileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddCreditCard() failed: " + e.Message);
            }

            Assert.IsFalse(string.IsNullOrEmpty(custPaymentProfileId));
            Assert.Greater(custPaymentProfileId.Trim().Length, 0);
            Assert.Greater(long.Parse(custPaymentProfileId), 0);
        }
        
        /// <summary>
        /// Reproducing the issue number 97 reported in github
        /// @Zalak
        /// </summary>
        [Test]
        public void ValidateProfileTest ()
        {
            var target = _target;
            try
            {
                const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
                LocalRequestObject.ResponseString = responseString;

                _target = new CustomerGateway(ApiLogin, TransactionKey, ServiceMode.Test);
                var email = Path.GetRandomFileName() + "@visa.com";
                const string description = "Create a new customer";
                var customer = CreateCustomer(email, description);

                const string cardNumber = "4111111111111111";
                const int expirationMonth = 1;
                const int expirationYear = 2030;
                
                var custPaymentProfileId = "";
                // if choose "USELOCAL", the test should pass with no exception
                // Otherwise, the test might fail for error, i.e. duplicated request.
                try
                {
                    custPaymentProfileId = target.AddCreditCard(customer.ProfileID, cardNumber, expirationMonth, expirationYear);
                  //   _target.ValidateProfile(customer.ProfileID, custPaymentProfileId);
                    // _target.ValidateProfile(customer.ProfileID, custPaymentProfileId, ValidationMode.None);
                   _target.ValidateProfile(customer.ProfileID, custPaymentProfileId, ValidationMode.TestMode);
                  //  _target.ValidateProfile(customer.ProfileID, custPaymentProfileId, ValidationMode.LiveMode);
                }
                catch (Exception e)
                {
                    Console.WriteLine("CustomerGateway.AddCreditCard() failed: " + e.Message);
                }

                Assert.IsFalse(string.IsNullOrEmpty(custPaymentProfileId));
                Assert.Greater(custPaymentProfileId.Trim().Length, 0);
                Assert.Greater(long.Parse(custPaymentProfileId), 0);
            }
            finally
            {
                _target = target;
            }
        }

        /// <summary>
        /// AddCreditCard set validationMode - success
        /// </summary>
        [Test]
        public void AddCreditCardTest_ValidationMode()
        {
            var target = _target;
            try
            {
                const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
                LocalRequestObject.ResponseString = responseString;

                _target = new CustomerGateway(ApiLogin, TransactionKey, ServiceMode.Test);

                var email = Path.GetRandomFileName() + "@visa.com";
                const string description = "Create a new customer";
                var customer = CreateCustomer(email, description);

                const string cardNumber = "4111111111111111";
                const int expirationMonth = 1;
                const int expirationYear = 2030;

                var custPaymentProfileId = "";

                // if choose "USELOCAL", the test should pass with no exception
                // Otherwise, the test might fail for error, i.e. duplicated request.
                try
                {
                    custPaymentProfileId = target.AddCreditCard(customer.ProfileID, cardNumber, expirationMonth, expirationYear);
                }
                catch (Exception e)
                {
                    Console.WriteLine("CustomerGateway.AddCreditCard() failed: " + e.Message);
                }

                Assert.IsFalse(string.IsNullOrEmpty(custPaymentProfileId));
                Assert.Greater(custPaymentProfileId.Trim().Length, 0);
                Assert.Greater(long.Parse(custPaymentProfileId), 0);
            }
            finally
            {
                _target = target;
            }
        }

        /// <summary>
        /// AddECheckBankAccount required data only to success- success
        /// </summary>
        [Test]
        public void AddECheckBankAccountTest_Required()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>24287597</customerPaymentProfileId><validationDirectResponse>1,1,1,(TESTMODE) This transaction has been approved.,000000,P,0,none,Test transaction for ValidateCustomerPaymentProfile.,1.00,ECHECK,auth_only,none,,,,,,,,,,,email@example.com,,,,,,,,,0.00,0.00,0.00,FALSE,none,ACD21540D94325D06FDC81558F3196AD,,,,,,,,,,,,,XXXX3458,Bank Account,,,,,,,,,,,,,,,,</validationDirectResponse></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            var custPaymentProfileId = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                custPaymentProfileId = _target.AddECheckBankAccount(customer.ProfileID, BankAccountType.Checking, "125000024", "123458", "Sue Zhu");
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }

            Assert.IsFalse(string.IsNullOrEmpty(custPaymentProfileId));
            Assert.Greater(long.Parse(custPaymentProfileId), 0);
        }

        /// <summary>
        /// AddECheckBankAccount all data success- success
        /// </summary>
        [Test]
        public void AddECheckBankAccountTest()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>24282439</customerPaymentProfileId><validationDirectResponse>1,1,1,(TESTMODE) This transaction has been approved.,000000,P,0,none,Test transaction for ValidateCustomerPaymentProfile.,1.00,ECHECK,auth_only,none,,,,,,,,,,,email@example.com,,,,,,,,,0.00,0.00,0.00,FALSE,none,ACD21540D94325D06FDC81558F3196AD,,,,,,,,,,,,,XXXX4587,Bank Account,,,,,,,,,,,,,,,,</validationDirectResponse></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            var custPaymentProfileId = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                custPaymentProfileId = _target.AddECheckBankAccount(customer.ProfileID, BankAccountType.Savings, "125000024", "1234588", "Sue Zhu", "Bank of Seattle", EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }

            Assert.IsFalse(string.IsNullOrEmpty(custPaymentProfileId));
            Assert.Greater(long.Parse(custPaymentProfileId), 0);
        }

        /// <summary>
        /// GetCustomer - success
        /// </summary>
        [Test]
        public void GetCustomerTest()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><profile><merchantCustomerId /><description>UpdateCustomerTest Success</description><email>suzhu@visa.com</email><customerProfileId>24231938</customerProfileId><paymentProfiles><customerType>individual</customerType><customerPaymentProfileId>22219473</customerPaymentProfileId><payment><creditCard><cardNumber>XXXX1111</cardNumber><expirationDate>XXXX</expirationDate></creditCard></payment></paymentProfiles></profile></getCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer0 = CreateCustomer(email, description);

            const string expectedCardExpiration = "XXXX";
            const string expectedCardNumber = "XXXX1111";
            string expectedCustPaymentProfileId = null;
            try
            {
                expectedCustPaymentProfileId = _target.AddCreditCard(customer0.ProfileID, "4111111111111111", 1, 2030);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }
	    Assert.IsNotNull(expectedCustPaymentProfileId);

            Customer customer1 = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                customer1 = _target.GetCustomer(customer0.ProfileID);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.GetCustomer() failed: " + e.Message);
            }

            Assert.IsNotNull(customer1);
            Assert.AreEqual(customer0.ProfileID, customer1.ProfileID);
            Assert.AreEqual(email, customer1.Email);
            Assert.AreEqual(description, customer1.Description);
            Assert.AreEqual(1, customer1.PaymentProfiles.Count);
            Assert.AreEqual(expectedCardExpiration, customer1.PaymentProfiles[0].CardExpiration);
            Assert.AreEqual(expectedCardNumber, customer1.PaymentProfiles[0].CardNumber);
            Assert.AreEqual(expectedCustPaymentProfileId, customer1.PaymentProfiles[0].ProfileID);
        }

        /// <summary>
        /// GetCustomer eCheck Bank Account - success
        /// </summary>
        [Test]
        public void GetCustomerTest_eCheck()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><profile><description>UpdateCustomerTest Success</description><email>suzhu2@visa.com</email><customerProfileId>24236276</customerProfileId><paymentProfiles><customerPaymentProfileId>24287458</customerPaymentProfileId><payment><bankAccount><accountType>checking</accountType><routingNumber>XXXX0024</routingNumber><accountNumber>XXXX3456</accountNumber><nameOnAccount>Sue Zhu</nameOnAccount></bankAccount></payment></paymentProfiles></profile></getCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer0 = CreateCustomer(email, description);

            const string expectedRoutingNumber = "XXXX0024";
            const string expectedAccountNumber = "XXXX4588";
            try
            {
                _target.AddECheckBankAccount(customer0.ProfileID, BankAccountType.Checking, "125000024", "1234588", "Sue Zhu", "Bank of Seattle", EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }

            Customer customer1 = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                customer1 = _target.GetCustomer(customer0.ProfileID);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.GetCustomer() failed: " + e.Message);
            }

            Assert.IsNotNull(customer1);
            Assert.AreEqual(customer0.ProfileID, customer1.ProfileID);
            Assert.AreEqual(email, customer1.Email);
            Assert.AreEqual(description, customer1.Description);
            Assert.AreEqual(1, customer1.PaymentProfiles.Count);
            Assert.AreEqual(expectedAccountNumber, customer1.PaymentProfiles[0].eCheckBankAccount.accountNumber);
            Assert.AreEqual(expectedRoutingNumber, customer1.PaymentProfiles[0].eCheckBankAccount.routingNumber);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// Minimum parameters to ensure a successful response
        /// </summary>
        [Test]
        public void UpdatePaymentProfileMinTest()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            const string cardNumber = "4111111111111111";
            const int expirationMonth = 1;
            const int expirationYear = 2030;

            var custPaymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID, cardNumber, expirationMonth, expirationYear);

            var profile = new PaymentProfile(new customerPaymentProfileMaskedType())
                {
                    ProfileID = custPaymentProfileId,
                    CardNumber = "4111111111111112",
                    CardExpiration = "2029-02"
                };

            var actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.UpdatePaymentProfile(customer.ProfileID, profile);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.UpdatePaymentProfile() failed: " + e.Message);
            }

            Assert.IsTrue(actual);
        }

	private string CreateCustomerPaymentProfile(string custProfileId, string cardNumber, int expirationMonth, int expirationYear)
	{
            string custPaymentProfileId = null;
            try
            {
                custPaymentProfileId = _target.AddCreditCard(custProfileId, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddCreditCard() failed: " + e.Message);
            }
	    Assert.IsNotNull(custPaymentProfileId);
            return custPaymentProfileId;
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// no mask and with billing
        /// </summary>
        [Test]
        public void UpdatePaymentProfileTest_NotMask()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            string custPaymentProfileId = null;
            try
            {
                custPaymentProfileId = _target.AddCreditCard(customer.ProfileID, "4111111111111111", 1, 2030);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }
            Assert.IsNotNull(custPaymentProfileId);

            var profile = new PaymentProfile(new customerPaymentProfileMaskedType())
                {
                    ProfileID = custPaymentProfileId,
                    CardNumber = "4111111111111112",
                    CardExpiration = "2016-03",
                    BillingAddress = new Address
                        {
                            First = "Sue",
                            Last = "Zhu",
                            Company = "Visa",
                            Street = "123 Elm Street",
                            City = "Bellevue",
                            State = "WA",
                            Country = "US",
                            Zip = "98006"
                        }
                };

            var actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.UpdatePaymentProfile(customer.ProfileID, profile);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.UpdatePaymentProfile() failed: " + e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// with mask
        /// </summary>
        [Test]
        public void UpdatePaymentProfileTest_Mask()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            string custPaymentProfileId = null;
            try
            {
                custPaymentProfileId = _target.AddCreditCard(customer.ProfileID, "4111111111111111", 1, 2030);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }
            Assert.IsNotNull(custPaymentProfileId);

            var profile = new PaymentProfile(new customerPaymentProfileMaskedType())
                {
                    ProfileID = custPaymentProfileId,
                    CardNumber = "XXXX1111",
                    CardExpiration = "XXXX",
                    BillingAddress = new Address
                        {
                            First = "Sue",
                            Last = "Zhu",
                            Company = "Visa",
                            Street = "123 Elm Street",
                            City = "Bellevue",
                            State = "WA",
                            Country = "US",
                            Zip = "98006"
                        }
                };

            var actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.UpdatePaymentProfile(customer.ProfileID, profile);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.UpdatePaymentProfile() failed: " + e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile eCheck - success
        /// with mask
        /// </summary>
        [Test]
        public void UpdatePaymentProfileTest_eCheckMask()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            string custPaymentProfileId = null;
            try
            {
                custPaymentProfileId = _target.AddECheckBankAccount(customer.ProfileID, BankAccountType.Checking, "125000024", "1234588", "Sue Zhu", "Bank of Seattle", EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }
            Assert.IsNotNull(custPaymentProfileId);
	    
            var profile = new PaymentProfile(new customerPaymentProfileMaskedType())
                {
                    ProfileID = custPaymentProfileId,
                    eCheckBankAccount = new BankAccount
                        {
                            routingNumber = "XXXX0024",
                            accountNumber = "XXXX4588",
                            nameOnAccount = "Sue Zhu"
                        },
                    BillingAddress = new Address
                        {
                            First = "Sue",
                            Last = "Zhu",
                            Company = "Visa",
                            Street = "123 Elm Street",
                            City = "Bellevue",
                            State = "WA",
                            Country = "US",
                            Zip = "98006"
                        }
                };

            var actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.UpdatePaymentProfile(customer.ProfileID, profile);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.UpdatePaymentProfile() failed: " + e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile eCheck - success
        /// with mask
        /// </summary>
        [Test]
        public void UpdatePaymentProfileTest_eCheck()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);

            string custPaymentProfileId = null;
            try
            {
                custPaymentProfileId = _target.AddECheckBankAccount(customer.ProfileID, BankAccountType.Checking, "125000024", "1234588", "Sue Zhu", "Bank of Seattle", EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AddECheckBankAccount() failed: " + e.Message);
            }
            Assert.IsNotNull(custPaymentProfileId);
	    
            var paymentProfile = new PaymentProfile(new customerPaymentProfileMaskedType())
                {
                    ProfileID = custPaymentProfileId,
                    eCheckBankAccount = new BankAccount
                        {
                            routingNumber = "125000025",
                            accountNumber = "1234567",
                            nameOnAccount = "Sue Zhu"
                        }
                };

            var actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.UpdatePaymentProfile(customer.ProfileID, paymentProfile);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.UpdatePaymentProfile() failed: " + e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction - Approved
        /// </summary>
        [Test]
        public void AuthorizeAndCaptureTest()
        {
            var txnAmount = getValidAmount();

            var responseString = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2C99N3,Y,2207640586,,,{0},CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C40BBCC10984A7A95471323B34FD4FFB,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>", txnAmount);
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@viscom";
            const string description = "CreateCustomerTest Success";
            var customer = CreateCustomer(email, description);

            const string expectedCardNumber = "XXXX1111";
            var paymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID,  "4111111111111111", 1, 2030);

            var order = new Order(customer.ProfileID, paymentProfileId, "") {Amount = txnAmount};

            IGatewayResponse actual = null;
            
            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AuthorizeAndCapture() failed: " + e.Message);
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(txnAmount, actual.Amount);
            Assert.AreEqual(true, actual.Approved);
            Assert.AreEqual(expectedCardNumber, actual.CardNumber);
            Assert.AreEqual("This transaction has been approved.", actual.Message);
            Assert.AreEqual("1", actual.ResponseCode);
	    
            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.Greater(long.Parse(actual.TransactionID), 0);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction with Invoice, Description and PONumber - Approved
        /// </summary>
        [Test]
        public void AuthorizeAndCaptureTest_InvoiceDescriptionPONumber()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,Q5G0UI,Y,2207641147,Invoice#123,Testing InvoiceDescriptionPONumber,25.10,CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,PO23456,BEEEB7C9F2F22B9955338A7E19427369,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@viscom";
            const string description = "CreateCustomerTest Success";
            var customer = CreateCustomer(email, description);

            const string expectedCardNumber = "XXXX1111";
            var paymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID,  "4111111111111111", 1, 2030);

            var txnAmount = getValidAmount();
            const string invoiceNumber = "Invoice#123";

            var order = new Order(customer.ProfileID, paymentProfileId, "")
                {
                    Amount = txnAmount,
                    InvoiceNumber = invoiceNumber,
                    Description = "Testing InvoiceDescriptionPONumber",
                    PONumber = "PO23456"
                };

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AuthorizeAndCapture() failed: " + e.Message);
            }
            Assert.IsNotNull(actual);

            Assert.AreEqual(txnAmount, actual.Amount);
            Assert.AreEqual(true, actual.Approved);
            Assert.AreEqual(expectedCardNumber, actual.CardNumber);
            Assert.AreEqual("This transaction has been approved.", actual.Message);
            Assert.AreEqual("1", actual.ResponseCode);
            Assert.AreEqual(invoiceNumber, actual.InvoiceNumber);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction - Approved
        /// </summary>
        [Test]
        public void AuthorizeAndCaptureTest_ExtraOptions()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,0PI2II,Y,2210620905,,,25.10,CC,auth_capture,Testing Extra Option,Sue,Zhu,Visa,123 Elm Street,Bellevue,WA,98006,US,,,suzhu@visa.com,,,,,,,,,,,,,,070CC74A6FDD5EA7951444C547FE7829,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@viscom";
            const string description = "CreateCustomerTest Success";
            var customer = CreateCustomer(email, description);

            const string expectedCardNumber = "XXXX1111";
            var paymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID,  "4111111111111111", 1, 2030);

            var txnAmount = getValidAmount();
	    
            var order = new Order(customer.ProfileID, paymentProfileId, "")
                {
                    Amount = txnAmount,
                    ExtraOptions = "x_customer_ip=100.0.0.1&x_cust_id=Testing Extra Options"
                };

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.AuthorizeAndCapture() failed: " + e.Message);
            }
            Assert.IsNotNull(actual);
	    
            Assert.AreEqual(txnAmount, actual.Amount);
            Assert.AreEqual(true, actual.Approved);
            Assert.AreEqual(expectedCardNumber, actual.CardNumber);
            Assert.AreEqual("This transaction has been approved.", actual.Message);
            Assert.AreEqual("1", actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// Capture Transaction - Approved
        /// </summary>
        [Test]
        public void SendTest_Capture_Approved()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,P,2207702175,,,25.12,CC,capture_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,5BB96CB66C1E0BCE123915E970D70166,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            Customer customer;
            string paymentProfileId;
            const string expectedCardNumber = "XXXX1111";

            var txnAmount = getValidAmount();
            var authCode = SendAuthOnly(txnAmount + 1, false, out customer, out paymentProfileId);
            Assert.Greater(authCode.Trim().Length, 0);

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.Capture(customer.ProfileID, paymentProfileId, "", txnAmount, authCode);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.Capture() failed: " + e.Message);
            }
            Assert.IsNotNull(actual);

            Assert.AreEqual(txnAmount, actual.Amount);
            Assert.AreEqual(true, actual.Approved);
            Assert.AreEqual(expectedCardNumber, actual.CardNumber);
            Assert.AreEqual("This transaction has been approved.", actual.Message);
            Assert.AreEqual("1", actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.Greater(long.Parse(actual.TransactionID), 0);
        }

        /// <summary>
        /// PriorAuthCapture Transaction - Approved
        /// </summary>
        [Test]
        public void SendTest_PriorAuthCapture_Approved()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,MFSOM8,P,2207702374,,,25.13,CC,prior_auth_capture,,,,,,,,,,,,,,,,,,,,,,,,,,E0DF3A88533C1F9CBE3B55159C514513,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            //setup
            var txnAmount = getValidAmount();
            var transId = SendAuthOnly(txnAmount + 1, true);
            Assert.IsTrue(transId.Trim().Length > 0);
            Assert.Greater(long.Parse(transId), 0);

            const string expectedCardNumber = "XXXX1111";

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.PriorAuthCapture(transId, txnAmount);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.PriorAuthCapture() failed: " + e.Message);
            }
            Assert.IsNotNull(actual);

            Assert.AreEqual(txnAmount, actual.Amount);
            Assert.AreEqual(true, actual.Approved);
            Assert.AreEqual(expectedCardNumber, actual.CardNumber);
            Assert.AreEqual("This transaction has been approved.", actual.Message);
            Assert.AreEqual("1", actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        private string SendAuthOnly(decimal amount, bool returnTransId, out Customer customer, out string paymentProfileId)
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,Y,2207702136,,,11.21,CC,auth_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C8E9860C9B9DF58A73FFD9D7A8BFB82F,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            customer = CreateCustomer(email, description);
	    
            paymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID,  "4111111111111111", 1, 2030);

            IGatewayResponse response = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                response = _target.Authorize(customer.ProfileID, paymentProfileId, amount);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.Authorize() failed: " + e.Message);
            }

            if (response != null && response.Approved)
            {
                return returnTransId ? response.TransactionID : response.AuthorizationCode;
            }
            else
            {
                return "";
            }
        }

        private string SendAuthOnly(decimal amount, bool returnTransId)
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,Y,2207702136,,,11.21,CC,auth_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C8E9860C9B9DF58A73FFD9D7A8BFB82F,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);
	    
            var paymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID,  "4111111111111111", 1, 2030);

            IGatewayResponse response = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                response = _target.Authorize(customer.ProfileID, paymentProfileId, amount);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.Authorize() failed: " + e.Message);
            }

            if (response != null && response.Approved)
            {
                return returnTransId ? response.TransactionID : response.AuthorizationCode;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// PriorAuthCapture Transaction - Approved
        /// </summary>
        [Test]
        public void SendTest_AuthOnly_ExtraOptions()
        {
            const string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,E4CGH9,Y,2210636215,,,25.15,CC,auth_only,Testing Extra Option,Sue,Zhu,Visa,123 Elm Street,Bellevue,WA,98006,US,,,suzhu@visa.com,,,,,,,,,,,,,,3445C1C7DFFB2F32357A316DE94C13D1,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            const string expectedCardNumber = "XXXX1111";

            var email = Path.GetRandomFileName() + "@visa.com";
            const string description = "Create a new customer";
            var customer = CreateCustomer(email, description);
	    
            var paymentProfileId = CreateCustomerPaymentProfile(customer.ProfileID,  "4111111111111111", 1, 2030);

            var txnAmount = getValidAmount();

            var order = new Order(customer.ProfileID, paymentProfileId, "")
                {
                    Amount = txnAmount,
                    ExtraOptions = "x_customer_ip=100.0.0.1&x_cust_id=Testing Extra Options"
                };

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = _target.Authorize(order);
            }
            catch (Exception e)
            {
                Console.WriteLine("CustomerGateway.Authorize() failed: " + e.Message);
            }
            Assert.IsNotNull(actual);

            Assert.AreEqual(txnAmount, actual.Amount);
            Assert.AreEqual(true, actual.Approved);
            Assert.AreEqual(expectedCardNumber, actual.CardNumber);
            Assert.AreEqual("This transaction has been approved.", actual.Message);
            Assert.AreEqual("1", actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        [Test]
        public void TestCheckForErrorscreateCustomerProfileTransactionResponse()
        {
            const string profileId = "24231938";
            const string paymentProfileId = "22219473";
            var gateway = new CustomerGateway(ApiLogin, TransactionKey);
            var order = new Order(profileId, paymentProfileId, "") {Amount = (decimal) 25.10};
            var response = gateway.AuthorizeAndCapture(order);
            Assert.IsNotNull(response);
        }

        [Test]
        public void TestSdkUpgradeCustomerOrder()
        {
            var random = new AnetRandom();
            var counter = random.Next(1, (int)(Math.Pow(2, 24)));
            const int maxAmount = 10000;// 214747;
            var amount = new decimal(counter > maxAmount ? (counter % maxAmount) : counter);
            var email = string.Format("user.{0}@authorize.net", counter);
            var description = string.Format("Description for Customer: {0}", counter);
            var merchantCustomerId = string.Format("CustomerId: {0}", counter);
            const string cardNumber = "4111111111111111";
            const string  cvv = "";
            var address = new Address
                {
                    First = string.Format("FName:{0}", counter),
                    Last = string.Format("LName:{0}", counter),
                    Company = "Visa",
                    Street = "123 Elm Street",
                    City = "Bellevue",
                    State = "WA",
                    Country = "US",
                    Zip = "98006"
                };

            //Save the customer first
            var gw = new CustomerGateway(ApiLogin, TransactionKey);

            var customer = gw.CreateCustomer(email, description, merchantCustomerId);
            var creditCardToken = gw.AddCreditCard(customer.ProfileID, cardNumber, DateTime.UtcNow.Month, DateTime.UtcNow.AddYears(1).Year, cvv, address);

            //Create order
            var order = new Order(customer.ProfileID, creditCardToken, "")
                {
                    Amount = amount,
                    CardCode = cvv,
                    ExtraOptions = "x_duplicate_window=0"
                };

            var result = (GatewayResponse)gw.AuthorizeAndCapture(order);
            Assert.IsNotNull(result, "GateWay response for Order AuthCapture is null");

            var buffer = new StringBuilder();

            buffer.Append( "IGateWayResponse->");
            buffer.AppendFormat( "  SplitTenderId:{0}", result.SplitTenderId);
            buffer.AppendFormat( ", MD5Hash:{0}", result.MD5Hash);
            buffer.AppendFormat( ", CCVResponse:{0}", result.CCVResponse);
            buffer.AppendFormat( ", Code:{0}", result.Code);
            buffer.AppendFormat( ", TransactionType:{0}", result.TransactionType);
            buffer.AppendFormat( ", AuthorizationCode:{0}", result.AuthorizationCode);
            buffer.AppendFormat( ", Method:{0}", result.Method);
            buffer.AppendFormat( ", Amount:{0}", result.Amount);
            buffer.AppendFormat( ", Tax:{0}", result.Tax);
            buffer.AppendFormat( ", TransactionID:{0}", result.TransactionID);
            buffer.AppendFormat( ", InvoiceNumber:{0}", result.InvoiceNumber);
            buffer.AppendFormat( ", Description:{0}", result.Description);
            buffer.AppendFormat( ", ResponseCode:{0}", result.ResponseCode);
            buffer.AppendFormat( ", CardNumber:{0}", result.CardNumber);
            buffer.AppendFormat( ", CardType:{0}", result.CardType);
            buffer.AppendFormat( ", CAVResponse:{0}", result.CAVResponse);
            buffer.AppendFormat( ", AVSResponse:{0}", result.AVSResponse);
            buffer.AppendFormat( ", SubCode:{0}", result.SubCode);
            buffer.AppendFormat( ", Message:{0}", result.Message);
            buffer.AppendFormat( ", Approved:{0}", result.Approved);
            buffer.AppendFormat( ", Declined:{0}", result.Declined);
            buffer.AppendFormat( ", Error:{0}", result.Error);
            buffer.AppendFormat( ", HeldForReview:{0}", result.HeldForReview);
            buffer.AppendFormat( ", FirstName:{0}", result.FirstName);
            buffer.AppendFormat( ", LastName:{0}", result.LastName);
            buffer.AppendFormat( ", Email:{0}", result.Email);
            buffer.AppendFormat( ", Company:{0}", result.Company);
            buffer.AppendFormat( ", Address:{0}", result.Address);
            buffer.AppendFormat( ", City:{0}", result.City);
            buffer.AppendFormat( ", State:{0}", result.State);
            buffer.AppendFormat( ", ZipCode:{0}", result.ZipCode);
            buffer.AppendFormat( ", Country:{0}", result.Country);
            buffer.AppendFormat( ", ShipFirstName:{0}", result.ShipFirstName);
            buffer.AppendFormat( ", ShipLastName:{0}", result.ShipLastName);
            buffer.AppendFormat( ", ShipCompany:{0}", result.ShipCompany);
            buffer.AppendFormat( ", ShipAddress:{0}", result.ShipAddress);
            buffer.AppendFormat( ", ShipCity:{0}", result.ShipCity);
            buffer.AppendFormat( ", ShipState:{0}", result.ShipState);
            buffer.AppendFormat( ", ShipZipCode:{0}", result.ShipZipCode);
            buffer.AppendFormat( ", ShipCountry:{0}", result.ShipCountry);

            Console.WriteLine(buffer);

            Assert.IsNotNull(result.MD5Hash);
            Assert.IsNotNullOrEmpty(result.TransactionType);
            Assert.IsNotNullOrEmpty(result.AuthorizationCode);
            Assert.IsNotNullOrEmpty(result.Method);
            Assert.AreEqual("CC", result.Method);
            Assert.IsNotNullOrEmpty(result.TransactionID);
            Assert.AreEqual("1", result.ResponseCode);
            Assert.AreEqual( "XXXX1111", result.CardNumber);
            Assert.AreEqual( "Visa", result.CardType);
            Assert.AreEqual("This transaction has been approved.", result.Message);
            Assert.IsTrue(result.Approved);
            Assert.IsFalse(result.Declined);
        }
        /// <summary>
        /// GetCustomerProfileIds - success
        /// </summary>
        [Test]
        public void GetCustomerProfileIds()
        {
            string[] customerIds = null;
            try
            {
                customerIds = _target.GetCustomerIDs();
            }
            catch (Exception e)
            {

                Console.WriteLine("CustomerGateway.GetCustomerIDs() failed: " + e.Message);
            }
            
            Assert.IsNotNull(customerIds);
        }
    }
}
