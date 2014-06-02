using AuthorizeNet;
using AuthorizeNet.APICore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace AuthorizeNETtest
{
    /// <summary>
    /// This is a test class for CustomerGatewayTest and is intended to contain all CustomerGateway Unit Tests
    /// </summary>
    [TestClass()]
    public class CustomerGatewayTest : BaseTest
    {
        /// <summary>
        /// CreateCustomer - success
        /// </summary>
        [TestMethod()]
        public void CreateCustomerTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString ="<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>24231938</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);
            string email = "suzhu@visa.com";
            string description = "CreateCustomerTest Success";
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
        [TestMethod()]
        public void CreateCustomerTest_CustomerID()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerProfileId>27092230</customerProfileId><customerPaymentProfileIdList /><customerShippingAddressIdList /><validationDirectResponseList /></createCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);
            string email = "suzhu@visa.com";
            string description = "CreateCustomerTest Success";
            string customerID = "Cust ID 1234";

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
                string s = e.Message;
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
        [TestMethod()]
        public void UpdateCustomerTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            Customer customer = new Customer()
            {
                ID = "",
                ProfileID = "24231938",
                Email = "suzhu@visa.com",
                Description = "UpdateCustomerTest Success"
            };
            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
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
        [TestMethod()]
        public void AddCreditCardTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24232683";
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = 16;

            string expected = "22219473";
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

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AddCreditCard set validationMode - success
        /// </summary>
        [TestMethod()]
        public void AddCreditCardTest_ValidationMode()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>22219473</customerPaymentProfileId></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey, ServiceMode.Test);

            string profileID = "24231938";
            string cardNumber = "4111111111111111";
            int expirationMonth = 1;
            int expirationYear = 16;

            string expected = "22219473";
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

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AddECheckBankAccount required data only to success- success
        /// </summary>
        [TestMethod()]
        public void AddECheckBankAccountTest_Required()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>24287597</customerPaymentProfileId><validationDirectResponse>1,1,1,(TESTMODE) This transaction has been approved.,000000,P,0,none,Test transaction for ValidateCustomerPaymentProfile.,1.00,ECHECK,auth_only,none,,,,,,,,,,,email@example.com,,,,,,,,,0.00,0.00,0.00,FALSE,none,ACD21540D94325D06FDC81558F3196AD,,,,,,,,,,,,,XXXX3458,Bank Account,,,,,,,,,,,,,,,,</validationDirectResponse></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24232683";

            string expected = "24287597";
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

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// AddECheckBankAccount all data success- success
        /// </summary>
        [TestMethod()]
        public void AddECheckBankAccountTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><customerPaymentProfileId>24282439</customerPaymentProfileId><validationDirectResponse>1,1,1,(TESTMODE) This transaction has been approved.,000000,P,0,none,Test transaction for ValidateCustomerPaymentProfile.,1.00,ECHECK,auth_only,none,,,,,,,,,,,email@example.com,,,,,,,,,0.00,0.00,0.00,FALSE,none,ACD21540D94325D06FDC81558F3196AD,,,,,,,,,,,,,XXXX4587,Bank Account,,,,,,,,,,,,,,,,</validationDirectResponse></createCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24232683";

            string expected = "24282439";
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

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// GetCustomer - success
        /// </summary>
        [TestMethod()]
        public void GetCustomerTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><profile><merchantCustomerId /><description>UpdateCustomerTest Success</description><email>suzhu@visa.com</email><customerProfileId>24231938</customerProfileId><paymentProfiles><customerType>individual</customerType><customerPaymentProfileId>22219473</customerPaymentProfileId><payment><creditCard><cardNumber>XXXX1111</cardNumber><expirationDate>XXXX</expirationDate></creditCard></payment></paymentProfiles></profile></getCustomerProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24231938";

            Customer expected = new Customer()
            {
                ProfileID = "24231938",
                Email = "suzhu@visa.com",
                Description = "UpdateCustomerTest Success",
                PaymentProfiles =
                    {
                        new PaymentProfile(new customerPaymentProfileMaskedType())
                            {
                                CardExpiration = "XXXX",
                                CardNumber = "XXXX1111",
                                ProfileID = "22219473"
                            }
                    }
            };

            Customer actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.GetCustomer(profileID);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ProfileID, actual.ProfileID);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.PaymentProfiles.Count, actual.PaymentProfiles.Count);
            Assert.AreEqual(expected.PaymentProfiles[0].CardExpiration, actual.PaymentProfiles[0].CardExpiration);
            Assert.AreEqual(expected.PaymentProfiles[0].CardNumber, actual.PaymentProfiles[0].CardNumber);
            Assert.AreEqual(expected.PaymentProfiles[0].ProfileID, actual.PaymentProfiles[0].ProfileID);
        }

        /// <summary>
        /// GetCustomer eCheck Bank Account - success
        /// </summary>
        [TestMethod()]
        public void GetCustomerTest_eCheck()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString1 = "<?xml version=\"1.0\" encoding=\"utf-8\"?><getCustomerProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><profile><description>UpdateCustomerTest Success</description><email>suzhu2@visa.com</email><customerProfileId>24236276</customerProfileId><paymentProfiles><customerPaymentProfileId>24287458</customerPaymentProfileId><payment><bankAccount><accountType>checking</accountType><routingNumber>XXXX0024</routingNumber><accountNumber>XXXX3456</accountNumber><nameOnAccount>Sue Zhu</nameOnAccount></bankAccount></payment></paymentProfiles></profile></getCustomerProfileResponse>";
            string responseString = responseString1;
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24236276";

            Customer expected = new Customer()
            {
                ProfileID = "24236276",
                Email = "suzhu2@visa.com",
                Description = "UpdateCustomerTest Success",
                PaymentProfiles =
                    {
                        new PaymentProfile(new customerPaymentProfileMaskedType())
                            {
                                eCheckBankAccount = new BankAccount()
                                    {
                                        accountTypeSpecified = true,
                                        accountType = BankAccountType.Checking, 
                                        routingNumber = "XXXX0024", 
                                        accountNumber = "XXXX3456", 
                                        nameOnAccount = "Sue Zhu", 
                                        bankName = "Bank of Seattle", 
                                        echeckType = EcheckType.WEB,
                                        echeckTypeSpecified = true
                                    }
                            }
                    }
            };

            Customer actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.GetCustomer(profileID);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ProfileID, actual.ProfileID);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.PaymentProfiles.Count, actual.PaymentProfiles.Count);
            Assert.AreEqual(expected.PaymentProfiles[0].eCheckBankAccount.accountNumber, actual.PaymentProfiles[0].eCheckBankAccount.accountNumber);
            Assert.AreEqual(expected.PaymentProfiles[0].eCheckBankAccount.routingNumber, actual.PaymentProfiles[0].eCheckBankAccount.routingNumber);
            Assert.AreEqual(expected.PaymentProfiles[0].eCheckBankAccount.accountNumber, actual.PaymentProfiles[0].eCheckBankAccount.accountNumber);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// Minimum parameters to ensure a successful response
        /// </summary>
        [TestMethod()]
        public void UpdatePaymentProfileMinTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24232683";
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = "22804148";
            profile.CardNumber = "4111111111111112";
            profile.CardExpiration = "2016-02";

            bool actual = false;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
            try
            {
                actual = target.UpdatePaymentProfile(profileID, profile);
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// UpdatePaymentProfile - success
        /// no mask and with billing
        /// </summary>
        [TestMethod()]
        public void UpdatePaymentProfileTest_NotMask()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24231938";
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = "22219473";
            profile.CardNumber = "4111111111111112";
            profile.CardExpiration = "2016-03";
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
                actual = target.UpdatePaymentProfile(profileID, profile);
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
        [TestMethod()]
        public void UpdatePaymentProfileTest_Mask()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24232683";
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = "22804148";
            profile.CardNumber = "XXXX1112";
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
                actual = target.UpdatePaymentProfile(profileID, profile);
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
        [TestMethod()]
        public void UpdatePaymentProfileTest_eCheckMask()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24236276";
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = "24287458";
            profile.eCheckBankAccount = new BankAccount()
                {
                    routingNumber = "XXXX0024",
                    accountNumber = "XXXX3456",
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
                actual = target.UpdatePaymentProfile(profileID, profile);
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
        [TestMethod()]
        public void UpdatePaymentProfileTest_eCheck()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><updateCustomerPaymentProfileResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages></updateCustomerPaymentProfileResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24236276";
            customerPaymentProfileMaskedType apiType = new customerPaymentProfileMaskedType();

            PaymentProfile profile = new PaymentProfile(apiType);
            profile.ProfileID = "24287458";
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
                actual = target.UpdatePaymentProfile(profileID, profile);
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
        [TestMethod()]
        public void AuthorizeAndCaptureTest()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2C99N3,Y,2207640586,,,25.10,CC,auth_capture,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C40BBCC10984A7A95471323B34FD4FFB,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";
            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = (decimal) 25.10;

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

            Assert.IsTrue(actual.AuthorizationCode.Trim().Length > 0);
            Assert.IsTrue(actual.TransactionID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(actual.TransactionID) > 0);
        }

        /// <summary>
        /// AuthorizeAndCapture Transaction with Invoice, Description and PONumber - Approved
        /// </summary>
        [TestMethod()]
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

            string profileID = "24231938";
            string paymentProfileID = "22219473";
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
        [TestMethod()]
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

            string profileID = "24231938";
            string paymentProfileID = "22219473";
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
        /// Capture Transaction - Approved
        /// </summary>
        [TestMethod()]
        public void SendTest_Capture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)25.12;
            string authCode = SendAuthOnly(amount + 1, false);
            Assert.IsTrue(authCode.Trim().Length > 0);

            //start testing
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,P,2207702175,,,25.12,CC,capture_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,5BB96CB66C1E0BCE123915E970D70166,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";

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
        [TestMethod()]
        public void SendTest_PriorAuthCapture_Approved()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            //setup
            decimal amount = (decimal)25.13;
            string transID = SendAuthOnly(amount + 1, true);
            Assert.IsTrue(transID.Trim().Length > 0);
            Assert.IsTrue(long.Parse(transID) > 0);

            //start testing
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,MFSOM8,P,2207702374,,,25.13,CC,prior_auth_capture,,,,,,,,,,,,,,,,,,,,,,,,,,E0DF3A88533C1F9CBE3B55159C514513,,,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

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

        private string SendAuthOnly(decimal amount, bool returnTransID)
        {
            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,2JM6IE,Y,2207702136,,,11.21,CC,auth_only,,,,,,,,,,,,suzhu@visa.com,,,,,,,,,,,,,,C8E9860C9B9DF58A73FFD9D7A8BFB82F,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";

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
        [TestMethod()]
        public void SendTest_AuthOnly_ExtraOptions()
        {
            //check login / password
            string sError = CheckLoginPassword();
            Assert.IsTrue(sError == "", sError);

            string responseString = "<?xml version=\"1.0\" encoding=\"utf-8\"?><createCustomerProfileTransactionResponse xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"AnetApi/xml/v1/schema/AnetApiSchema.xsd\"><messages><resultCode>Ok</resultCode><message><code>I00001</code><text>Successful.</text></message></messages><directResponse>1,1,1,This transaction has been approved.,E4CGH9,Y,2210636215,,,25.15,CC,auth_only,Testing Extra Option,Sue,Zhu,Visa,123 Elm Street,Bellevue,WA,98006,US,,,suzhu@visa.com,,,,,,,,,,,,,,3445C1C7DFFB2F32357A316DE94C13D1,,2,,,,,,,,,,,XXXX1111,Visa,,,,,,,,,,,,,,,,</directResponse></createCustomerProfileTransactionResponse>";
            LocalRequestObject.ResponseString = responseString;
            XmlSerializer serializer = new XmlSerializer(typeof(createCustomerProfileTransactionResponse));
            StringReader reader = new StringReader(responseString);
            createCustomerProfileTransactionResponse apiResponse = (createCustomerProfileTransactionResponse)serializer.Deserialize(reader);
            IGatewayResponse expected = new GatewayResponse(apiResponse.directResponse.Split(','));

            CustomerGateway target = new CustomerGateway(ApiLogin, TransactionKey);

            string profileID = "24231938";
            string paymentProfileID = "22219473";
            Order order = new Order(profileID, paymentProfileID, "");
            order.Amount = (decimal)25.15;
            order.ExtraOptions = "x_customer_ip=100.0.0.1&x_cust_id=Testing Extra Options";

            IGatewayResponse actual = null;

            // if choose "USELOCAL", the test should pass with no exception
            // Otherwise, the test might fail for error, i.e. duplicated request.
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
    }
}
