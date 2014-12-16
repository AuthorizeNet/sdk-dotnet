using AuthorizeNet;
using AuthorizeNet.APICore;
using V1 = AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers.Test;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for CustomerGatewayTest and is intended to contain all CustomerGateway Unit Tests
    /// </summary>
    [TestFixture()]
    public class CustomerGatewayTest : BaseTest
    {
        /// <summary>
        /// CreateCustomer - success
        /// </summary>
        [Test()]
        public void CreateCustomerTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString ="<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>24231938</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);
            string email = string.Format("suzhu{0}@visa.com", rnd.Next(9999).ToString());
            string description = string.Format("CreateCustomerTest Success {0}", rnd.Next(9999).ToString());
            Customer expected = new Customer()
                {
                    Email = email,
                    Description = description
                };
            Customer actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.CreateCustomer(email, description);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ID));
            Assert.IsTrue(actual.ID.Trim().Length > 0);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ProfileID));
            Assert.IsTrue(actual.ProfileID.Trim().Length > 0);
        }

        /// <summary>
        /// CreateCustomer - success
        /// </summary>
        [Test()]
        public void CreateCustomerTest_CustomerID()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>27092230</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);
            string randomID = rnd.Next(9999).ToString();
            string email = string.Format("suzhu{0}@visa.com", randomID);
            string description = string.Format("CreateCustomerTest Success {0}", randomID);
            string customerID = string.Format("Cust ID {0}", randomID);

            Customer expected = new Customer()
            {
                ID = customerID,
                Email = email,
                Description = description
            };
            Customer actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.CreateCustomer(email, description, customerID);
            }
            catch (Exception e)
            {
                Assert.Fail( e.Message);
            }

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.IsFalse(string.IsNullOrEmpty(actual.ProfileID));
            Assert.IsTrue(actual.ProfileID.Trim().Length > 0);
        }

        /// <summary>
        /// UpdateCustomer - successful
        /// </summary>
        [Test()]
        public void UpdateCustomerTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //Create new customer to update
            string customerProfileID = getNewCustomerID();

            string randomID = rnd.Next(9999).ToString();

            //Update customer record.
            Customer customer = new Customer()
            {
                ID = "",
                ProfileID = customerProfileID,
                Email = string.Format("suzhu{0}@visa.com", randomID),
                Description = string.Format("CreateCustomerTest Success {0}", randomID)
            };
            bool actual = false;

            try
            {
                actual = target.UpdateCustomer(customer);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// AddCreditCard - success
        /// </summary>
        [Test()]
        public void AddCreditCardTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = this.getNewCustomerID();

            //string profileID = "24232683";
            string cardNumber = "4111111111111111";
            int expirationMonth = rnd.Next(1,12);
            int expirationYear = DateTime.Now.Year + 2;

            //string expected = "22219473";
            string actual = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
            Assert.IsTrue(actual != null && actual != string.Empty);
            //Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AddCreditCard set validationMode - success
        /// </summary>
        [Test()]
        public void AddCreditCardTest_ValidationMode()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey, ServiceMode.Test);

            string profileID = getNewCustomerID();
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = 16;

            string actual = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual != null && actual != string.Empty);
        }

        /// <summary>
        /// AddECheckBankAccount required data only to success- success
        /// </summary>
        [Test()]
        public void AddECheckBankAccountTest_Required()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>24287597</customerPaymentProfileId><validationDirectResponse>1,1,1,(TESTMODE) This transaction has been approved.,000000,P,0,none,Test transaction for ValidateCustomerPaymentProfile.,1.00,ECHECK,auth_only,none,,,,,,,,,,,email@example.com,,,,,,,,,0.00,0.00,0.00,FALSE,none,ACD21540D94325D06FDC81558F3196AD,,,,,,,,,,,,,XXXX3458,Bank Account,,,,,,,,,,,,,,,,</validationDirectResponse></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string newCustomerProfileId = getNewCustomerID();

            string profileID = newCustomerProfileId;

            string actual = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AddECheckBankAccount(profileID, BankAccountType.Checking, "125000024", "123458", "Sue Zhu");
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual != null && actual != string.Empty);
        }

        /// <summary>
        /// AddECheckBankAccount all data success- success
        /// </summary>
        [Test()]
        public void AddECheckBankAccountTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>24282439</customerPaymentProfileId><validationDirectResponse>1,1,1,(TESTMODE) This transaction has been approved.,000000,P,0,none,Test transaction for ValidateCustomerPaymentProfile.,1.00,ECHECK,auth_only,none,,,,,,,,,,,email@example.com,,,,,,,,,0.00,0.00,0.00,FALSE,none,ACD21540D94325D06FDC81558F3196AD,,,,,,,,,,,,,XXXX4587,Bank Account,,,,,,,,,,,,,,,,</validationDirectResponse></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = getNewCustomerID();

            string actual = "";

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AddECheckBankAccount(profileID, BankAccountType.Savings, "125000024", "1234588", "Sue Zhu", "Bank of Seattle", EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual != null && actual != string.Empty);
        }

        /// <summary>
        /// GetCustomer - success
        /// </summary>
        [Test()]
        public void GetCustomerTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><profile><merchantCustomerId /><description>UpdateCustomerTest Success</description><email>suzhu@visa.com</email><customerProfileId>24231938</customerProfileId><paymentProfiles><customerType>individual</customerType><customerPaymentProfileId>22219473</customerPaymentProfileId><payment><creditCard><cardNumber>XXXX1111</cardNumber><expirationDate>XXXX</expirationDate></creditCard></payment></paymentProfiles></profile></getCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //Build new customer with credit card payment profile.
            Customer originalCustomer = null;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";
            string paymentProfileID = string.Empty;

            try
            {
                string email = string.Format("suzhu{0}@visa.com", rnd.Next(9999).ToString());
                string description = string.Format("CreateCustomerTest Success {0}", rnd.Next(9999).ToString());
                originalCustomer = new Customer()
                {
                    Email = email,
                    Description = description
                };

                originalCustomer = target.CreateCustomer(email, description);

                paymentProfileID = target.AddCreditCard(originalCustomer.ProfileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Customer actual = null;

            try
            {
                actual = target.GetCustomer(originalCustomer.ProfileID);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(originalCustomer.ProfileID, actual.ProfileID);
            Assert.AreEqual(originalCustomer.Email, actual.Email);
            Assert.AreEqual(originalCustomer.Description, actual.Description);
            Assert.AreEqual(1, actual.PaymentProfiles.Count);
            Assert.AreEqual(maskedExpirationDate, actual.PaymentProfiles[0].CardExpiration);
            Assert.AreEqual(maskedCardNumber, actual.PaymentProfiles[0].CardNumber);
            Assert.AreEqual(paymentProfileID, actual.PaymentProfiles[0].ProfileID);
        }

        /// <summary>
        /// GetCustomer eCheck Bank Account - success
        /// </summary>
        [Test()]
        public void GetCustomerTest_eCheck()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString1 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><profile><description>UpdateCustomerTest Success</description><email>suzhu2@visa.com</email><customerProfileId>24236276</customerProfileId><paymentProfiles><customerPaymentProfileId>24287458</customerPaymentProfileId><payment><bankAccount><accountType>checking</accountType><routingNumber>XXXX0024</routingNumber><accountNumber>XXXX3456</accountNumber><nameOnAccount>Sue Zhu</nameOnAccount></bankAccount></payment></paymentProfiles></profile></getCustomerProfileResponse>";
            string responseString = responseString1;
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //Build new customer with credit card payment profile.
            Customer originalCustomer = null;
            string routingNumber = "125000024";
            string maskedRoutingNumber = "XXXX0024";
            string accountNumber = "1234588";
            string maskedAccountNumber = "XXXX4588";
            string nameOnAccount = "Sue Zhu";
            string bankName = "Bank of Seattle";
            string paymentProfileID = string.Empty;

            try
            {
                string email = string.Format("suzhu{0}@visa.com", rnd.Next(9999).ToString());
                string description = string.Format("CreateCustomerTest Success {0}", rnd.Next(9999).ToString());
                originalCustomer = new Customer()
                {
                    Email = email,
                    Description = description
                };

                originalCustomer = target.CreateCustomer(email, description);

                paymentProfileID = target.AddECheckBankAccount(originalCustomer.ProfileID, BankAccountType.Savings, routingNumber, accountNumber, nameOnAccount, bankName, EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Customer actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.GetCustomer(originalCustomer.ProfileID);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(originalCustomer.ProfileID, actual.ProfileID);
            Assert.AreEqual(originalCustomer.Email, actual.Email);
            Assert.AreEqual(originalCustomer.Description, actual.Description);
            Assert.AreEqual(1, actual.PaymentProfiles.Count);
            Assert.AreEqual(maskedAccountNumber, actual.PaymentProfiles[0].eCheckBankAccount.accountNumber);
            Assert.AreEqual(maskedRoutingNumber, actual.PaymentProfiles[0].eCheckBankAccount.routingNumber);
            Assert.AreEqual(bankName, actual.PaymentProfiles[0].eCheckBankAccount.bankName);
            Assert.AreEqual(nameOnAccount, actual.PaymentProfiles[0].eCheckBankAccount.nameOnAccount);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// Minimum parameters to ensure a successful response
        /// </summary>
        [Test()]
        public void UpdatePaymentProfileMinTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();


            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = paymentProfileID;
            profile.CardNumber = "4111111111111112";
            profile.CardExpiration = "2018-02";

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(profileID, profile);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// no mask and with billing
        /// </summary>
        [Test()]
        public void UpdatePaymentProfileTest_NotMask()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer for test
            string customerProfileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;

            try
            {
                string rndId = rnd.Next(9999).ToString();
                customerProfileID = getNewCustomerID();
                Address billingAddress = new Address(){
                    First = "Sue",
                    Last = "Zhu",
                    Company = "Visa",
                    Street = "123 Elm Street",
                    City = "Bellevue",
                    State = "WA",
                    Country = "US",
                    Zip = "98006"
                };

                paymentProfileID = target.AddCreditCard(customerProfileID, cardNumber, expirationMonth, expirationYear, null, billingAddress); 
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            

            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = paymentProfileID;
            profile.CardNumber = "4111111111111112";
            profile.CardExpiration = "2016-03";
            profile.BillingAddress = new Address()
                {
                    First = "Sue1",
                    Last = "Zhu1",
                    Company = "Visa1",
                    Street = "1231 Elm Street",
                    City = "Bellevue1",
                    State = "WA",
                    Country = "US",
                    Zip = "98001"
                };

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(customerProfileID, profile);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// with mask
        /// </summary>
        [Test()]
        public void UpdatePaymentProfileTest_Mask()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer for test
            string customerProfileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;

            try
            {
                string rndId = rnd.Next(9999).ToString();
                customerProfileID = getNewCustomerID();

                paymentProfileID = target.AddCreditCard(customerProfileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = paymentProfileID;
            profile.CardNumber = "XXXX1111";
            profile.CardExpiration = "XXXX";
            profile.BillingAddress = new Address()
                {
                    First = "Sue",
                    Last = "Zhu",
                    Company = "Visa",
                    Street = "123 Elm Street",
                    City = "Bellevue",
                    State = "WA",
                    Country = "US",
                    Zip = "98006"
                };

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(customerProfileID, profile);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile eCheck - success
        /// with mask
        /// </summary>
        [Test()]
        public void UpdatePaymentProfileTest_eCheckMask()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //Build new customer with credit card payment profile.
            Customer originalCustomer = null;
            string routingNumber = "125000024";
            string maskedRoutingNumber = "XXXX0024";
            string accountNumber = "1234588";
            string maskedAccountNumber = "XXXX4588";
            string nameOnAccount = "Sue Zhu";
            string bankName = "Bank of Seattle";
            string paymentProfileID = string.Empty;

            try
            {
                string email = string.Format("suzhu{0}@visa.com", rnd.Next(9999).ToString());
                string description = string.Format("CreateCustomerTest Success {0}", rnd.Next(9999).ToString());
                originalCustomer = new Customer()
                {
                    Email = email,
                    Description = description
                };

                originalCustomer = target.CreateCustomer(email, description);

                paymentProfileID = target.AddECheckBankAccount(originalCustomer.ProfileID, BankAccountType.Savings, routingNumber, accountNumber, nameOnAccount, bankName, EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = paymentProfileID;
            profile.eCheckBankAccount = new BankAccount()
                {
                    routingNumber = "XXXX0024",
                    accountNumber = "XXXX4588",
                    nameOnAccount = "Sue Zhu"
                };

            profile.BillingAddress = new Address()
            {
                First = "Sue",
                Last = "Zhu",
                Company = "Visa",
                Street = "123 Elm Street",
                City = "Bellevue",
                State = "WA",
                Country = "US",
                Zip = "98006"
            };

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(originalCustomer.ProfileID, profile);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile eCheck - success
        /// with mask
        /// </summary>
        [Test()]
        public void UpdatePaymentProfileTest_eCheck()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //Build new customer with credit card payment profile.
            Customer originalCustomer = null;
            string routingNumber = "125000024";
            string maskedRoutingNumber = "XXXX0024";
            string accountNumber = "1234588";
            string maskedAccountNumber = "XXXX4588";
            string nameOnAccount = "Sue Zhu";
            string bankName = "Bank of Seattle";
            string paymentProfileID = string.Empty;

            try
            {
                string email = string.Format("suzhu{0}@visa.com", rnd.Next(9999).ToString());
                string description = string.Format("CreateCustomerTest Success {0}", rnd.Next(9999).ToString());
                originalCustomer = new Customer()
                {
                    Email = email,
                    Description = description
                };

                originalCustomer = target.CreateCustomer(email, description);

                paymentProfileID = target.AddECheckBankAccount(originalCustomer.ProfileID, BankAccountType.Savings, routingNumber, accountNumber, nameOnAccount, bankName, EcheckType.WEB, null);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }


            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = paymentProfileID;
            profile.eCheckBankAccount = new BankAccount()
            {
                routingNumber = "125000025",
                accountNumber = "1234567",
                nameOnAccount = "Sue Zhu"
            };

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(originalCustomer.ProfileID, profile);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction - Approved
        /// </summary>
        [Test()]
        public void AuthorizeAndCaptureTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Order order = new Order(profileID, paymentProfileID, "");

            //random amount so rerunning test does not fail with duplicate transaction
            order.Amount = (decimal)((double)rnd.Next(9999)/100);


            //define expected result
            string responseString = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2C99N3,Y,2207640586,,,25.10,CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C40BBCC10984A7A95471323B34FD4FFB,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>", order.Amount);
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            IGatewayResponse actual = null;
            
            try
            {
                actual = target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            //taking amount from 
            Assert.AreEqual(order.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction with Invoice, Description and PONumber - Approved
        /// </summary>
        [Test()]
        public void AuthorizeAndCaptureTest_InvoiceDescriptionPONumber()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,Q5G0UI,Y,2207641147,Invoice#123,Testing InvoiceDescriptionPONumber,25.10,CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,PO23456,BEEEB7C9F2F22B9955338A7E19427369,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }


            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = (decimal)25.10;
            order.InvoiceNumber = "Invoice#123";
            order.Description = "Testing InvoiceDescriptionPONumber";
            order.PONumber = "PO23456";

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);
            Assert.AreEqual(expected.InvoiceNumber, actual.InvoiceNumber);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction - Approved
        /// </summary>
        [Test()]
        public void AuthorizeAndCaptureTest_ExtraOptions()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,0PI2II,Y,2210620905,,,25.10,CC,auth_capture,Testing Extra Option,Sue,Zhu,Visa,123 Elm Street,Bellevue,WA,98006,US,,,suzhu@visa.com,,,,,,,,,,,,,,070CC74A6FDD5EA7951444C547FE7829,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            } 
            
            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = (decimal)25.10;
            order.ExtraOptions = "x_customer_ip=100.0.0.1&x_cust_id=Testing Extra Options";

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.AuthorizeAndCapture(order);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// Capture Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_Capture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            } 
            
            decimal amount = (decimal)((double)rnd.Next(9999) / 100);
            string authCode = SendAuthOnly(profileID, paymentProfileID, amount + 1, false);
            Assert.IsTrue(authCode.Trim().Length > 0);

            //start testing
            string responseString = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,P,2207702175,,,{0},CC,capture_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,5BB96CB66C1E0BCE123915E970D70166,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>", amount);
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));



            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.Capture(profileID, paymentProfileID, "", amount, authCode);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// PriorAuthCapture Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_PriorAuthCapture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            decimal amount = (decimal)rnd.Next(9999) / 100;
            string transID = SendAuthOnly(profileID, paymentProfileID, amount + 1, true);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,MFSOM8,P,2207702374,,,{0},CC,prior_auth_capture,,,,,,,,,,,,,,,,,,,,,,,,,,E0DF3A88533C1F9CBE3B55159C514513,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>", amount);
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.PriorAuthCapture(transID, amount);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        private string SendAuthOnly(string profileID, string paymentProfileID, decimal amount, bool returnTransID)
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,Y,2207702136,,,{0},CC,auth_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C8E9860C9B9DF58A73FFD9D7A8BFB82F,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>", amount);
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);


            IGatewayResponse response = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                response = target.Authorize(profileID, paymentProfileID, amount);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            if (response != null && response.Approved)
            {
                if (returnTransID)
                {
                    return response.TransactionID;
                }
                else
                {
                    return response.AuthorizationCode;
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// PriorAuthCapture Transaction - Approved
        /// </summary>
        [Test()]
        public void SendTest_AuthOnly_ExtraOptions()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //set random transaction amount so rerun does not fail with duplicate transaction
            decimal testAmount = (decimal)((double)rnd.Next(9999) / 100);

            string responseString = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,E4CGH9,Y,2210636215,,,{0},CC,auth_only,Testing Extra Option,Sue,Zhu,Visa,123 Elm Street,Bellevue,WA,98006,US,,,suzhu@visa.com,,,,,,,,,,,,,,3445C1C7DFFB2F32357A316DE94C13D1,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>", testAmount);
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = target.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = testAmount;
            order.ExtraOptions = "x_customer_ip=100.0.0.1&x_cust_id=Testing Extra Options";

            IGatewayResponse actual = null;

            try
            {
                actual = target.Authorize(order);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Approved, actual.Approved);
            Assert.AreEqual(expected.CardNumber, actual.CardNumber);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.ResponseCode, actual.ResponseCode);

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        [Test]
        public void TestCheckForErrorscreateCustomerProfileTransactionResponse()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);
            var gateway = new CustomerGateway(ApiLogin, TransactionKey);

            //create customer and payment profile to update
            string profileID = string.Empty;
            string paymentProfileID = string.Empty;
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = DateTime.Now.Year + 1;
            string maskedExpirationDate = "XXXX";
            string maskedCardNumber = "XXXX1111";

            try
            {
                profileID = getNewCustomerID();
                paymentProfileID = gateway.AddCreditCard(profileID, cardNumber, expirationMonth, expirationYear);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            
            var order = new Order(profileID, paymentProfileID, "") { Amount = (decimal)((double)rnd.Next(9999) / 100) };
            var response = gateway.AuthorizeAndCapture(order);
            Assert.IsNotNull(response);
        }
    }
}
