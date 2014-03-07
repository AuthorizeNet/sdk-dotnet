using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Net;

namespace AuthorizeNETtest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BaseTest
    {
        protected static WebRequestCreateLocal LocalRequestObject = new WebRequestCreateLocal();
        protected string ApiLogin = "";
        protected string TransactionKey = "";
        protected string ApiLoginCP = "";
        protected string TransactionKeyCP = "";

        public BaseTest()
        {
#if USELOCAL
            WebRequest.RegisterPrefix("https://", LocalRequestObject);
#endif
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        /// <summary>
        /// CheckLoginPassword - make sure that we are not using the default invalid login and password.
        /// </summary>
        protected string CheckLoginPassword()
        {
            string sRet = "";
            if ((string.IsNullOrEmpty(ApiLogin)) || (ApiLogin.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKey)) || (TransactionKey.Trim().Length == 0)
                || (string.IsNullOrEmpty(ApiLoginCP)) || (ApiLoginCP.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKeyCP)) || (TransactionKeyCP.Trim().Length == 0))
            {
                LoadLoginTranskey();
            }

            if ((string.IsNullOrEmpty(ApiLogin)) || (ApiLogin.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKey)) || (TransactionKey.Trim().Length == 0)
                || (string.IsNullOrEmpty(ApiLoginCP)) || (ApiLoginCP.Trim().Length == 0)
                || (string.IsNullOrEmpty(TransactionKeyCP)) || (TransactionKeyCP.Trim().Length == 0))
            {
                sRet = "Invalid Login / Password: blank \n";
            }

#if !USELOCAL
            if ((ApiLogin == "ApiLogin") || (TransactionKey == "TransactionKey")
                || (ApiLoginCP == "ApiLoginCP") || (TransactionKeyCP == "TransactionKeyCP"))
            {
                sRet += "Invalid Login / Password \n";
            }
#endif
            return sRet;
        }

        protected void LoadLoginTranskey()
        {
            ApiLogin = ConfigurationManager.AppSettings["ApiLogin"];
            TransactionKey = ConfigurationManager.AppSettings["TransactionKey"];
            ApiLoginCP = ConfigurationManager.AppSettings["ApiLoginCP"];
            TransactionKeyCP = ConfigurationManager.AppSettings["TransactionKeyCP"];
        }
    }
}
